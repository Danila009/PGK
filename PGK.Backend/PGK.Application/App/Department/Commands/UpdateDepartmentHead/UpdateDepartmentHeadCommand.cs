using MediatR;

namespace PGK.Application.App.Department.Commands.UpdateDepartmentHead
{
    public class UpdateDepartmentHeadCommand : IRequest
    {
        public int DepartmentId { get; set; }
        public int DepartmentHeadId { get; set; }
    }
}
