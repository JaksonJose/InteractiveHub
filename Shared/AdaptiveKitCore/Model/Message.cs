
using AdaptiveKitCore.Enums;

namespace AdaptiveKitCore.Model
{
    /// <summary>
    /// Encapsulates a message and all it's metadata or information.
    /// </summary>
    public sealed class Message
    {
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public Message()
        {
        }

        public Message(MessageTypeEnum messageType, string messageText = "")
        {
            MessageType = messageType;
            MessageText = messageText;
        }

        public MessageTypeEnum MessageType { get; set; } = MessageTypeEnum.None;

        public string MessageText { get; set; } = string.Empty;
    }
}
