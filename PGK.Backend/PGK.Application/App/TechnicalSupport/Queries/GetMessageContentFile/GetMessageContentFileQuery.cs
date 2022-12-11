using MediatR;
using PGK.Domain.TechnicalSupport.Enums;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageContentFile
{
    public class GetMessageContentFileQuery : IRequest<byte[]>
    {
        public string FileId { get; set; } = string.Empty;
        public MessageContentType Type { get; set; }
    }
}
