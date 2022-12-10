using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.Journal.Queries.GetJournalList
{
    public class GetJournalListQuery : IRequest<JournalListVm>
    {
        [FromQuery(Name = "course")] public List<int>? Courses { get; set; } = new List<int>();
        [FromQuery(Name = "semesters")] public List<int>? Semesters { get; set; } = new List<int>();
        [FromQuery(Name = "groupIds")] public List<int>? GroupIds { get; set; } = new List<int>();
        [FromQuery(Name = "specialityIds")] public List<int>? SpecialityIds { get; set; } = new List<int>();
        [FromQuery(Name = "departmentIds")] public List<int>? DepartmentIds { get; set; } = new List<int>();

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
