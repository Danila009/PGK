using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Student.Commands.Registration;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;

namespace PGK.WebApi.Controllers
{
    public class StudentController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<StudentUserListVm>> GetAll(
            string? search,[FromQuery] List<string> userRoles,
            [FromQuery] List<int> groupIds,
            int pageNumber = 1, int pageSize = 20)
        {
            var query = new GetStudentUserListQuery {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search,
                UserRoles = userRoles,
                GroupIds = groupIds
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }


        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationStudentVm>> Registration(
            RegistrationStudentCommand command)
        {
           var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
