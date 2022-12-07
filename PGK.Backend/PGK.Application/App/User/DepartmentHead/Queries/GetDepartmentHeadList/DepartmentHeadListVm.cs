using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList
{
    public class DepartmentHeadListVm : PagedResult<DepartmentHeadDto>
    {
        public override PagedList<DepartmentHeadDto> Results { get; set; }
    }
}
