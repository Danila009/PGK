using PGK.Application.Common.Paged;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectRowList
{
    public class JournalSubjectRowListVm : PagedResult<JournalSubjectRowDto>
    {
        public override PagedList<JournalSubjectRowDto> Results { get; set; }
    }
}
