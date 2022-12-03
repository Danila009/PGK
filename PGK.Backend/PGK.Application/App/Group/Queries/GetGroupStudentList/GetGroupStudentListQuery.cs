using MediatR;

namespace PGK.Application.App.Group.Queries.GetGroupStudentList
{
    public class GetGroupStudentListQuery : IRequest<GroupStudentListVm>
    {
        public int GroupId { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
