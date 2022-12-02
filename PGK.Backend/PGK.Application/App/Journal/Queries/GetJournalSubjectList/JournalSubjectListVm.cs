using PGK.Application.Common.Paged;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectList
{
    public class JournalSubjectListVm : PagedResult<JournalSubjectDto>
    {
        public override PagedList<JournalSubjectDto> Results { get; set; }
    }
}
