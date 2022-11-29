using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Department.Queries.GetDepartmentList;

namespace PGK.WebApi.Controllers
{
    public class DepartmentController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<DepartmentListVm>> GetAll(
            string? search, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetDepartmentListQuery
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
