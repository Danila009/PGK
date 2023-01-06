using MediatR;
using PGK.Application.Common.Model;

namespace PGK.Application.App.User.Commands.UpdateEmail
{
    public class UserUpdateEmailCommand : IRequest<MessageModel>
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
