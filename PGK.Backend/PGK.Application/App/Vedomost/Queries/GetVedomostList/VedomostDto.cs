using AutoMapper;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostList
{
    public class VedomostDto : IMapWith<Domain.Vedomost.Vedomost>
    {
        [Key] public int Id { get; set; }
        [Required] public string Url { get; set; } = string.Empty;

        [Required] public DateTime Date { get; set; }

        [Required] public GroupDetails Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Vedomost.Vedomost, VedomostDto>();
        }
    }
}
