using MediatR;

namespace PGK.Application.App.Group.Queries.GetGroupList
{
    public class GetGroupListQuery : IRequest<GroupListVm>
    {
        public string? Search { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
