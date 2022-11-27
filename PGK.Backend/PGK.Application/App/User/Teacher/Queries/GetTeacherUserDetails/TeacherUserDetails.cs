using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.User.Teacher;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails
{
    public class TeacherUserDetails : IMapWith<TeacherUser>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [MaxLength(128)] public string? MiddleName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TeacherUser, TeacherUserDetails>();
        }
    }
}
