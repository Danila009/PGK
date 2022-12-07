using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.DepartmentHead.Commands.DeleteDepartmentHead;
using PGK.Application.App.User.DepartmentHead.Commands.Registration;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadDetails;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList;

namespace PGK.WebApi.Controllers
{
    public class DepartmentHeadController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<DepartmentHeadListVm>> GetAll(
            [FromQuery] GetDepartmentHeadListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentHeadDto>> GetById(int id)
        {
            var query = new GetDepartmentHeadDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentHeadCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }


        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationDepartmentHeadVm>> Registration(
            RegistrationDepartmentHeadCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

    }
}
