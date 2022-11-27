using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest
    {
        [Required] public string RefreshToken { get; set; } = string.Empty;
    }
}
