using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Raportichka
{
    public class CreateRaportichkaModel
    {
        [Required] public int GroupId { get; set; }
    }
}
