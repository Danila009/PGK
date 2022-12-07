using MediatR;
using PGK.Application.App.Subject.Queries.GetSubjectList;

namespace PGK.Application.App.Subject.Commands.UpdateSubject
{
    public class UpdateSubjectCommand : IRequest<SubjectDto>
    {
        public int Id { get; set; }

        public string SubjectTitle { get; set; } = string.Empty;
    }
}
