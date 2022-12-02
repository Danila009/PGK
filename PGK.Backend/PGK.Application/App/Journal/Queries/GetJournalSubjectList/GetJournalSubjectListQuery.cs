using MediatR;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectList
{
    public class GetJournalSubjectListQuery : IRequest<JournalSubjectListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
