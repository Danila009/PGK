using MediatR;

namespace PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList
{
    public class GetScheduleDepartmentListQuery 
        :IRequest<ScheduleDepartmentListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
