using MediatR;

namespace PGK.Application.App.Subject.Queries.GetSubjectList
{
    public class GetSubjectListQuery : IRequest<SubjectListVm>
    {
        public string? Search { get; set; } = null;
    }
}
