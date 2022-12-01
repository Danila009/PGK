using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleVm
    {
        [Required] public int ScheduleId { get; set; }
    }
}
