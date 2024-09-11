
using AdaptiveKitCore.Responses;
using CrossCutting.Models;
using InteractiveLead.Core.Enums;
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.NotifyServices;

namespace InteractiveLead.Core.Bac.CentralHub
{
    public class LeadHubCentralBac : ILeadHubCentralBac
    {
        private readonly ILeadCentralHubRepository _leadCentralRepository;
        private readonly IChatMessageBac _chatMessageBac;
        private readonly IDistribuitionBac _distribuitionBac;
        private readonly MessageNotifyService _messageNotifyService;

        public LeadHubCentralBac(
            ILeadCentralHubRepository leadCentralRepository,
            IChatMessageBac chatMessageBac,
            IDistribuitionBac distribuitionBac,
            MessageNotifyService messageNotifyService)
        {
            _leadCentralRepository = leadCentralRepository;
            _chatMessageBac = chatMessageBac;
            _messageNotifyService = messageNotifyService;
            _distribuitionBac = distribuitionBac;
        }

        public async Task ReceiveMessagerToChatAsync(LeadMessage leadMessage)
        {
            //TODO: Apply parallelism

            Lead lead = new()
            {
                Identifier = leadMessage.Identifier,
                Name = leadMessage.Name,
                PhoneNumber = leadMessage.PhoneNumber,
                CompanyId = leadMessage.CompanyId,
                PathReferece = leadMessage.PathReference,
                SourceChannel = leadMessage.SourceChannel,
            };

            lead = await _leadCentralRepository.VerifyLeadExistAsync(lead);

            // new lead
            if (lead.Id == 0)
            {
                SimpleResponse<Consultant> consultantResponse = await _distribuitionBac.DistributeLeadsSequencialAsync(lead.CompanyId);
                if (consultantResponse.HasErrorMessage || consultantResponse.HasExcptionMessage)
                {
                    // TODO: must implement a log here
                }

                Consultant consultant = consultantResponse.Model!;

                lead.ConsultantId = consultant.Id;
                lead.Consultant = consultant;
                lead.Status = LeadStatusEnum.New.Name;

                SimpleResponse<Lead>  leadResponse = await _leadCentralRepository.RegisterLeadAsync(lead);
                if (leadResponse.HasErrorMessage)
                {
                    // TODO: must implement a log here
                }

                lead.Id = leadResponse.Model!.Id;

                consultant.TimeLastLeadAssigned = DateTime.UtcNow;

                await _leadCentralRepository.UpdateConsultantsByRequestAsync(consultant);

                _messageNotifyService.NotifyNewLeadReceived(leadResponse.Model!);
            }

            await RegisterLeadMessageAsync(leadMessage, lead);
            // TODO: must implement a log here
        }

        /// <summary>
        /// Register chat message of the lead
        /// </summary>
        /// <param name="message">Lead Message</param>
        /// <param name="lead"></param>
        private async Task RegisterLeadMessageAsync(LeadMessage leadMessage, Lead lead)
        {
            ChatMessage chatMessage = new()
            {
                LeadId = lead.Id,
                ConsultantId = lead.ConsultantId,
                MessageBody = leadMessage.MessageBody,
                MessageDate = leadMessage.MessageDate,
                MessageSender = ChatMessageEnum.Customer.Name,
                MessageType = leadMessage.MessageType,
                MessageStatus = MessageStatusEnum.New.Name
            };

            var resposne = await _chatMessageBac.RegisterChatMessageAsync(chatMessage, lead);

            if (resposne.HasErrorMessage || resposne.HasExcptionMessage)
            {
                // TODO: must implement a log here
            }

            // Should notify only the summary and the least of leads should update if there is less lead then default.           
            _messageNotifyService.NotifyNewChatMessage(chatMessage);
        }
    }
}
