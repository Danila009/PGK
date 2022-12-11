using PGK.Application.Common.Paged;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList
{
    public class MessageContentListVm : PagedResult<MessageContentDto>
    {
        public override PagedList<MessageContentDto> Results { get; set; }
    }
}
