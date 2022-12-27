using AutoMapper;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.Common.Mappings;
using PGK.Domain.User.Headman;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Headman.Queries.GetHeadmanList
{
    public class HeadmanDto : IMapWith<HeadmanUser>
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }
        public string? PhotoUrl { get; set; } = null;

        [Required] public GroupDetails Group { get; set; }

        [Required] public DepartmentDto Department { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HeadmanUser, HeadmanDto>();
        }
    }
}
