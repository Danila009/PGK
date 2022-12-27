using MediatR;

namespace PGK.Application.App.User.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<string>
    {
        public int UserId { get; set; }
    }
}
