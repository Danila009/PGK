using AutoMapper;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Queries.GetUserLits
{
    public class UserDto : IMapWith<Domain.User.User>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User.User, UserDto>();
        }
    }
}
