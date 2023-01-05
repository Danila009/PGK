using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.User.Director;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Director.Queries.GetDirectorList
{
    public class DirectorDto : IMapWith<DirectorUser>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }
        [Required] public bool Current { get; set; } = true;
        public string? Cabinet { get; set; }
        public string? Information { get; set; }
        public string? PhotoUrl { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DirectorUser, DirectorDto>();
        }
    }
}
