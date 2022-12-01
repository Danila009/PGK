using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleRow
{
    public class CreateScheduleRowVm
    {
        [Required] public int ScheduleRowId { get; set; }
    }
}
