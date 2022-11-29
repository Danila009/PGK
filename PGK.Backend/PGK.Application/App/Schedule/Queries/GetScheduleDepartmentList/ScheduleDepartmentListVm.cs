using PGK.Application.Common.Paged;

namespace PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList
{
    public class ScheduleDepartmentListVm : PagedResult<ScheduleDepartmentDto>
    {
        public override PagedList<ScheduleDepartmentDto> Results { get; set; }
    }
}
