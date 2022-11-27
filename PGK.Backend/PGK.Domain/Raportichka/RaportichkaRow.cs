using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Raportichka
{
    public class RaportichkaRow
    {
        [Key] public int Id { get; set; }
        [Required] public int NumberLesson { get; set; }
        [Required] public bool Confirmation { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public Subject.Subject Subject { get; set; }
        [Required] public TeacherUser Teacher { get; set; }
        [Required] public StudentUser Student { get; set; }
        [Required] public Raportichka Raportichka { get; set; }
    }
}
