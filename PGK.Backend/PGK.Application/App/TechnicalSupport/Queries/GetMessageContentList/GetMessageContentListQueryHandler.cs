using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.TechnicalSupport;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList
{
    internal class GetMessageContentListQueryHandler
        : IRequestHandler<GetMessageContentListQuery, MessageContentListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetMessageContentListQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MessageContentListVm> Handle(GetMessageContentListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<MessageContent> query = _dbContext.MessageContents;

            if(request.Type != null)
            {
                query = query.Where(u => u.Type == request.Type);
            }

            if(request.ChatId != null)
            {
                query = query.Where(u => u.Message.Chat.Id == request.ChatId);
            }

            var content = query
                .ProjectTo<MessageContentDto>(_mapper.ConfigurationProvider);

            var contentPaged = await PagedList<MessageContentDto>.ToPagedList(content,
                request.PageNumber, request.PageSize);

            return new MessageContentListVm
            {
                Results = contentPaged
            };
        }
    }
}
