using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Domain.User.Enums;

namespace PGK.Application.App.User.Commands.UpdateThemeFontStyle
{
    public class UpdateThemeFontStyleCommand : IRequest<UserSettingsDto>
    {
        public ThemeFontStyle ThemeFontStyle { get; set; }

        public int UserId { get; set; }
    }
}
