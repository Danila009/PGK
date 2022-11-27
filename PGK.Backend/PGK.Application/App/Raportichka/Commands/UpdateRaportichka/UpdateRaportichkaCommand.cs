using MediatR;

namespace PGK.Application.App.Raportichka.Commands.UpdateRaportichka
{
    public class UpdateRaportichkaCommand : IRequest
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
    }
}
