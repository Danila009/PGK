using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Domain.User.Enums;

namespace PGK.Application.App.User.Commands.UpdateThemeFontSize
{
    public class UpdateThemeFontSizeCommand : IRequest<UserSettingsDto>
    {
        public ThemeFontSize ThemeFontSize { get; set; }

        public int UserId { get; set; }
    }
}
