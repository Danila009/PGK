using MediatR;

namespace PGK.Application.App.User.Teacher.Commands.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest
    {
        public int Id { get; set; }
    }
}
