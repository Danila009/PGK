using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.Raportichka;
using PGK.Domain.Schedules;
using PGK.Domain.User;
using PGK.Domain.User.Admin;
using PGK.Domain.User.DeputyHeadma;
using PGK.Domain.User.EducationalSector;
using PGK.Domain.User.Headman;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;
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
        public DbSet<User> Users { get; set; }

        public DbSet<Domain.Group.Group> Groups { get; set; }
        public DbSet<Schedules> Schedules { get; set; }
        public DbSet<Raportichka> Raportichkas { get; set; }
        public DbSet<RaportichkaRow> RaportichkaRows { get; set; }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalSubject> JournalSubjects { get; set; }
        public DbSet<JournalSubjectColumn> JournalSubjectColumns { get; set; }
        public DbSet<JournalSubjectRow> JournalSubjectRows { get; set; }

        public PGKDbContext(DbContextOptions<PGKDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
