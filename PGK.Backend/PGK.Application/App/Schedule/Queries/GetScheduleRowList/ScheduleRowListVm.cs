using PGK.Application.Common.Paged;

namespace PGK.Application.App.Schedule.Queries.GetScheduleRowList
{
    public class ScheduleRowListVm : PagedResult<ScheduleRowDto>
    {
        public override PagedList<ScheduleRowDto> Results { get; set; }
    }
}
