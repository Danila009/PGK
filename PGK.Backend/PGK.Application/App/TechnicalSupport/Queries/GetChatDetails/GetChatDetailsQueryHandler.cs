using AutoMapper;
using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.App.TechnicalSupport.Queries.GetChatList;
using PGK.Application.Interfaces;
using PGK.Domain.TechnicalSupport;

namespace PGK.Application.App.TechnicalSupport.Queries.GetChatDetails
{
    internal class GetChatDetailsQueryHandler
        : IRequestHandler<GetChatDetailsQuery, ChatDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetChatDetailsQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ChatDto> Handle(GetChatDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Chats.FindAsync(request.Id);

            if(chat == null)
            {
                throw new NotFoundException(nameof(Chat), request.Id);
            }

            return _mapper.Map<ChatDto>(chat);
        }
    }
}
