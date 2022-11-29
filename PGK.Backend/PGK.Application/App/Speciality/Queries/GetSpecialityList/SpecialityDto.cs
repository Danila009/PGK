using AutoMapper;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Speciality.Queries.GetSpecialityList
{
    public class SpecialityDto : IMapWith<Domain.Speciality.Speciality>
    {
        [Key] public int Id { get; set; }
        [Required] public string Number { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string NameAbbreviation { get; set; } = string.Empty;
        [Required] public string Qualification { get; set; } = string.Empty;

        //[Required] public Department.Department Department { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Speciality.Speciality, SpecialityDto>();
        }
    }
}
