using PGK.Domain.Language;
using PGK.Domain.User.Enums;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Auth.Commands.SignIn
{
    public class SignInVm
    {
        public string? ErrorMessage { get; set; }
        [Required] public string AccessToken { get; set; } = string.Empty;
        [Required] public string RefreshToken { get; set; } = string.Empty;

        [Required] public int UserId { get; set; }
        [Required] public string UserRole { get; set; } = string.Empty;

        [Required] public bool? DrarkMode { get; set; }
        [Required] public ThemeStyle ThemeStyle { get; set; } = ThemeStyle.Blue;
        [Required] public ThemeFontStyle ThemeFontStyle { get; set; } = ThemeFontStyle.Default;
        [Required] public ThemeFontSize ThemeFontSize { get; set; } = ThemeFontSize.Medium;
        [Required] public ThemeCorners ThemeCorners { get; set; } = ThemeCorners.Rounded;
        
        [Required] public Domain.Language.Language? Language { get; set; }

        [MaxLength(256)] public string? Email { get; set; } = string.Empty;
        [Required] public bool EmailVerification { get; set; } = false;

        public int? TelegramId { get; set; }
    }
}
