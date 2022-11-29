using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using PGK.Application.App.Schedule.Queries;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;
using PGK.Application.App.Schedule.Queries.GetScheduleRowList;

namespace PGK.WebApi.Controllers
{
    public class ScheduleController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<ScheduleListVm>> GetAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

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
    }
}
