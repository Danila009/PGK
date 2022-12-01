using MediatR;

namespace PGK.Application.App.Schedule.GetScheduleList.Queries
{
    public class GetScheduleListQuery : IRequest<ScheduleListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DateTime? OnlyDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<int> DepartmentIds { get; set; } = new List<int>();
        public List<int> GroupIds { get; set; } = new List<int>();

    }
}
