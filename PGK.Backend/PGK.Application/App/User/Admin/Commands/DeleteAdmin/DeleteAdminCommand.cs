using MediatR;

namespace PGK.Application.App.User.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest
    {
        public int AdminId { get; set; }
    }
}
