using PGK.Application.Common.Paged;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageList
{
    public class MessageListVm
    {
        public int MessagesCount
        {
            get
            {
                return Messages.Count;
            }
        }
        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
    }
}
