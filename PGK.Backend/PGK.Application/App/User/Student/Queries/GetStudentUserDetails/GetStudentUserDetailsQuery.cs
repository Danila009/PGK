using MediatR;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserDetails
{
    public class GetStudentUserDetailsQuery : IRequest<StudentDto>
    {
        public int Id { get; set; }
    }
}
