
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.IRepository
{
    public interface IChatMessageRepository
    {
        Task<ChatMessageResponse> FetchChatMessagesByRequestAsync(FilterRequest filterRequest);

        Task<SimpleResponse<ChatMessage>> RegisterChatMessageAsync(ChatMessage chatMessage);

        /// <summary>
        /// Set messages as read
        /// </summary>
        /// <param name="messageIds">Ids of messages to set as read</param>
        /// <returns></returns>
        Task SetMessageAsReadAsync(IEnumerable<long> messageIds);

        Task UpdateMessageByRequestAsync(ChatMessage chatMessage);
    }
}
