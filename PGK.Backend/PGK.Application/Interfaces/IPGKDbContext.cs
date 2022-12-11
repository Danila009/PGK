using Microsoft.EntityFrameworkCore;
using PGK.Domain.Department;
using PGK.Domain.Journal;
using PGK.Domain.Notification;
using PGK.Domain.Raportichka;
using PGK.Domain.Schedules;
using PGK.Domain.Speciality;
using PGK.Domain.Subject;
using PGK.Domain.TechnicalSupport;
using PGK.Domain.User;
using PGK.Domain.User.Admin;
using PGK.Domain.User.DepartmentHead;
using PGK.Domain.User.DeputyHeadma;
using PGK.Domain.User.EducationalSector;
using PGK.Domain.User.Headman;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;
using PGK.Domain.Vedomost;

namespace PGK.Application.Interfaces
{
    public interface IPGKDbContext
    {
        DbSet<AdminUser> AdminUsers { get; set; }
        DbSet<DeputyHeadmaUser> DeputyHeadmaUsers { get; set; }
        DbSet<EducationalSectorUser> EducationalSectorUsers { get; set; }
        DbSet<HeadmanUser> HeadmanUsers { get; set; }
        DbSet<StudentUser> StudentsUsers { get; set; }
        DbSet<TeacherUser> TeacherUsers { get; set; }
        DbSet<DepartmentHeadUser> DepartmentHeadUsers { get; set; }
        DbSet<User> Users { get; set; }

        DbSet<Schedule> Schedules { get; set; }
        DbSet<ScheduleDepartment> ScheduleDepartments { get; set; }
        DbSet<ScheduleColumn> ScheduleColumns { get; set; }
        DbSet<ScheduleRow> ScheduleRows { get; set; }

        DbSet<Raportichka> Raportichkas { get; set; }
        DbSet<RaportichkaRow> RaportichkaRows { get; set; }

        DbSet<Domain.Group.Group> Groups { get; set; }

        DbSet<Subject> Subjects { get; set; }

        DbSet<Journal> Journals { get; set; }
        DbSet<JournalSubject> JournalSubjects { get; set; }
        DbSet<JournalSubjectColumn> JournalSubjectColumns { get; set; }
        DbSet<JournalSubjectRow> JournalSubjectRows { get; set; }
        DbSet<JournalTopic> JournalTopics { get; set; }

        DbSet<Department> Departments { get; set; }
        DbSet<Speciality> Specialties { get; set; }

        DbSet<Vedomost> Vedomost { get; set; }

        DbSet<Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<MessageContent> MessageContents { get; set; }

        DbSet<Notification> Notifications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
}
