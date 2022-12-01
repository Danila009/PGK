using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleDepartment
{
    public class CreateScheduleDepartmentCommand : IRequest<CreateScheduleDepartmentVm>
    {
        [Required] public int DepartmentId { get; set; }

        [Required] public int ScheduleId { get; set; }
    }
}
