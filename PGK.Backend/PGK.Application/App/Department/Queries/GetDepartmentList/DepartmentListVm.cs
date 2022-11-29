using PGK.Application.Common.Paged;

namespace PGK.Application.App.Department.Queries.GetDepartmentList
{
    public class DepartmentListVm : PagedResult<DepartmentDto>
    {
        public override PagedList<DepartmentDto> Results { get; set; }
    }
}
