using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageList
{
    public class GetMessageListQuery : IRequest<MessageListVm>
    {
        [FromQuery(Name = "search")] public string? Search { get; set; }
        [FromQuery(Name = "pin")] public bool? Pin { get; set; }
        [FromQuery(Name = "userVisible")] public bool? UserVisible { get; set; }

        [FromQuery(Name = "onlyDate")] public DateTime? OnlyDate { get; set; }
        [FromQuery(Name = "startDate")] public DateTime? StartDate { get; set; }
        [FromQuery(Name = "endDate")] public DateTime? EndDate { get; set; }

        [FromQuery(Name = "userId")] public int? UserId { get; set; }
        [FromQuery(Name = "chatId")] public int? ChatId { get; set; }

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
