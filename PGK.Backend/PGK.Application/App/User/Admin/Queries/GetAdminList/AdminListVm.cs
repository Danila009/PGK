
using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.Admin.Queries.GetAdminList
{
    public class AdminListVm : PagedResult<AdminDto>
    {
        public override PagedList<AdminDto> Results { get; set; }
    }
}
