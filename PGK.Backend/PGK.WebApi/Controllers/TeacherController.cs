using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Teacher.Commands.Registration;
using PGK.Application.App.User.Teacher.Commands.TeacherAddSubject;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserList;

namespace PGK.WebApi.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TeacherUserListVm>> GetAll(
            string? search,
            int pageNumber = 1, 
            int pageSize = 20)
        {
            var query = new GetTeacherUserListQuery
            {
                Search = search,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationTeacherVm>> Registration(
            RegistrationTeacherCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("{id}/Subject")]
        public async Task<ActionResult> AddSubject(int id, int subjectId)
        {
            var command = new TeacherAddSubjectCommand
            {
                TeacgerId = id,
                SubjectId = subjectId
            };

            await Mediator.Send(command);

            return Ok("Successfully");
        }

    }
}
