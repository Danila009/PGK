using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenVm>
    {
        [Required] public string RefreshToken { get; set; } = string.Empty;
    }
}
