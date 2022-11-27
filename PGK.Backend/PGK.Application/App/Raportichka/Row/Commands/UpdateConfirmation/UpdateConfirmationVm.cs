using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateConfirmation
{
    public class UpdateConfirmationVm
    {
        [Required] public bool Confirmation { get; set; }
    }
}
