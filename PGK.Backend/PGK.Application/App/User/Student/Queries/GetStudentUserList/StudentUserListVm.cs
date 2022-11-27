using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class StudentUserListVm : PagedResult<StudentDto>
    {
        public override PagedList<StudentDto> Results { get; set; }
    }
}
