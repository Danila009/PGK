using MediatR;

namespace PGK.Application.App.User.Student.Commands.Delete
{
    public class DeleteStudentCommand : IRequest
    {
        public int StudentId { get; set; }
    }
}
