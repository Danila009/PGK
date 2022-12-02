using MediatR;

namespace PGK.Application.App.Journal.Queries.GetJournalList
{
    public class GetJournalListQuery : IRequest<JournalListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
