using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using MediatR;

namespace PGK.Application.App.Group.Queries.GetClassroomTeacher
{
    public class GetClassroomTeacherQuery : IRequest<TeacherUserDetails>
    {
        public int GroupId { get; set; }
    }
}
