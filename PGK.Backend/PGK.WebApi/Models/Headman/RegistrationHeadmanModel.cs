using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Headman
{
    public class RegistrationHeadmanModel
    {
        [Required] public int StudentId { get; set; }
    }
}
