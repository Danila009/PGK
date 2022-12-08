using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubjectRow
{
    public class CreateJournalSubjectRowCommand : IRequest<JournalSubjectRowDto>
    {
        public int StudentId { get; set; }

        public int JournalSubjectId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
