using MediatR;

namespace PGK.Application.App.Schedule.Commands.DeleteSchedule
{
    public class DeleteScheduleCommand : IRequest
    {
        public int Id { get; set; }
    }
}
