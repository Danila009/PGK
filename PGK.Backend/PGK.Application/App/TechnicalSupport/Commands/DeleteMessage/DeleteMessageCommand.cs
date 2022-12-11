using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.TechnicalSupport.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public int MessageId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
