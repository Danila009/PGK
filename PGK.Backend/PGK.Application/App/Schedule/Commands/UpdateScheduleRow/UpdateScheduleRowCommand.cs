using MediatR;
using PGK.Application.App.Schedule.Queries.GetScheduleRowList;

namespace PGK.Application.App.Schedule.Commands.UpdateScheduleRow
{
    public class UpdateScheduleRowCommand : IRequest<ScheduleRowDto>
    {
        public int Id { get; set; }
        
        public string? Text { get; set; }
        public int TeacherId { get; set; }
    }
}
