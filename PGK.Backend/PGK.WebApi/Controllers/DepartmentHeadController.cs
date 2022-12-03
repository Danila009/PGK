using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.DepartmentHead.Commands.Registration;

namespace PGK.WebApi.Controllers
{
    public class DepartmentHeadController : Controller
    {

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
