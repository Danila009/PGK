using MediatR;

namespace PGK.Application.App.Schedule.Commands.DeleteScheduleRow
{
    public class DeleteScheduleRowCommand : IRequest
    {
        public int Id { get; set; }
    }
}
