using PGK.Domain.User.DeputyHeadma;
using PGK.Domain.User.Headman;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Group
{
    public class Group
    {
        [Key] public int Id { get; set; }
        //[Required] public int Course { get; set; }
        [Required] public int Number { get; set; }
        [Required] public Speciality.Speciality Speciality { get; set; }
        [Required] public TeacherUser ClassroomTeacher { get; set; }
        public HeadmanUser? Headman { get; set; } = null;
        public DeputyHeadmaUser? DeputyHeadma { get; set; } = null;

        [Required] public Department.Department Department { get; set; }

        public virtual List<StudentUser> Students { get; set; } = new List<StudentUser>();
        public virtual List<Raportichka.Raportichka> Raportichkas { get; set; } =
            new List<Raportichka.Raportichka>();

        public virtual List<Journal.Journal> Journals { get; set; } = new List<Journal.Journal>();
    }
}
