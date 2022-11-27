using PGK.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Headman.Commands.Registration
{
    public class RegistrationHeadmanVm
    {
        [Required] public string AccessToken { get; set; } = string.Empty;
        [Required] public string RefreshToken { get; set; } = string.Empty;

        [Required] public string UserRole { get; set; }
    }
}
