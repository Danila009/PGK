using PGK.Application.App.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Search.Enums;

namespace PGK.WebApi.Controllers
{
    public class SearchController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<SearchVm>> Search(
            string search, SearchType type = SearchType.ALL,
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new SearchQuery
            {
                Search = search,
                Type = type,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
