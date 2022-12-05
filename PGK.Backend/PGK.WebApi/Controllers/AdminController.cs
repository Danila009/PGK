using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Admin.Commands.Registration;
using PGK.Application.App.Vedomost.Commands.CreateVedomost;
using PGK.Application.App.Vedomost.Commands.DeleteVedomost;

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

        [Authorize(Roles = "ADMIN")]
        [HttpPost("Vedomost")]
        public async Task<ActionResult<CreateVedomostVm>> CreateStatement(
            DateTime date,int groupId, IFormFile file)
        {
            var command = new CreateVedomostCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                File = file,
                Date = date,
                GroupId = groupId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}/Vedomost")]
        public async Task<ActionResult> DeleteVedomost(
            int id)
        {
            var command = new DeleteVedomostCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                VedomostId = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
