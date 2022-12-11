using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGK.Domain.TechnicalSupport.Enums;
using System.ComponentModel;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList
{
    public class GetMessageContentListQuery : IRequest<MessageContentListVm>
    {
        [FromQuery(Name = "type")] public MessageContentType? Type { get; set; }
        [FromQuery(Name = "chatId")] public int? ChatId { get; set; }
        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
