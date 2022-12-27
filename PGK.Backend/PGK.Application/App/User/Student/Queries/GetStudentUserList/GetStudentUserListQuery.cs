using MediatR;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class GetStudentUserListQuery : IRequest<StudentUserListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Search { get; set; } = null;
        public List<int> SpecialityIds { get; set; } = new List<int>();
        public List<int> DepartmenIds { get; set; } = new List<int>();

        public List<int> GroupIds { get; set; } = new List<int>();
    }
}
