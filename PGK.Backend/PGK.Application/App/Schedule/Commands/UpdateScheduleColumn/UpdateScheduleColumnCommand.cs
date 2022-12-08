using MediatR;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;

namespace PGK.Application.App.Schedule.Commands.UpdateScheduleColumn
{
    public class UpdateScheduleColumnCommand : IRequest<ScheduleColumnDto>
    {
        public int Id { get; set; }

        public string? Text { get; set; }
        public string Time { get; set; } = string.Empty;
        public int GroupId { get; set; }
    }
}
