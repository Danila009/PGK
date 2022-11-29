using MediatR;

namespace PGK.Application.App.Department.Queries.GetDepartmentList
{
    public class GetDepartmentListQuery : IRequest<DepartmentListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Search { get; set; } = string.Empty;
    }
}
