using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Speciality.Commands.CreateSpeciality;
using PGK.Application.App.Speciality.Commands.DeleteSpeciality;
using PGK.Application.App.Speciality.Commands.UpdateSpeciality;
using PGK.Application.App.Speciality.Queries.GetSpecialityDetails;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;

namespace PGK.WebApi.Controllers
{
    public class SpecialityController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<SpecialityListVm>> GetAll(
            string? search, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetSpecialityListQuery
            {
                Search = search,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialityDto>> GetById(int id)
        {
            var query = new GetSpecialityDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SpecialityDto>> Create(CreateSpecialityCommand command)
        {
            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<SpecialityDto>> Update(int id, UpdateSpecialityCommand command)
        {
            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSpecialityCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
