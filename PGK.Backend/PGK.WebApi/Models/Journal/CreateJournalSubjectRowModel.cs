using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Journal
{
    public class CreateJournalSubjectRowModel
    {
        [Required] public int StudentId { get; set; }
    }
}
