
using PGK.Application.App.Group.Queries.GetGroupDetails;

namespace PGK.Application.App.Group.Queries.GetGroupList
{
    public class GroupListVm
    {
        public int Count
        {
            get
            {
                return Groups.Count;
            }
        }

        public virtual List<GroupDetails> Groups { get; set; } = new List<GroupDetails>();
    }
}
