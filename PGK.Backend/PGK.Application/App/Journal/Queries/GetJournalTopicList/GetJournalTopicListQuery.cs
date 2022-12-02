using MediatR;

namespace PGK.Application.App.Journal.Queries.GetJournalTopicList
{
    public class GetJournalTopicListQuery : IRequest<JournalTopicListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
