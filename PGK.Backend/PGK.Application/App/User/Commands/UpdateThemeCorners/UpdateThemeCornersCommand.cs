using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Domain.User.Enums;

namespace PGK.Application.App.User.Commands.UpdateThemeCorners
{
    public class UpdateThemeCornersCommand : IRequest<UserSettingsDto>
    {
        public ThemeCorners ThemeCorners { get; set; }
        public int UserId { get; set; }
    }
}
