using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.TechnicalSupport.Commands.DeleteMessageContent
{
    public class DeleteMessageContentCommand : IRequest
    {
        public int MessageContentId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
