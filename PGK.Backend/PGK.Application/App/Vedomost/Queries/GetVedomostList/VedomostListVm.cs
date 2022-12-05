using PGK.Application.Common.Paged;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostList
{
    public class VedomostListVm : PagedResult<VedomostDto>
    {
        public override PagedList<VedomostDto> Results { get; set; }
    }
}
