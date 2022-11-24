using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;

namespace PGK.WebApi.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<StudentUserListVm>> GetAll(string? search)
        {
            var query = new GetStudentUserListQuery { Search = search };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
