using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;

namespace PGK.Application.App.User.Commands.UpdateDrarkMode
{
    public class UpdateDrarkModeCommand : IRequest<UserSettingsDto>
    {
        public int UserId { get; set; }
    }
}
