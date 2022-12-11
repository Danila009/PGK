using MediatR;

namespace PGK.Application.App.User.Commands.UpdateTelegramId
{
    public class UpdateTelegramIdCommand : IRequest
    {
        public int? TelegramId { get; set; }
        public int UserId { get; set; } 
    }
}
