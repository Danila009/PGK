using PGK.Domain.User.Student;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Journal
{
    public class JournalSubjectRow
    {
        [Key] public int Id { get; set; }
        [Required] public StudentUser Student { get; set; }

        [Required] public JournalSubject JournalSubject { get; set; }

        public virtual List<JournalSubjectColumn> Columns { get; set; } = new List<JournalSubjectColumn>();
    }
}
