using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User;

namespace PGK.Application.App.Raportichka.Row.Commands.DeleteRow
{
    public class DeleteRaportichkaRowCommandHandler
        : IRequestHandler<DeleteRaportichkaRowCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteRaportichkaRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteRaportichkaRowCommand request,
            CancellationToken cancellationToken)
        {
            var raportichkaRow = await _dbContext.RaportichkaRows
                .Include(u => u.Teacher)
                .Include(u => u.Raportichka)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Headman)
                .Include(u => u.Raportichka)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.DeputyHeadma)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            if (raportichkaRow == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.RaportichkaRow), request.Id);
            }

            if (request.UserRole == UserRole.TEACHER)
            {
                if(raportichkaRow.Teacher.Id != request.UserId)
                {
                    throw new UnauthorizedAccessException("У вас нет доступа к этой рапортичке");
                }
            }
            else if (request.UserRole == UserRole.HEADMAN || request.UserRole == UserRole.DEPUTY_HEADMAN)
            {
                if(
                    raportichkaRow.Raportichka.Group.Headman.Id != request.UserId ||
                    raportichkaRow.Raportichka.Group.DeputyHeadma.Id != request.UserId
                    )
                {
                    throw new UnauthorizedAccessException("У вас нет доступа к этой рапортичке");
                }
            }
            else if (request.UserRole != UserRole.ADMIN)
            {
                throw new UnauthorizedAccessException("У вас нет доступа к этой рапортичке");
            }

            _dbContext.RaportichkaRows.Remove(raportichkaRow);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
