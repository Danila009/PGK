using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Commands.UpdateNotificationsSettings
{
    public class UpdateNotificationsSettingsCommand : IRequest<UserSettingsDto>
    {
        [Required] public int UserId { get; set; }

        [Required] public bool IncludedNotifications { get; set; } = true;
        [Required] public bool SoundNotifications { get; set; } = true;
        [Required] public bool VibrationNotifications { get; set; } = true;
        [Required] public bool IncludedSchedulesNotifications { get; set; } = true;
        [Required] public bool IncludedJournalNotifications { get; set; } = true;
        [Required] public bool IncludedRaportichkaNotifications { get; set; } = true;
        [Required] public bool IncludedTechnicalSupportNotifications { get; set; } = true;
    }
}
