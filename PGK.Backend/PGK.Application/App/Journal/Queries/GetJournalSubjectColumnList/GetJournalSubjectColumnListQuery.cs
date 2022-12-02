using MediatR;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList
{
    public class GetJournalSubjectColumnListQuery : IRequest<JournalSubjectColumnListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
