using MediatR;

namespace PGK.Application.App.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
