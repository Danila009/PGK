using AutoMapper;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Schedule.GetScheduleList.Queries
{
    public class ScheduleDto : IMapWith<Domain.Schedules.Schedule>
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime Date { get; set; }

        public virtual List<ScheduleDepartmentDto> ScheduleDepartments { get; set; } = new List<ScheduleDepartmentDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Schedules.Schedule, ScheduleDto>();
        }
    }
}
