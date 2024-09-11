
using CrossCutting.Enums;
using CrossCutting.Models;
using MercadoLivre.Core.Broker;
using MercadoLivre.Core.Interface.IBac;
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLMessage;
using MercadoLivre.Core.Models.MLOrder;
using System.ComponentModel.Design;

namespace MercadoLivre.Core.Bac
{
    public class MessageBac : IMessageBac
    {
        private readonly IMessageBroker _messageBroker;

        public MessageBac(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public Task SendMensageToLeadManager(MLMessage message, string buyerName, string pathRef, long companyId)
        {
            LeadMessage leadMessage = new()
            {
                CompanyId = companyId,
                Identifier = message.From.UserId.ToString(), // buyer id
                MessageBody = message.Text,
                MessageDate = message.MessageDate.Created,
                MessageType = "Text",
                Name = buyerName,
                PathReference = pathRef,
                PhoneNumber = "",
                SourceChannel = ChannelTypeEnum.MercadoLivre.Name,
            };

            _messageBroker.SendMessage(leadMessage, "leadmessage");

            return Task.CompletedTask;
        }
    }
}
