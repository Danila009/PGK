using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleRow
{
    public class CreateScheduleRowCommand : IRequest<CreateScheduleRowVm>
    {
        [Required] public int TeacherId { get; set; }

        [Required] public int ScheduleColumnId { get; set; }
    }
}
