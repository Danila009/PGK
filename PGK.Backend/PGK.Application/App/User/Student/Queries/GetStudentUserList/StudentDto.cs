using PGK.Domain.User.Student;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class StudentDto : IMapWith<StudentUser>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(64)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(64)] public string LastName { get; set; } = string.Empty;
        [MaxLength(64)] public string? MiddleName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudentUser, StudentDto>();
        }
    }
}
