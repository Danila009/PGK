using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

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
            var raportichkaRow = await _dbContext.RaportichkaRows.FindAsync(request.Id);

            if (raportichkaRow == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.RaportichkaRow), request.Id);
            }

            _dbContext.RaportichkaRows.Remove(raportichkaRow);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
