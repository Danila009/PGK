﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList
{
    public class GetDepartmentHeadListQuery : IRequest<DepartmentHeadListVm>
    {
        [FromQuery(Name = "search")] public string? Search { get; set; }

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
