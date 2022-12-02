using MediatR;

namespace PGK.Application.App.User.Queries.GetUserSettings
{
    public class GetUserSettingsQuery : IRequest<UserSettingsDto>
    {
        public int UserId { get; set; }
    }
}
