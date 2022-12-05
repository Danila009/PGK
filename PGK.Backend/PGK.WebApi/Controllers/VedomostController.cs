using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Vedomost.Queries.GetVedomostFile;
using PGK.Application.App.Vedomost.Queries.GetVedomostList;

namespace PGK.WebApi.Controllers
{
    public class VedomostController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<VedomostListVm>> GetAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetVedomostListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("File/{fileId}")]
        public async Task<ActionResult> GetFile(string fileId)
        {
            var query = new GetVedomostFileQuery
            {
                FileId = fileId
            };

            var file = await Mediator.Send(query);

            return File(file, "multipart/form-data");
        }
    }
}
