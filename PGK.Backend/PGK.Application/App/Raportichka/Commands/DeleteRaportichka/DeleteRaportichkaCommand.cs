using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Commands.DeleteRaportichka
{
    public class DeleteRaportichkaCommand : IRequest
    {
        [Required] public int Id { get; set; }
    }
}
