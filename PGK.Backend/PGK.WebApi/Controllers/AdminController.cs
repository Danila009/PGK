using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Admin.Commands.DeleteAdmin;
using PGK.Application.App.User.Admin.Commands.Registration;
using PGK.Application.App.User.Admin.Queries.GetAdminDetails;
using PGK.Application.App.User.Admin.Queries.GetAdminList;
using PGK.Application.App.Vedomost.Commands.CreateVedomost;
using PGK.Application.App.Vedomost.Commands.DeleteVedomost;

namespace PGK.WebApi.Controllers
{
    public class AdminController : Controller
    {

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<AdminListVm>> GetAll(
            [FromQuery] GetAdminListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetById(
            int id
            )
        {
            var query = new GetAdminDetailsQuery
            {
                AdminId = id
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(
            int id
            )
        {
            var command = new DeleteAdminCommand
            {
                AdminId = id
            };

            await Mediator.Send(command);

            return Ok();
        }

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
