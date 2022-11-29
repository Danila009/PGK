using MediatR;

namespace PGK.Application.App.User.Commands.UpdateEmail
{
    public class UserUpdateEmailCommand : IRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
