using AutoMapper;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Mappings;
using PGK.Domain.User.DeputyHeadma;
using PGK.Domain.User.Headman;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Queries.GetGroupDetails
{
    public class GroupDetails : IMapWith<Domain.Group.Group>
    {
        [Key] public int Id { get; set; }
        [Required] public int Number { get; set; }
        [Required] public string Speciality { get; set; } = string.Empty;
        [Required] public string SpecialityAbbreviation { get; set; } = string.Empty;
        [Required] public TeacherUserDetails ClassroomTeacher { get; set; }
        //public HeadmanUser? Headman { get; set; } = null;
        //public DeputyHeadmaUser? DeputyHeadma { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Group.Group, GroupDetails>();
        }
    }
}
