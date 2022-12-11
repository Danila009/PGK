using MediatR;
using Microsoft.AspNetCore.Http;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList;
using PGK.Domain.TechnicalSupport.Enums;

namespace PGK.Application.App.TechnicalSupport.Commands.CreateMessageContent
{
    public class CreateMessageContentCommand : IRequest<MessageContentDto>
    {
        public IFormFile File { get; set; }
        public MessageContentType Type { get; set; }

        public int MessageId { get; set; }
        public int UserId { get; set; }
    }
}
