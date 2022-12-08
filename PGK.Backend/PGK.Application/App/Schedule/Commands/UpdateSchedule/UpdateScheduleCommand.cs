using MediatR;
using PGK.Application.App.Schedule.GetScheduleList.Queries;

namespace PGK.Application.App.Schedule.Commands.UpdateSchedule
{
    public class UpdateScheduleCommand : IRequest<ScheduleDto>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}
