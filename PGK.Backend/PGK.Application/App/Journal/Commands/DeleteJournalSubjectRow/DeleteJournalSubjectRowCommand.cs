using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.DeleteJournalSubjectRow
{
    public class DeleteJournalSubjectRowCommand : IRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
