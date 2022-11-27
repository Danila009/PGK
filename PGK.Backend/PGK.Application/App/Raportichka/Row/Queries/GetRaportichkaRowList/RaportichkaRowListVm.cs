using PGK.Application.Common.Paged;

namespace PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList
{
    public class RaportichkaRowListVm : PagedResult<RaportichkaRowDto>
    {
        public override PagedList<RaportichkaRowDto> Results { get; set; }
    }
}
