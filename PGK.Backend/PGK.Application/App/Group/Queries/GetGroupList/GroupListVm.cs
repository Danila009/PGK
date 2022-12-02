
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.Common.Paged;

namespace PGK.Application.App.Group.Queries.GetGroupList
{
    public class GroupListVm : PagedResult<GroupDetails>
    {
        public override PagedList<GroupDetails> Results { get; set; }
    }
}
