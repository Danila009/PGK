using MediatR;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList;

namespace PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadDetails
{
    public class GetDepartmentHeadDetailsQuery : IRequest<DepartmentHeadDto>
    {
        public int Id { get; set; }
    }
}
