using MediatR;

namespace PGK.Application.App.User.Commands.SendEmailPaswordReset
{
    public class SendEmailPaswordResetCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
