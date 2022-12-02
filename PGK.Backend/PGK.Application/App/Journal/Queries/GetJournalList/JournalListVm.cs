using PGK.Application.Common.Paged;

namespace PGK.Application.App.Journal.Queries.GetJournalList
{
    public class JournalListVm : PagedResult<JournalDto>
    {
        public override PagedList<JournalDto> Results { get; set; }
    }
}
