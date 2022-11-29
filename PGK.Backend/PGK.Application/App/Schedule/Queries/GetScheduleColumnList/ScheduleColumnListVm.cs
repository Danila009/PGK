using PGK.Application.Common.Paged;

namespace PGK.Application.App.Schedule.Queries.GetScheduleColumnList
{
    public class ScheduleColumnListVm : PagedResult<ScheduleColumnDto>
    {
        public override PagedList<ScheduleColumnDto> Results { get; set; }
    }
}
