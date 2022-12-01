using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleColumn
{
    public class CreateScheduleColumnCommand : IRequest<CreateScheduleColumnVm>
    {
        [Required] public string Time { get; set; } = string.Empty;
        [Required] public int GroupId { get; set; }
        [Required] public int ScheduleDepartmentId { get; set; }
    }
}
