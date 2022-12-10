using MediatR;
using PGK.Application.App.Vedomost.Queries.GetVedomostList;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostDetails
{
    public class GetVedomostDetailsQuery : IRequest<VedomostDto>
    {
        public int Id { get; set; }
    }
}
