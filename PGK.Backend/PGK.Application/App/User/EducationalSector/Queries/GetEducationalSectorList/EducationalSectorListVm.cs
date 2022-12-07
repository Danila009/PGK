using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList
{
    public class EducationalSectorListVm : PagedResult<EducationalSectorDto>
    {
        public override PagedList<EducationalSectorDto> Results { get; set; }
    }
}
