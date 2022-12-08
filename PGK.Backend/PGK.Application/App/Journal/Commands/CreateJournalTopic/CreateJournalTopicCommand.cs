using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalTopicList;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.CreateJournalTopic
{
    public class CreateJournalTopicCommand : IRequest<JournalTopicDto>
    {
        public string Title { get; set; } = string.Empty;
        public string? HomeWork { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }

        public int JournalSubjectId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
