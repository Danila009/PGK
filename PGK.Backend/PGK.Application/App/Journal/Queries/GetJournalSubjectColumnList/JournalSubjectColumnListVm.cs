using PGK.Application.Common.Paged;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList
{
    public class JournalSubjectColumnListVm : PagedResult<JournalSubjectColumnDto>
    {
        public override PagedList<JournalSubjectColumnDto> Results { get; set; }
    }
}
