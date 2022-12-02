using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PGK.WebApi.Controllers
{
    public class SearchController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Search(
            string search
            )
        {
            return Ok(search);
        }
    }
}
