using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Speciality
{
    public class Speciality
    {
        [Key] public int Id { get; set; }
        [Required] public string Number { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string NameAbbreviation { get; set; } = string.Empty;
        [Required] public string Qualification { get; set; } = string.Empty;

        [Required] public Department.Department Department { get; set; }
    }
}
