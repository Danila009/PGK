using MediatR;

namespace PGK.Application.App.Schedule.GetScheduleList.Queries
{
    public class GetScheduleListQuery : IRequest<ScheduleListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
