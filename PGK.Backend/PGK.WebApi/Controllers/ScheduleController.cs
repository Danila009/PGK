using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Schedule.Commands.CreateSchedule;
using PGK.Application.App.Schedule.Commands.CreateScheduleColumn;
using PGK.Application.App.Schedule.Commands.CreateScheduleDepartment;
using PGK.Application.App.Schedule.Commands.CreateScheduleRow;
using PGK.Application.App.Schedule.Commands.FileCreateSchedule;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;
using PGK.Application.App.Schedule.Queries.GetScheduleRowList;

namespace PGK.WebApi.Controllers
{
    public class ScheduleController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ScheduleListVm>> GetAll(
            DateTime? onlyDate, DateTime? startDate, DateTime? endDate,
            [FromQuery] List<int> departmentIds, [FromQuery] List<int> groupIds,
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyDate = onlyDate,
                StartDate = startDate,
                EndDate = endDate,
                DepartmentIds = departmentIds,
                GroupIds = groupIds
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("Departments")]
        public async Task<ActionResult<ScheduleDepartmentListVm>> GetDepartments(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleDepartmentListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("Columns")]
        public async Task<ActionResult<ScheduleColumnListVm>> GetColumns(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleColumnListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("Rows")]
        public async Task<ActionResult<ScheduleRowListVm>> GetRows(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleRowListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("File")]
        public async Task<ActionResult> FileCreate(IFormFile file)
        {
            var command = new FileCreateScheduleCommand
            {
                File = file
            };

            await Mediator.Send(command);

            return Ok();
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<CreateScheduleVm>> Create(CreateScheduleCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department")]
        public async Task<ActionResult<CreateScheduleDepartmentVm>> CreateDepartment(
            CreateScheduleDepartmentCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department/Column")]
        public async Task<ActionResult<CreateScheduleColumnVm>> CreateColumn(
            CreateScheduleColumnCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department/Column/Row")]
        public async Task<ActionResult<CreateScheduleRowVm>> CreateRow(
            CreateScheduleRowCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
