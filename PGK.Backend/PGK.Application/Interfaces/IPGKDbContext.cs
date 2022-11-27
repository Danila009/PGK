using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PGK.Domain.Journal;
using PGK.Domain.Raportichka;
using PGK.Domain.Schedules;
using PGK.Domain.Subject;
using PGK.Domain.User;
using PGK.Domain.User.Admin;
using PGK.Domain.User.DeputyHeadma;
using PGK.Domain.User.EducationalSector;
using PGK.Domain.User.Headman;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;
using System.Text.RegularExpressions;

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
        DbSet<User> Users { get; set; }

        DbSet<Schedules> Schedules { get; set; }

        DbSet<Raportichka> Raportichkas { get; set; }
        DbSet<RaportichkaRow> RaportichkaRows { get; set; }

        DbSet<Domain.Group.Group> Groups { get; set; }

        DbSet<Subject> Subjects { get; set; }

        DbSet<Journal> Journals { get; set; }
        DbSet<JournalSubject> JournalSubjects { get; set; }
        DbSet<JournalSubjectColumn> JournalSubjectColumns { get; set; }
        DbSet<JournalSubjectRow> JournalSubjectRows { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
}
