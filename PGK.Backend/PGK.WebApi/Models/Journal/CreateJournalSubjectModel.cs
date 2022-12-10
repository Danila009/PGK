using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Journal
{
    public class CreateJournalSubjectModel
    {
        [Required] public int Hours { get; set; }
        [Required] public int SubjectId { get; set; }
    }
}
