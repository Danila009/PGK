using PGK.Application.Common.Paged;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageList
{
    public class MessageListVm : PagedResult<MessageDto>
    {
        public override PagedList<MessageDto> Results { get; set; }
    }
}
