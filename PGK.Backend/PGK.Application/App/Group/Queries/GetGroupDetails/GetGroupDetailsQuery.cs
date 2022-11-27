using MediatR;

namespace PGK.Application.App.Group.Queries.GetGroupDetails
{
    public class GetGroupDetailsQuery : IRequest<GroupDetails>
    {
        public int GroupId { get; set; }
    }
}
