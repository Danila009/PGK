using AutoMapper;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Mappings;
using PGK.Domain.Raportichka;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList
{
    public class RaportichkaRowDto : IMapWith<RaportichkaRow>
    {
        [Key] public int Id { get; set; }
        [Required] public int NumberLesson { get; set; }
        [Required] public bool Confirmation { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public SubjectDto Subject { get; set; }
        [Required] public TeacherUserDetails Teacher { get; set; }
        [Required] public StudentDto Student { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RaportichkaRow, RaportichkaRowDto>();
        }
    }
}
