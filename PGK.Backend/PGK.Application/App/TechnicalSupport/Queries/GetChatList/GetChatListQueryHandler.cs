using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using PGK.Application.App.User.Queries.GetUserLits;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.TechnicalSupport;

namespace PGK.Application.App.TechnicalSupport.Queries.GetChatList
{
    internal class GetChatListQueryHandler
        : IRequestHandler<GetChatListQuery, ChatListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetChatListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ChatListVm> Handle(GetChatListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Chat> query = _dbContext.Chats
                .Include(u => u.Messages)
                    .ThenInclude(u => u.User);

            var count = query.Count();
            var chatList = await query.Skip(
                (request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var chatDtoList = _mapper.Map<List<ChatDto>>(chatList);

            _mapper.Map<List<ChatDto>>(chatList);

            var chatListPaged = new PagedList<ChatDto>(chatDtoList.ToList(),
                count,request.PageNumber,request.PageSize);

            return new ChatListVm { Results = chatListPaged };
        }
    }
}