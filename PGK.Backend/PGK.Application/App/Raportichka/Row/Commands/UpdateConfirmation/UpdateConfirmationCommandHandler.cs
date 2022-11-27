using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

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
            var row = await _dbContext.RaportichkaRows.FindAsync(request.RaportichkaRowId);

            if (row == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.RaportichkaRow),
                    request.RaportichkaRowId);
            }

            row.Confirmation = !row.Confirmation;
            await _dbContext.SaveChangesAsync(cancellationToken);


            return new UpdateConfirmationVm { Confirmation = row.Confirmation };
        }
    }
}
