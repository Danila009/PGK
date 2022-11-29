using MediatR;

namespace PGK.Application.App.Schedule.Queries.GetScheduleRowList
{
    public class GetScheduleRowListQuery : IRequest<ScheduleRowListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
