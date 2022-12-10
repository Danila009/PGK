using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.DeleteJournalSubject
{
    public class DeleteJournalSubjectCommand : IRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
