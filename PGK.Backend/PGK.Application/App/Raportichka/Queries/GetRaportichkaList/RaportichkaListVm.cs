using PGK.Application.Common.Paged;

namespace PGK.Application.App.Raportichka.Queries.GetRaportichkaList
{
    public class RaportichkaListVm : PagedResult<RaportichkaDto>
    {
        public override PagedList<RaportichkaDto> Results { get; set; }
    }
}
