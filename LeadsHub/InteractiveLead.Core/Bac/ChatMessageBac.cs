
using InteractiveLead.Core.Enums;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Interfaces.IRepository;
using AdaptiveKitCore.Responses;
using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Interfaces.IServices;

namespace InteractiveLead.Core.Bac
{
    /// <summary>
    /// This represent Chat Message Business Logic
    /// </summary>
    public sealed class ChatMessageBac : IChatMessageBac
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ILeadBac _leadBac;
        private readonly IWhatsappService _whatsappService;

        public ChatMessageBac(IChatMessageRepository chatMessageRepository, ILeadBac leadBac, IWhatsappService whatsappService)
        {
            _chatMessageRepository = chatMessageRepository;
            _leadBac = leadBac;
            _whatsappService = whatsappService;
        }

        public async Task<ChatMessageResponse> FetchChatMessagesByRequestAsync(FilterRequest filterRequest)
        {
            ChatMessageResponse response = await _chatMessageRepository.FetchChatMessagesByRequestAsync(filterRequest);

            return response;
        }

        /// <summary>
        /// Regiter a message, if its consultant change the message to in progress
        /// If everything ok send the message to the source channel
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <param name="lead"></param>
        /// <returns></returns>
        public async Task<SimpleResponse<ChatMessage>> RegisterChatMessageAsync(ChatMessage chatMessage, Lead lead)
        {
            SimpleResponse<ChatMessage> response = await _chatMessageRepository.RegisterChatMessageAsync(chatMessage);
            if (response.HasExcptionMessage || response.HasErrorMessage)
            {
                return response;
            }

            // Adding notes must not send message to anyone or update the leads
            if (MessageTypeEnum.FromName(chatMessage.MessageType).Equals(MessageTypeEnum.Note))
            {
                return response;
            }

            response.Model!.MessageStatus = MessageStatusEnum.Success.Name;

            // If it was sent by the consultant change to in progress
            if (chatMessage.MessageSender.Equals(ChatMessageEnum.Consultant.Name) 
                && !lead.Status.Equals(LeadStatusEnum.Scheduled))
            {
                if (!lead.Status.Equals(LeadStatusEnum.InProgress.Name)) 
                { 
                    lead.Status = LeadStatusEnum.InProgress.Name;                
                    ModelResponse result = await _leadBac.UpdateLeadByRequestAsync(lead);
                    if (result.HasExcptionMessage)
                    {
                        response.Messages.AddRange(result.Messages);
                    }
                }

                BaseResponse messageResult = await _whatsappService.SendMessageToIntegrationApiAsync(lead, chatMessage);
                if (messageResult.HasErrorMessage || messageResult.HasExcptionMessage)
                {
                    response.Model!.MessageStatus = MessageStatusEnum.Failed.Name;                    
                }

                await UpdateMessageByRequestAsync(response.Model!);
            }

            return response;
        }

        public async Task SetMessageAsReadAsync(IEnumerable<long> messageIds)
        {
           await _chatMessageRepository.SetMessageAsReadAsync(messageIds);
        }

        public async Task UpdateMessageByRequestAsync(ChatMessage chatMessage)
        {
            await _chatMessageRepository.UpdateMessageByRequestAsync(chatMessage);
        }
    }
}
