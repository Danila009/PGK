using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Raportichka.Commands.UpdateRaportichka
{
    public class UpdateRaportichkaCommandHandler
        : IRequestHandler<UpdateRaportichkaCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateRaportichkaCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateRaportichkaCommand request,
            CancellationToken cancellationToken)
        {
            var raportichka = await _dbContext.Raportichkas.FindAsync(request.Id);

            if (raportichka == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.Raportichka), request.Id);
            }

            var group = await _dbContext.Groups.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            raportichka.Group = group;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
