using MediatR;

namespace PGK.Application.App.User.Commands.SendEmailVerification
{
    public class SendEmailVerificationCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
