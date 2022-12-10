using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User;
using PGK.Domain.User.Student;

namespace PGK.Application.App.Raportichka.Commands.DeleteRaportichka
{
    public class DeleteRaportichkaCommandHandler
        : IRequestHandler<DeleteRaportichkaCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteRaportichkaCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteRaportichkaCommand request,
            CancellationToken cancellationToken)
        {
            if(request.UserRole != UserRole.HEADMAN ||
                request.UserRole != UserRole.DEPUTY_HEADMAN ||
                request.UserRole != UserRole.ADMIN)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.UserRole != UserRole.ADMIN)
            {
                var student = await _dbContext.StudentsUsers
                    .Include(u => u.Group)
                        .ThenInclude(u => u.Raportichkas)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (student == null)
                {
                    throw new NotFoundException(nameof(StudentUser), request.Id);
                }

                if (student.Group.Raportichkas.Any(u => u.Id != request.Id))
                {
                    throw new UnauthorizedAccessException("Вы пытаетесь удалить рапортичку другой группы");
                }
            }

            var raportichka = await _dbContext.Raportichkas.FindAsync(request.Id);

            if(raportichka == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.Raportichka), request.Id);
            }

            _dbContext.Raportichkas.Remove(raportichka);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
