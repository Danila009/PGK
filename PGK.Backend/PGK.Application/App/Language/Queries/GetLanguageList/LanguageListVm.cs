using PGK.Application.Common.Paged;

namespace PGK.Application.App.Language.Queries.GetLanguageList
{
    public class LanguageListVm : PagedResult<LanguageDto>
    {
        public override PagedList<LanguageDto> Results { get; set; }
    }
}
