using MediatR;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateConfirmation
{
    public class UpdateConfirmationCommand : IRequest<UpdateConfirmationVm>
    {
        public int RaportichkaRowId { get; set; }
    }
}
