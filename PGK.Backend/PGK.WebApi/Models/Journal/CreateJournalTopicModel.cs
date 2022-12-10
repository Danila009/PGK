using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Journal
{
    public class CreateJournalTopicModel
    {
        [Required] public string Title { get; set; } = string.Empty;
        public string? HomeWork { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}
