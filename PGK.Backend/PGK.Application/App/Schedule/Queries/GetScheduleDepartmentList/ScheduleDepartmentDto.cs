using AutoMapper;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;
using PGK.Application.Common.Mappings;
using PGK.Domain.Schedules;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList
{
    public class ScheduleDepartmentDto : IMapWith<ScheduleDepartment>
    {
        [Key] public int Id { get; set; }
        [Required] public string? Text { get; set; }
        public DepartmentDto? Department { get; set; }

        [Required] public ScheduleDto Schedule { get; set; }

        public virtual List<ScheduleColumnDto> Columns { get; set; } = new List<ScheduleColumnDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ScheduleDepartment, ScheduleDepartmentDto>();
        }
    }
}
