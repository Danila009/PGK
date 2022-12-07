using PGK.Application.Common.Paged;

namespace PGK.Application.App.Subject.Queries.GetSubjectList
{
    public class SubjectListVm : PagedResult<SubjectDto>
    {
     
        public override PagedList<SubjectDto> Results { get; set; }
    }
}
