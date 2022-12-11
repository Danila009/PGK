using PGK.Application.Common.Paged;

namespace PGK.Application.App.TechnicalSupport.Queries.GetChatList
{
    public class ChatListVm : PagedResult<ChatDto>
    {
        public override PagedList<ChatDto> Results { get; set; }
    }
}
