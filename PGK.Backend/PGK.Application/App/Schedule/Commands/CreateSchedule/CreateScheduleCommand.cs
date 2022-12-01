using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PGK.Application.App.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleCommand : IRequest<CreateScheduleVm>
    {
        [Required] public DateTime Date { get; set; }
    }
}
