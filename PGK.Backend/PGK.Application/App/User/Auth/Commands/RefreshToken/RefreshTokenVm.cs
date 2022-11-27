using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Auth.Commands.RefreshToken
{
    public class RefreshTokenVm
    {
        [Required] public string AccessToken { get; set; } = string.Empty;
    }
}
