using MediatR;

namespace PGK.Application.App.User.DepartmentHead.Commands.DeleteDepartmentHead
{
    public class DeleteDepartmentHeadCommand : IRequest
    {
        public int Id { get; set; }
    }
}
