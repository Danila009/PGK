using PGK.Domain.User.Student;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PGK.Application.App.Group.Queries.GetGroupDetails;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class StudentDto : IMapWith<StudentUser>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [MaxLength(128)] public string? MiddleName { get; set; }
        [Required] public string Role { get; set; } = string.Empty;

        [Required] public GroupDetails Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudentUser, StudentDto>();
        }
    }
}
