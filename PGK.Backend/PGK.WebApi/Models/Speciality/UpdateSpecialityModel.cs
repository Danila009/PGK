using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Speciality
{
    public class UpdateSpecialityModel
    {
        [Required] public string Number { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string NameAbbreviation { get; set; } = string.Empty;
        [Required] public string Qualification { get; set; } = string.Empty;
 
        [Required] public int DepartmentId { get; set; }
    }
}
