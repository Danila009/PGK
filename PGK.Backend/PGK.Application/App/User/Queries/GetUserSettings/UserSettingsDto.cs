using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.Language;
using PGK.Domain.User.Enums;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Queries.GetUserSettings
{
    public class UserSettingsDto : IMapWith<Domain.User.User>
    {
        public bool? DrarkMode { get; set; }
        [Required] public ThemeStyle ThemeStyle { get; set; }
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User.User, UserSettingsDto>();
        }
    }
}
