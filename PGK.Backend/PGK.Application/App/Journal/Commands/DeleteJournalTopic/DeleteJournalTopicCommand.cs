using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.DeleteJournalTopic
{
    public class DeleteJournalTopicCommand : IRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
