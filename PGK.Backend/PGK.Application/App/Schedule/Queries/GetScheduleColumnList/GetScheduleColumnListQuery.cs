using MediatR;

namespace PGK.Application.App.Schedule.Queries.GetScheduleColumnList
{
    public class GetScheduleColumnListQuery : IRequest<ScheduleColumnListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
