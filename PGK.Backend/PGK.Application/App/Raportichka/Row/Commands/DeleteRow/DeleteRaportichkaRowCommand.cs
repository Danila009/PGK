using MediatR;

namespace PGK.Application.App.Raportichka.Row.Commands.DeleteRow
{
    public class DeleteRaportichkaRowCommand : IRequest
    {
        public int Id { get; set; }
    }
}
