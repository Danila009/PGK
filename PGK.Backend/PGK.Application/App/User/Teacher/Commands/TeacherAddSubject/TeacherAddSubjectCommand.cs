using MediatR;

namespace PGK.Application.App.User.Teacher.Commands.TeacherAddSubject
{
    public class TeacherAddSubjectCommand : IRequest
    {
        public int TeacgerId { get; set; }
        public int SubjectId { get; set; }
    }
}
