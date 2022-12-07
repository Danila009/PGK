using MediatR;

namespace PGK.Application.App.Subject.Commands.DeleteSubject
{
    public class DeleteSubjectCommand : IRequest
    {
        public int Id { get; set; }
    }
}
