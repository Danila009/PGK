using MediatR;

namespace PGK.Application.App.User.Commands.UpdateDrarkMode
{
    public class UpdateDrarkModeCommand : IRequest<UpdateDrarkModeVm>
    {
        public int UserId { get; set; }
    }
}
