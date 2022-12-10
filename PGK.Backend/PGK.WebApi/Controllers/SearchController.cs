using PGK.Application.App.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Search.Enums;

namespace PGK.WebApi.Controllers
{
    public class SearchController : Controller
    {
        /// <summary>
        /// Поиск по ключевым словам
        /// </summary>
        /// <param name="search">Ключивые слова для поиска</param>
        /// <param name="type">Тип поиска</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>SearchVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchVm))]
        public async Task<ActionResult> Search(
            string search, SearchType type = SearchType.ALL,
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new SearchQuery
            {
                Search = search,
                Type = type,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
