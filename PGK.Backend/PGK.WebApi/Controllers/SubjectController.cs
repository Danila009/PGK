using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Subject.Commands.CreateSubject;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using System.Security.Claims;

namespace PGK.WebApi.Controllers
{
    public class SubjectController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<SubjectListVm>> GetAll(string? search)
        {
            var query = new GetSubjectListQuery { Search = search };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<CreateSubjectVm>> Create(CreateSubjectCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
