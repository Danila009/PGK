using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Schedule
{
    public class UpdateScheduleDepartmentModel
    {
        public string? Text { get; set; } = string.Empty;
        [Required] public int DepartmentId { get; set; }
    }
}
