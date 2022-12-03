using MediatR;
using PGK.Application.App.Search.Enums;

namespace PGK.Application.App.Search
{
    public class SearchQuery : IRequest<SearchVm>
    {
        public SearchType Type { get; set; }

        public string Search { get; set; } = string.Empty;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
