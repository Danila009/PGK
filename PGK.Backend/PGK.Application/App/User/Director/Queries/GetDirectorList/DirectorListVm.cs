using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.Director.Queries.GetDirectorList
{
    public class DirectorListVm : PagedResult<DirectorDto>
    {
        public override PagedList<DirectorDto> Results { get; set; }
    }
}
