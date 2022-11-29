using AutoMapper;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Queries.GetGroupDetails
{
    public class GroupDetails : IMapWith<Domain.Group.Group>
    {
        [Key] public int Id { get; set; }
        [Required] public int Number { get; set; }
        [Required] public SpecialityDto Speciality { get; set; }
        [Required] public DepartmentDto Department { get; set; }
        [Required] public TeacherUserDetails ClassroomTeacher { get; set; }
        //public StudentDto? Headman { get; set; } = null;
        //public StudentDto? DeputyHeadma { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Group.Group, GroupDetails>();
        }
    }
}
