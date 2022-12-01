using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Commands.FileCreateSchedule
{
    public class FileCreateScheduleCommand : IRequest
    {
        [Required] public IFormFile File { get; set; }
    }
}
