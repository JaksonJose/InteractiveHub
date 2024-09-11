using AdaptiveKitCore.Enums;
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Enums;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.App.Components.Pages.Manager.Adapters
{
    public sealed class ChatMessageAdapter
    {
        private readonly IChatMessageBac _chatMessageBac;
        private readonly ILeadBac _leadBac;

        public ChatMessageAdapter(IChatMessageBac chatMessageBac, ILeadBac leadBac)
        {
            _chatMessageBac = chatMessageBac;
            _leadBac = leadBac;
        }

        /// <summary>
        /// Fetch lead by id
        /// </summary>
        /// <param name="leadId">id of the lead to fetch</param>
        /// <returns>Response containing results and the lead fetched</returns>
        public async Task<ChatMessageResponse> FetchChatMessagesByLeadIdtAsync(long leadId)
        {
            FilterRequest filterRequest = new();
            filterRequest.AddFilter(nameof(ChatMessage.LeadId), FilterOperatorEnum.EqualTo, leadId);

            ChatMessageResponse response = await _chatMessageBac.FetchChatMessagesByRequestAsync(filterRequest);

            return response;
        }

        /// <summary>
        /// Register the message of the chat
        /// </summary>
        /// <param name="chatMessage">object containing the chat message attributes</param>
        /// <returns></returns>
        public async Task<SimpleResponse<ChatMessage>> RegisterChatMessageAsync(ChatMessage chatMessage)
        {
            FilterRequest filterRequest = new();
            filterRequest.AddFilter(nameof(Lead.Id), FilterOperatorEnum.EqualTo, chatMessage.LeadId, "ld");

            LeadResponse leadResponse = await _leadBac.FetchLeadsByRequestAsync(filterRequest);
            if (leadResponse.HasErrorMessage || leadResponse.HasExcptionMessage)
            {
                chatMessage.MessageStatus = MessageStatusEnum.Failed.Name;

                SimpleResponse<ChatMessage> responseFailed = new(chatMessage);

                return responseFailed;
            }

            Lead? lead = leadResponse.ResponseData.FirstOrDefault();
            if (lead == null)
            {
                chatMessage.MessageStatus = MessageStatusEnum.Failed.Name;
                SimpleResponse<ChatMessage> responseFailed = new(chatMessage);

                return responseFailed;
            }

            return await _chatMessageBac.RegisterChatMessageAsync(chatMessage, lead);
        }

        /// <summary>
        /// Change the Message status to read
        /// </summary>
        /// <param name="messageIds"></param>
        /// <returns></returns>
        public async Task SetMessagesToReadAsync(IEnumerable<long> messageIds)
        {
            await _chatMessageBac.SetMessageAsReadAsync(messageIds);
        }

        /// <summary>
        /// Update message
        /// </summary>
        /// <param name="chatMessage">Message to be updated</param>
        /// <returns></returns>
        public async Task UpdateMessageByRequestAsync(ChatMessage chatMessage)
        {
            await _chatMessageBac.UpdateMessageByRequestAsync(chatMessage);
        }

    }
}
