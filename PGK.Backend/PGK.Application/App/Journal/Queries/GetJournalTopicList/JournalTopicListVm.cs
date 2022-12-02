using PGK.Application.Common.Paged;

namespace PGK.Application.App.Journal.Queries.GetJournalTopicList
{
    public class JournalTopicListVm : PagedResult<JournalTopicDto>
    {
        public override PagedList<JournalTopicDto> Results { get; set; }
    }
}