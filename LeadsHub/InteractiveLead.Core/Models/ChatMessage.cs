
using InteractiveLead.Core.Enums;

namespace InteractiveLead.Core.Models
{
    public sealed class ChatMessage
    {
        /// <summary>
        /// The message Id (it can be duplicate since it is not a primary key)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Company representant that the lead is talking with
        /// </summary>
        public long ConsultantId { get; set; }

        /// <summary>
        /// Lead that own the chat message
        /// </summary>
        public long LeadId { get; set; }

        /// <summary>
        /// Date and time that the message was registered
        /// </summary>
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Message to send, if it is a text;
        /// </summary>
        public string MessageBody { get; set; } = string.Empty;

        /// <summary>
        /// Type of message (template or text)
        /// </summary>
        public string MessageType { get; set; } = "Text";

        /// <summary>
        /// The sender of message (customer or company representative)
        /// </summary>
        public string MessageSender { get; set; } = ChatMessageEnum.Consultant.Name;

        /// <summary>
        /// Message status when try to send a message
        /// </summary>
        public string MessageStatus { get; set; } = MessageStatusEnum.Pending.Name;
    }
}
