using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Language.Queries.GetLanguageList;

namespace PGK.WebApi.Controllers
{
    public class LanguageController : Controller
    {

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LanguageListVm))]
        public async Task<ActionResult> GetLanguageAll(GetLanguageListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
