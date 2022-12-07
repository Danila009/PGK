using MediatR;
using PGK.Application.App.User.Admin.Queries.GetAdminList;

namespace PGK.Application.App.User.Admin.Queries.GetAdminDetails
{
    public class GetAdminDetailsQuery : IRequest<AdminDto>
    {
        public int AdminId { get; set; }
    }
}
