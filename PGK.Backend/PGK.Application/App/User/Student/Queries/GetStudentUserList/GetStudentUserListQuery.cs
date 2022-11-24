using MediatR;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class GetStudentUserListQuery : IRequest<StudentUserListVm>
    {
        public string? Search { get; set; } = null;
    }
}
