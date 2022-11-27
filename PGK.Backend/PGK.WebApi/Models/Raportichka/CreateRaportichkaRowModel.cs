using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Raportichka
{
    public class CreateRaportichkaRowModel
    {
        [Required] public int NumberLesson { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public int SubjectId { get; set; }
        [Required] public int StudentId { get; set; }
        [Required] public int TeacherId { get; set; }
    }
}
