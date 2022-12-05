using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Vedomost.Commands.CreateVedomost
{
    public class CreateVedomostVm
    {
        [Required] public int StatementId { get; set; }
        [Required] public string StatementUrl { get; set; } = string.Empty;
    }
}
