using AutoMapper;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;
using PGK.Application.Common.Mappings;
using PGK.Domain.Schedules;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Queries.GetScheduleColumnList
{
    public class ScheduleColumnDto : IMapWith<ScheduleColumn>
    {
        [Key] public int Id { get; set; }
        [Required] public string Text { get; set; } = string.Empty;
        [Required] public string Time { get; set; } = string.Empty;
        public GroupDetails? Group { get; set; }

        [Required] public ScheduleDepartmentDto ScheduleDepartment { get; set; }

        public virtual List<ScheduleRow> Rows { get; set; } = new List<ScheduleRow>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ScheduleColumn, ScheduleColumnDto>();
        }
    }
}
