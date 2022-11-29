using PGK.Application.Common.Paged;

namespace PGK.Application.App.Schedule.GetScheduleList.Queries
{
    public class ScheduleListVm : PagedResult<ScheduleDto>
    {
        public override PagedList<ScheduleDto> Results { get; set; }
    }
}
