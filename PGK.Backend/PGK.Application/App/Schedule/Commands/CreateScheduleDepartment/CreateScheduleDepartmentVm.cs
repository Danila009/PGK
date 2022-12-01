using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleDepartment
{
    public class CreateScheduleDepartmentVm
    {
        [Required] public int ScheduleDepartmentId { get; set; }
    }
}
