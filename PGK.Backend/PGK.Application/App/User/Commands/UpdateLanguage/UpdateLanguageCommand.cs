using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;

namespace PGK.Application.App.User.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest<UserSettingsDto>
    {
        public int LanguageId { get; set; }
        public int UserId { get; set; }
    }
}
