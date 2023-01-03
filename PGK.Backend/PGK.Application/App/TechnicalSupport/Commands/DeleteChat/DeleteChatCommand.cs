using MediatR;

namespace PGK.Application.App.TechnicalSupport.Commands.DeleteChat
{
    public class DeleteChatCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
