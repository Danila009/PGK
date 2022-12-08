using MediatR;

namespace PGK.Application.App.Schedule.Commands.DeleteScheduleDepartment
{
    public class DeleteScheduleDepartmentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
