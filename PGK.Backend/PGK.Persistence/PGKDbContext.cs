using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;
using PGK.Domain.Department;
using PGK.Domain.Journal;
using PGK.Domain.Language;
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
using PGK.Domain.User.Director;
using PGK.Domain.User.EducationalSector;
using PGK.Domain.User.Headman;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;
using PGK.Domain.Vedomost;
using PGK.Persistence.EntityTypeConfiguration;

namespace PGK.Persistence
{
    public class PGKDbContext : DbContext, IPGKDbContext
    {
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<DeputyHeadmaUser> DeputyHeadmaUsers { get; set; }
        public DbSet<EducationalSectorUser> EducationalSectorUsers { get; set; }
        public DbSet<HeadmanUser> HeadmanUsers { get; set; }
        public DbSet<StudentUser> StudentsUsers { get; set; }
        public DbSet<TeacherUser> TeacherUsers { get; set; }
        public DbSet<DepartmentHeadUser> DepartmentHeadUsers { get; set; }
        public DbSet<DirectorUser> DirectorUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Domain.Group.Group> Groups { get; set; }
        
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleDepartment> ScheduleDepartments { get; set; }
        public DbSet<ScheduleColumn> ScheduleColumns { get; set; }
        public DbSet<ScheduleRow> ScheduleRows { get; set; }

        public DbSet<Raportichka> Raportichkas { get; set; }
        public DbSet<RaportichkaRow> RaportichkaRows { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalSubject> JournalSubjects { get; set; }
        public DbSet<JournalSubjectColumn> JournalSubjectColumns { get; set; }
        public DbSet<JournalSubjectRow> JournalSubjectRows { get; set; }
        public DbSet<JournalTopic> JournalTopics { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Speciality> Specialties { get; set; }

        public DbSet<Vedomost> Vedomost { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageContent> MessageContents { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Language> Languages { get; set; }

        public PGKDbContext(DbContextOptions<PGKDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            builder.Entity<TeacherUser>()
                .HasMany(u => u.Subjects).WithMany(u => u.Teachers);

            builder.Entity<User>()
                .HasMany(u => u.Notifications).WithMany(u => u.Users);

            base.OnModelCreating(builder);
        }
    }
}
