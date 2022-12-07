using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Department
{
    public class UpdateDepartmentModel
    {
        [Required] public string Name { get; set; } = string.Empty;

        [Required] public int DepartmentHeadId { get; set; }
    }
}
