using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Queries.GetUserSettings
{
    public class UserSettingsDto : IMapWith<Domain.User.User>
    {
        [Required] public bool DrarkMode { get; set; }
        [Required] public SecondaryBackground SecondaryBackground { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User.User, UserSettingsDto>();
        }
    }
}
