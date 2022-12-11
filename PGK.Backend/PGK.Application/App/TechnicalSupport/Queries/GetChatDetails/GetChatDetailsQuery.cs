using MediatR;
using PGK.Application.App.TechnicalSupport.Queries.GetChatList;

namespace PGK.Application.App.TechnicalSupport.Queries.GetChatDetails
{
    public class GetChatDetailsQuery : IRequest<ChatDto>
    {
        public int Id { get; set; }
    }
}
