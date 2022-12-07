using MediatR;

namespace PGK.Application.App.User.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenVm>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
