using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Journal
{
    public class CreateJournalModel
    {
        [Required] public int Course { get; set; }
        [Required]public int Semester { get; set; }
        [Required] public int GroupId { get; set; }

    }
}
