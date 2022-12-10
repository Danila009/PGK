using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Vedomost.Queries.GetVedomostDetails;
using PGK.Application.App.Vedomost.Queries.GetVedomostFile;
using PGK.Application.App.Vedomost.Queries.GetVedomostList;

namespace PGK.WebApi.Controllers
{
    public class VedomostController : Controller
    {
        /// <summary>
        /// Получить список из ведомастей
        /// </summary>
        /// <param name="query">GetVedomostListQuery object</param>
        /// <returns>VedomostListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VedomostListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetVedomostListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить ведомость по индификатору
        /// </summary>
        /// <param name="id">индификатор ведомости</param>
        /// <returns>VedomostDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VedomostDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetVedomostDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Получить файл ведомости
        /// </summary>
        /// <param name="fileId">индификатор файла</param>
        /// <returns>File</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("File/{fileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetFile(string fileId)
        {
            var query = new GetVedomostFileQuery
            {
                FileId = fileId
            };

            var file = await Mediator.Send(query);

            return File(file, "multipart/form-data");
        }
    }
}
