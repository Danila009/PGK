using MediatR;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;

namespace PGK.Application.App.Schedule.Commands.UpdateScheduleDepartment
{
    public class UpdateScheduleDepartmentCommand : IRequest<ScheduleDepartmentDto>
    {
        public int Id { get; set; }

        public string? Text { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
