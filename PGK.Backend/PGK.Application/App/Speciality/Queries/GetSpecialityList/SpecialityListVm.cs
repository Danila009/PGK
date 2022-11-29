using PGK.Application.Common.Paged;

namespace PGK.Application.App.Speciality.Queries.GetSpecialityList
{
    public class SpecialityListVm : PagedResult<SpecialityDto>
    {
        public override PagedList<SpecialityDto> Results { get; set; }
    }
}
