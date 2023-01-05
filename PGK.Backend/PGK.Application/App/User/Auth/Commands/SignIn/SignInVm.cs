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

        [Required] public bool IncludedNotifications { get; set; } = true;
        [Required] public bool SoundNotifications { get; set; } = true;
        [Required] public bool VibrationNotifications { get; set; } = true;
        [Required] public bool IncludedSchedulesNotifications { get; set; } = true;
        [Required] public bool IncludedJournalNotifications { get; set; } = true;
        [Required] public bool IncludedRaportichkaNotifications { get; set; } = true;
        [Required] public bool IncludedTechnicalSupportNotifications { get; set; } = true;

    }
}
