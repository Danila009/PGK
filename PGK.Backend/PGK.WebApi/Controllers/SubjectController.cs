using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Subject.Commands.CreateSubject;
using PGK.Application.App.Subject.Commands.DeleteSubject;
using PGK.Application.App.Subject.Commands.UpdateSubject;
using PGK.Application.App.Subject.Queries.GetSubjectDetails;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.WebApi.Models.Subject;

namespace PGK.WebApi.Controllers
{
    public class SubjectController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<SubjectListVm>> GetAll(
            [FromQuery] GetSubjectListQuery query)
        {

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetById(int id)
        {
            var query = new GetSubjectDetailsQuery { Id = id };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<CreateSubjectVm>> Create(CreateSubjectCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SubjectDto>> Update(
            int id, UpdateSubjectModel model)
        {
            var command = new UpdateSubjectCommand
            {
                Id = id,
                SubjectTitle = model.SubjectTitle
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSubjectCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
