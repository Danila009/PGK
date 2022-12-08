using MediatR;

namespace PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails
{
    public class GetTeacherUserDetailsQuery : IRequest<TeacherUserDetails>
    {
        public int Id { get; set; }
    }
}
