using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Admin.Commands.Registration;

namespace PGK.WebApi.Controllers
{
    public class AdminController : Controller
    {

        [Authorize(Roles = "ADMIN")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationAdminVm>> Registration(
            RegistrationAdminCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
