using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.Teacher.Queries.GetTeacherUserList
{
    public class TeacherUserListVm : PagedResult<TeacherUserDetails>
    {
        public override PagedList<TeacherUserDetails> Results { get; set; }
    }
}
