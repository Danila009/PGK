using MediatR;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostList
{
    public class GetVedomostListQuery : IRequest<VedomostListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
