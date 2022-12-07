using MediatR;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;

namespace PGK.Application.App.Speciality.Queries.GetSpecialityDetails
{
    public class GetSpecialityDetailsQuery : IRequest<SpecialityDto>
    {
        public int Id { get; set; }
    }
}
