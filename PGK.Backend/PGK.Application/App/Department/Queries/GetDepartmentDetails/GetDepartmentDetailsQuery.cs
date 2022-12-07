using MediatR;
using PGK.Application.App.Department.Queries.GetDepartmentList;

namespace PGK.Application.App.Department.Queries.GetDepartmentDetails
{
    public class GetDepartmentDetailsQuery : IRequest<DepartmentDto>
    {
        public int Id { get; set; }
    }
}
