using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Department.Commands.CreateDepartment;
using PGK.Application.App.Department.Commands.DeleteDepartment;
using PGK.Application.App.Department.Commands.UpdateDepartment;
using PGK.Application.App.Department.Queries.GetDepartmentDetails;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.WebApi.Models.Department;

namespace PGK.WebApi.Controllers
{
    public class DepartmentController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<DepartmentListVm>> GetAll(
            string? search, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetDepartmentListQuery
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
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var query = new GetDepartmentDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create(
            [FromBody] CreateDepartmentCommand command)
        {
            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentDto>> Update(
            int id,
            [FromBody] UpdateDepartmentModel model)
        {
            var command = new UpdateDepartmentCommand
            {
                DepartmentId = id,
                DepartmentHeadId = model.DepartmentHeadId,
                Name = model.Name
            };


            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
