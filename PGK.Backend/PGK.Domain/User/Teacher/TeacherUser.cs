using PGK.Domain.Journal;
using PGK.Domain.Raportichka;
using PGK.Domain.Schedules;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.Teacher
{
    [Table("TeacherUsers")]
    public class TeacherUser : User
    {
        public override string Role => UserRole.TEACHER.ToString();

        //Классный руководитель
        public virtual List<Group.Group> Groups { get; set; } = new List<Group.Group>();

        public virtual List<RaportichkaRow> RaportichkaRows { get; set; } = new List<RaportichkaRow>();

        public virtual List<Subject.Subject> Subjects { get; set; } = new List<Subject.Subject>();

        public virtual List<ScheduleRow> ScheduleRows { get; set; } = new List<ScheduleRow>();

        public virtual List<JournalSubject> JournalSubjects { get; set; } = new List<JournalSubject>();
    }
}
