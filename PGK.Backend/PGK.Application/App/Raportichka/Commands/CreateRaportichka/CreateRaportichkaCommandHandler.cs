using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Raportichka.Commands.CreateRaportichka
{
    public class CreateRaportichkaCommandHandler
        : IRequestHandler<CreateRaportichkaCommand, CreateRaportichkaVm>
    {
        public readonly IPGKDbContext _dbContext;

        public CreateRaportichkaCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateRaportichkaVm> Handle(CreateRaportichkaCommand request, 
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            var raportichka = new Domain.Raportichka.Raportichka
            {
                Date = request.Date,
                Group = group
            };

            await _dbContext.Raportichkas.AddAsync(raportichka);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateRaportichkaVm
            {
                Id = raportichka.Id
            };
        }
    }
}
