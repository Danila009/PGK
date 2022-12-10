using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostList
{
    public class GetVedomostListQuery : IRequest<VedomostListVm>
    {
        [FromQuery(Name = "onlyDate")] public DateTime? OnlyDate { get; set; }
        [FromQuery(Name = "startDate")] public DateTime? StartDate { get; set; }
        [FromQuery(Name = "endDate")] public DateTime? EndDate { get; set; }

        [FromQuery(Name = "groupIds")] public List<int>? GroupIds { get; set; } = new List<int>();

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
