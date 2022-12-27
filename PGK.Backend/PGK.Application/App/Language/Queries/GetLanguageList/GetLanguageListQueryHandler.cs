using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Language.Queries.GetLanguageList
{
    internal class GetLanguageListQueryHandler
        : IRequestHandler<GetLanguageListQuery, LanguageListVm>
    {
        private readonly IPGKDbContext _dbContext;
        public readonly IMapper _mapper;

        public GetLanguageListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<LanguageListVm> Handle(GetLanguageListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Language.Language> query = _dbContext.Languages;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();

                query = query.Where(u => 
                    u.Name.ToLower().Contains(search) 
                        || u.NameEn.ToLower().Contains(search)
                        || u.Code.ToLower().Contains(search));
            }

            var language = query.ProjectTo<LanguageDto>(_mapper.ConfigurationProvider);

            var languagePaged = await PagedList<LanguageDto>.ToPagedList(language,
                    request.PageNumber, request.PageSize);

            return new LanguageListVm { Results = languagePaged };
        }
    }
}
