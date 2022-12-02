using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Journal
{
    public class Journal
    {
        [Key] public int Id { get; set; }
        [Required] public int Course { get; set; }
        [Required] public int Semester { get; set; }
        [Required] public Group.Group Group { get; set; }
    
        public virtual List<JournalSubject> Subjects { get; set; } = new List<JournalSubject>();
    }
}
