
using CrossCutting.Enums;
using CrossCutting.Models;
using whatsapp.Core.Broker;
using whatsapp.Core.Interfaces.IBac;
using Whatsapp.Core.Models;
using Whatsapp.Core.Models.Receive;

namespace whatsapp.Core.Bac
{
    public sealed class WhatsappSendMessaBac : IWhatsappSendMessageBac
    {
        private readonly IMessageBroker _messageBroker;

        public WhatsappSendMessaBac(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public Task SendMessageToLeadsManagerAsync(WhatsappPayLoad whatsappMessage, long companyId)
        {
            LeadMessage leadMessage = new();

            foreach (var entry in whatsappMessage.Entry)
            {
                leadMessage.CompanyId = companyId;
                leadMessage.Name = entry.Changes[0].Value.Contacts[0].Profile.Name;
                leadMessage.PhoneNumber = entry.Changes[0].Value.Messages[0].From;
                leadMessage.MessageBody = entry.Changes[0].Value.Messages[0].Text.Body;

                string messageType = entry.Changes[0].Value.Messages[0].Type;
                string messageTypeCaptalized = char.ToUpper(messageType[0]) + messageType[1..];

                leadMessage.MessageType = messageTypeCaptalized;                

                // As we are using postgresSQL TimestampTz, have to convert to server hour
                long timestampUnix = Convert.ToInt64(entry.Changes[0].Value.Messages[0].TimeStamp);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestampUnix);
                leadMessage.MessageDate = dateTimeOffset.LocalDateTime;
            };

            leadMessage.SourceChannel = ChannelTypeEnum.Whatsapp.Name;

            _messageBroker.SendMessage(leadMessage, "leadmessage");

            return Task.CompletedTask;
        }
    }
}
