

using AdaptiveKitCore.Responses;
using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.IBac
{
    public interface IChatMessageBac
    {
        /// <summary>
        /// Fetch chat messages by request
        /// </summary>
        /// <param name="filterRequest">Filter Request for filtering the query</param>
        /// <returns>Response with chat messages filtered</returns>
        Task<ChatMessageResponse> FetchChatMessagesByRequestAsync(FilterRequest filterRequest);

        Task<SimpleResponse<ChatMessage>> RegisterChatMessageAsync(ChatMessage chatMessage, Lead lead);

        /// <summary>
        /// Set all messages as Read 
        /// </summary>
        /// <param name="messageIds">Messages ids to set as read</param>
        /// <returns></returns>
        Task SetMessageAsReadAsync(IEnumerable<long> messageIds);

        Task UpdateMessageByRequestAsync(ChatMessage chatMessage);
    }
}
