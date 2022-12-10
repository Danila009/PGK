using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Domain.Journal;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.UpdateJournalEvaluation
{
    public class UpdateJournalEvaluationCommand : IRequest<JournalSubjectColumnDto>
    {
        public int JournalColumnId { get; set; }

        public JournalEvaluation Evaluation { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
