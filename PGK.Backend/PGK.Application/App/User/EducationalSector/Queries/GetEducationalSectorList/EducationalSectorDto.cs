using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.User.EducationalSector;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList
{
    public class EducationalSectorDto : IMapWith<EducationalSectorUser>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }
        public string? PhotoUrl { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EducationalSectorUser, EducationalSectorDto>();
        }
    }
}
