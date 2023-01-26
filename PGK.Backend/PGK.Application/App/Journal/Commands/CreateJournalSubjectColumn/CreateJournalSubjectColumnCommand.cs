using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Domain.Journal;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubjectColumn
{
    public class CreateJournalSubjectColumnCommand : IRequest<JournalSubjectColumnDto>
    {
        public JournalEvaluation Evaluation { get; set; }
        public DateTime Date { get; set; }
        public int? JournalSubjectRowId { get; set; }
        public int? StudentId { get; set; }
        public int? JournalSubjectId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
