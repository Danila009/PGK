using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.User.Admin.Queries.GetAdminList
{
    public class GetAdminListQuery : IRequest<AdminListVm>
    {
        [FromQuery(Name = "search")] public string? Search { get; set; }

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
