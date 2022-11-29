using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;

namespace PGK.WebApi.Controllers
{
    public class SpecialityController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<SpecialityListVm>> GetAll(
            string? search, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetSpecialityListQuery
            {
                Search = search,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
