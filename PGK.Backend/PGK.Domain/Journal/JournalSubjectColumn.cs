using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Journal
{
    public class JournalSubjectColumn
    {
        [Key] public int Id { get; set; }
        [Required] public JournalEvaluation Evaluation { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public JournalSubjectRow Row { get; set; }
    }
}
