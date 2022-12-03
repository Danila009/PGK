using PGK.Application.App.User.Student.Queries.GetStudentUserList;
using PGK.Application.Common.Paged;

namespace PGK.Application.App.Group.Queries.GetGroupStudentList
{
    public class GroupStudentListVm : PagedResult<StudentDto>
    {
        public override PagedList<StudentDto> Results { get; set; }
    }
}
