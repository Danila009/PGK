using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Headman.Commands.Registration;
using PGK.WebApi.Models.Headman;

namespace PGK.WebApi.Controllers
{
    public class HeadmanController : Controller
    {
        [Authorize(Roles = "TEACHER")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationHeadmanVm>> Registration(
            RegistrationHeadmanModel model)
        {
            var command = new RegistrationHeadmanCommand
            {
                StudentId = model.StudentId,
                TeacherId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
