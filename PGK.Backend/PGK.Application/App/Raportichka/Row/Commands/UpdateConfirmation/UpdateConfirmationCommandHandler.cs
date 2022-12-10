using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateConfirmation
{
    public class UpdateConfirmationCommandHandler
        : IRequestHandler<UpdateConfirmationCommand, UpdateConfirmationVm>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateConfirmationCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<UpdateConfirmationVm> Handle(UpdateConfirmationCommand request,
            CancellationToken cancellationToken)
        {
            if(request.Role != UserRole.TEACHER || request.Role != UserRole.ADMIN)
            {
                throw new UnauthorizedAccessException("У вас нет доступа к этой рапортичке");
            }

            var row = await _dbContext.RaportichkaRows.FindAsync(request.RaportichkaRowId);

            if (row == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.RaportichkaRow),
                    request.RaportichkaRowId);
            }

            if(request.Role != UserRole.ADMIN)
            {
                var teacher = await _dbContext.TeacherUsers
                    .Include(u => u.RaportichkaRows)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser),
                        request.UserId);
                }

                if (!teacher.RaportichkaRows.Any(u => u.Id == request.RaportichkaRowId))
                {
                    throw new UnauthorizedAccessException("У вас нет доступа к этой рапортичке");
                }
            }

            row.Confirmation = !row.Confirmation;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateConfirmationVm { Confirmation = row.Confirmation };
        }
    }
}
