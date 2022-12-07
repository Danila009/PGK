using MediatR;

namespace PGK.Application.App.User.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
