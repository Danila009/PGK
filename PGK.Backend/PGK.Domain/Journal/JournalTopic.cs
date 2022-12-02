using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Journal
{
    public class JournalTopic
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        public string? HomeWork { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public DateTime Date { get; set; }

        [Required] public JournalSubject JournalSubject { get; set; }
    }
}
