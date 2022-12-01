using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleColumn
{
    public class CreateScheduleColumnVm
    {
        [Required] public int ScheduleColumnId { get; set; }
    }
}
