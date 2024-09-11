
using InteractiveLead.Core.Enums;

namespace InteractiveLead.Core.Models
{
    public sealed class LeadInfoCard
    {
        /// <summary>
        /// Id to identify the consultant
        /// </summary>
        public long ConsultantId { get; private set; }

        /// <summary>
        /// Consultant Name
        /// </summary>
        public string ConsultantName { get; private set; } = string.Empty;

        /// <summary>
        /// Id to identify the lead
        /// </summary>
        public long LeadId { get; private set; }

        /// <summary>
        /// The Name of the Lead
        /// </summary>
        public string LeadName { get; private set; } = string.Empty;

        /// <summary>
        /// The Total of new messages in the lead chat
        /// </summary>
        public long TotalNewMessages { get; private set; }

        /// <summary>
        /// The status of the lead
        /// </summary>
        public string LeadStatus { get; private set; } = string.Empty;

        /// <summary>
        /// Last message time in the Message chat
        /// </summary>
        public DateTime LastMessageDate { get; private set; }


        public List<ChatMessage> ChatMessages { get; private set; } = [];

        /// <summary>
        /// Date that it was registered in the system
        /// </summary>
        public DateTime CreatedAt {  get; private set; }

        public bool CanSendMessage { get; private set; } = false;

        public void VerifyCanSendMessage()
        {
            ChatMessage? message = ChatMessages.FindLast(c => c.MessageSender.Equals(ChatMessageEnum.Customer.Name));

            DateTime currentDateTime = DateTime.Now;

            TimeSpan? timeResult = currentDateTime.Subtract(message!.MessageDate);

            CanSendMessage = timeResult?.TotalHours <= 24;
        }

        public void AddMessage(ChatMessage chatMessage)
        {
            LastMessageDate = chatMessage.MessageDate;
            ChatMessages.Add(chatMessage);
        }

        public void AddMessages(List<ChatMessage> chatMessages)
        {
            ChatMessages = chatMessages;
        }

        /// <summary>
        /// Set Total New Messages as zero,
        /// It means all messages was read
        /// </summary>
        public void ClearTotalNewMessages() => TotalNewMessages = 0;

        public void SetLastMessageDate(DateTime dateTime) => LastMessageDate = dateTime;
    }
}
