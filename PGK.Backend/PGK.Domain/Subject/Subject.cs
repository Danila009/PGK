using PGK.Domain.Journal;
using PGK.Domain.User.Teacher;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Subject
{
    public class Subject
    {
        [Key] public int Id { get; set; }
        [Required] public string SubjectTitle { get; set; } = string.Empty;

        public virtual List<TeacherUser> Teachers { get; set; } = new List<TeacherUser>();
        public virtual List<JournalSubject> JournalSubjects { get; set; } = new List<JournalSubject>();
    }
}
