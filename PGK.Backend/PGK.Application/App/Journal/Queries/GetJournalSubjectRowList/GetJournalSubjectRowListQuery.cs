using MediatR;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectRowList
{
    public class GetJournalSubjectRowListQuery : IRequest<JournalSubjectRowListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
