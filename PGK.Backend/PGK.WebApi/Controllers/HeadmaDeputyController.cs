using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.DeputyHeadma.Commands.Registration;

namespace PGK.WebApi.Controllers
{
    public class HeadmaDeputyController : Controller
    {

        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationDeputyHeadmaVm>> Registration(
            RegistrationDeputyHeadmaCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
