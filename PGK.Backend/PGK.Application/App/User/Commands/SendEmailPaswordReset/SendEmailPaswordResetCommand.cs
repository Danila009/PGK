using MediatR;

namespace PGK.Application.App.User.Commands.SendEmailPaswordReset
{
    public class SendEmailPaswordResetCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
