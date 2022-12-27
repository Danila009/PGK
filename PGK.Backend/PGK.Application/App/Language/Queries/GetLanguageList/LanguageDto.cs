using AutoMapper;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Language.Queries.GetLanguageList
{
    public class LanguageDto : IMapWith<Domain.Language.Language>
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string NameEn { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Language.Language, LanguageDto>();
        }
    }
}
