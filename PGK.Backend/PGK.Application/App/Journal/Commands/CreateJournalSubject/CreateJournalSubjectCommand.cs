using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubject
{
    public class CreateJournalSubjectCommand : IRequest<JournalSubjectDto>
    {
        public int Hours { get; set; }
        public int SubjectId { get; set; }
        public int? TeacherId { get; set; }
        public int JournalId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
