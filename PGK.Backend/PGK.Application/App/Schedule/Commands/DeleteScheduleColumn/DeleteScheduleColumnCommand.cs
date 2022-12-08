using MediatR;

namespace PGK.Application.App.Schedule.Commands.DeleteScheduleColumn
{
    public class DeleteScheduleColumnCommand : IRequest
    {
        public int Id { get; set; }
    }
}
