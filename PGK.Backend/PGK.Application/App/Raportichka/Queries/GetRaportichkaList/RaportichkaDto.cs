using AutoMapper;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Queries.GetRaportichkaList
{
    public class RaportichkaDto : IMapWith<Domain.Raportichka.Raportichka>
    {

        [Key] public int Id { get; set; }
        [Required] public GroupDetails Group { get; set; }
        [Required] public DateTime Date { get; set; }

        public int RowsCount
        {
            get
            {
                return Rows.Count;
            }
        }
        [Required] public virtual List<RaportichkaRowDto> Rows { get; set; } = new List<RaportichkaRowDto>();
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Raportichka.Raportichka, RaportichkaDto>();
        }
    }
}
