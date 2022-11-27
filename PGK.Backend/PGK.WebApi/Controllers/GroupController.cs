using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Group.Commands.CreateGroup;
using PGK.Application.App.Group.Commands.DeleteGroup;
using PGK.Application.App.Group.Queries.GetClassroomTeacher;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Group.Queries.GetGroupList;
using PGK.Application.App.Raportichka.Commands.CreateRaportichka;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;

namespace PGK.WebApi.Controllers
{
    public class GroupController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GroupListVm>> GetAll()
        {
            var query = new GetGroupListQuery();

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDetails>> GetDetails(int id)
        {
            var query = new GetGroupDetailsQuery
            {
                GroupId = id
            };

            var details = await Mediator.Send(query);

            return Ok(details);
        }

        [Authorize]
        [HttpGet("{id}/ClassroomTeacher")]
        public async Task<ActionResult<TeacherUserDetails>> GetClassroomTeacher(int id)
        {
            var query = new GetClassroomTeacherQuery
            {
                GroupId = id
            };

            var details = await Mediator.Send(query);

            return Ok(details);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<CreateGroupVm>> Create(CreateGroupCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("{id}/Raportichka")]
        public async Task<ActionResult<CreateRaportichkaVm>> CreateRaportichka(int id)
        {
            var command = new CreateRaportichkaCommand
            {
                Role = UserRole.Value,
                UserId = UserId,
                GroupId = id
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGroupCommand { GroupId = id };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
