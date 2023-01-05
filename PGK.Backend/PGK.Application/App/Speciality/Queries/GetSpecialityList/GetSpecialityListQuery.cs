using MediatR;

namespace PGK.Application.App.Speciality.Queries.GetSpecialityList
{
    public class GetSpecialityListQuery : IRequest<SpecialityListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Search { get; set; } = string.Empty;
        public List<int>? DepartmentIds { get; set; }
    }
}
