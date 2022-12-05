using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Repository.FileRepository;
using PGK.Domain.User;
using PGK.Domain.User.Student;

namespace PGK.Application.App.Vedomost.Commands.DeleteVedomost
{
    internal class DeleteVedomostCommandHandler
        : IRequestHandler<DeleteVedomostCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteVedomostCommandHandler(IPGKDbContext dbContext) => _dbContext = dbContext;


        public async Task<Unit> Handle(DeleteVedomostCommand request,
            CancellationToken cancellationToken)
        {
            var vedomost = await _dbContext.Vedomost
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);
        
            if(vedomost == null)
            {
                throw new NotFoundException(nameof(Domain.Vedomost.Vedomost), request.VedomostId);
            }

            if (request.Role == UserRole.HEADMAN || request.Role == UserRole.DEPUTY_HEADMAN)
            {
                var student = await _dbContext.StudentsUsers
                    .Include(u => u.Group)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (student == null)
                {
                    throw new NotFoundException(nameof(StudentUser), request.UserId);
                }

                if(student.Group.Id != vedomost.Group.Id)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            else if(request.Role != UserRole.ADMIN)
            {
                throw new UnauthorizedAccessException();
            }

            _dbContext.Vedomost.Remove(vedomost);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
