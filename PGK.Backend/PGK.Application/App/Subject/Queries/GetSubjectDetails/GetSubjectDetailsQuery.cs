using MediatR;
using PGK.Application.App.Subject.Queries.GetSubjectList;

namespace PGK.Application.App.Subject.Queries.GetSubjectDetails
{
    public class GetSubjectDetailsQuery : IRequest<SubjectDto>
    {
        public int Id { get; set; }
    }
}
