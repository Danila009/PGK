using PGK.Domain.Journal;
using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Journal
{
    public class CreateJournalSubjectColumnModel
    {
        public int? JournalSubjectRowId { get; set; }
        public int? StudentId { get; set; }
        public int? JournalSubjectId { get; set; }
        [Required] public JournalEvaluation Evaluation { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}
