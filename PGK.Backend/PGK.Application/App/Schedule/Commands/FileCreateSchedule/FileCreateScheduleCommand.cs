using MediatR;
using Microsoft.AspNetCore.Http;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.FileCreateSchedule
{
    public class FileCreateScheduleCommand : IRequest<ScheduleDto>
    {
        [Required] public IFormFile File { get; set; }
    }
}
