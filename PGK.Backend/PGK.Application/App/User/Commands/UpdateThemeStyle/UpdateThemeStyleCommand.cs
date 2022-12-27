using MediatR;
using PGK.Domain.User.Enums;

namespace PGK.Application.App.User.Commands.UpdateThemeStyle
{
    public class UpdateThemeStyleCommand : IRequest<UpdateThemeStyleVm>
    {
        public int UserId { get; set; }
        public ThemeStyle ThemeStyle { get; set; }
    }
}
