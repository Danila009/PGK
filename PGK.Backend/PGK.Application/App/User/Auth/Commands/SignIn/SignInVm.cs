using PGK.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Auth.Commands.SignIn
{
    public class SignInVm
    {
        [Required] public string AccessToken { get; set; } = string.Empty;
        [Required] public string RefreshToken { get; set; } = string.Empty;

        [Required] public int UserId { get; set; }
        [Required] public string UserRole { get; set; }

        [Required] public bool DrarkMode { get; set; }
        [Required] public SecondaryBackground SecondaryBackground { get; set; }

    }
}
