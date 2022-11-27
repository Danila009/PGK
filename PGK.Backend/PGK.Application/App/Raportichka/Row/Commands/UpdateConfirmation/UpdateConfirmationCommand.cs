using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateConfirmation
{
    public class UpdateConfirmationCommand : IRequest<UpdateConfirmationVm>
    {
        public UserRole Role { get; set; }
        public int UserId { get; set; }
        public int RaportichkaRowId { get; set; }
    }
}
