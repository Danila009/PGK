using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Teacher
{
    public class TeacherCreateRaportichkaRowModel
    {
        [Required] public int NumberLesson { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public int SubjectId { get; set; }
        [Required] public int StudentId { get; set; }
    }
}
