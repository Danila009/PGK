using PGK.Domain.Journal;
using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Journal
{
    public class CreateJournalSubjectColumnModel
    {
        [Required] public JournalEvaluation Evaluation { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}
