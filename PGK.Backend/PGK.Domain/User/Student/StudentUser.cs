using PGK.Domain.Journal;
using PGK.Domain.Raportichka;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.Student
{
    [Table("StudentUsers")]
    public class StudentUser : User
    {
        public override UserRole Role => UserRole.STUDENT;

        [Required] public Group.Group Group { get; set; }

        public virtual List<RaportichkaRow> RaportichkaRows { get; set; } = new List<RaportichkaRow>();

        public virtual List<JournalSubjectRow> JournalSubjectRows { get; set; } = new List<JournalSubjectRow>();
    }
}
