using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.EducationalSector.Commands.DeleteEducationalSector;
using PGK.Application.App.User.EducationalSector.Commands.Registration;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorDetails;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList;

namespace PGK.WebApi.Controllers
{
    public class EducationalSectorController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<EducationalSectorListVm>> GetAll(
            [FromQuery] GetEducationalSectorListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalSectorDto>> GetById(int id)
        {
            var query = new GetEducationalSectorDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var command = new DeleteEducationalSectorCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }

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
