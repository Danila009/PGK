using AutoMapper;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Mappings;
using PGK.Domain.Schedules;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.Queries.GetScheduleRowList
{
    public class ScheduleRowDto : IMapWith<ScheduleRow>
    {
        [Key] public int Id { get; set; }
        [Required] public string Text { get; set; } = string.Empty;

        public TeacherUserDetails? Teacher { get; set; }
        [Required] public ScheduleColumnDto Column { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ScheduleRow, ScheduleRowDto>();
        }
    }
}
