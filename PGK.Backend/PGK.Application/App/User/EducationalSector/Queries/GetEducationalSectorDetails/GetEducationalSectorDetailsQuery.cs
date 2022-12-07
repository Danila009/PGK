using MediatR;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList;

namespace PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorDetails
{
    public class GetEducationalSectorDetailsQuery : IRequest<EducationalSectorDto>
    {
        public int Id { get; set; }
    }
}
