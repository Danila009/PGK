using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.Group.Queries.GetGroupList
{
    public class GetGroupListQuery : IRequest<GroupListVm>
    {
        [FromQuery(Name = "search")] public string? Search { get; set; }
        [FromQuery(Name = "course")] public List<int>? Courses { get; set; } = new List<int>();
        [FromQuery(Name = "number")] public List<int>? Number { get; set; } = new List<int>();
       
        [FromQuery(Name = "specialityIds")] public List<int>? SpecialityIds { get; set; } = new List<int>();
        [FromQuery(Name = "departmentIds")] public List<int>? DepartmentIds { get; set; } = new List<int>();
        [FromQuery(Name = "classroomTeacherIds")] public List<int>? ClassroomTeacherIds { get; set; } = new List<int>();
        [FromQuery(Name = "deputyHeadmaIds")] public List<int>? DeputyHeadmaIds { get; set; } = new List<int>();
        [FromQuery(Name = "headmanIds")] public List<int>? HeadmanIds { get; set; } = new List<int>();

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
