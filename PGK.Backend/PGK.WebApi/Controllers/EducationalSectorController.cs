using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.EducationalSector.Commands.Registration;

namespace PGK.WebApi.Controllers
{
    public class EducationalSectorController : Controller
    {
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationEducationalSectorVm>> Registration(
            RegistrationEducationalSectorCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
