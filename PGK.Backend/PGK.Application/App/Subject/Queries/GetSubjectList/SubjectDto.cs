using AutoMapper;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Subject.Queries.GetSubjectList
{
    public class SubjectDto : IMapWith<Domain.Subject.Subject>
    {
        [Key] public int Id { get; set; }
        [Required] public string SubjectTitle { get; set; } = string.Empty;
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Subject.Subject, SubjectDto>();
        }
    }
}
