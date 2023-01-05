using MediatR;

namespace PGK.Application.App.User.Teacher.Queries.GetTeacherUserList
{
    public class GetTeacherUserListQuery : IRequest<TeacherUserListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Search { get; set; } = string.Empty;
        public List<int>? SubjectIds { get; set; } = new List<int>();
    }
}
