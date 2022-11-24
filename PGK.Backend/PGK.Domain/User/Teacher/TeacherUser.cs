using PGK.Domain.Raportichka;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.Teacher
{
    [Table("TeacherUsers")]
    public class TeacherUser : User
    {
        public override UserRole Role => UserRole.TEACHER;

        //Классный руководитель
        public virtual List<Group.Group> Groups { get; set; } = new List<Group.Group>();

        public virtual List<RaportichkaRow> RaportichkaRows { get; set; } = new List<RaportichkaRow>();

        public virtual List<Subject.Subject> Subjects { get; set; } = new List<Subject.Subject>();
    }
}
