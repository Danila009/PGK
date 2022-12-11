using MediatR;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using PGK.Domain.User;

namespace PGK.Application.App.TechnicalSupport.Commands.UpdatePin
{
    public class UpdateMessagePinCommand : IRequest<MessageDto>
    {

        public int MessageId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
