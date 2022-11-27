using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

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
