using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.TechnicalSupport.Queries.GetChatList
{
    public class GetChatListQuery : IRequest<ChatListVm>
    {
        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; }
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; }
    }
}
