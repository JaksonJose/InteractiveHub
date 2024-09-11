

using Ardalis.SmartEnum;

namespace InteractiveLead.Core.Enums
{
    public class MessageStatusEnum : SmartEnum<MessageStatusEnum>
    {
        /// <summary>
        /// The message was suscessfully delivered to the client
        /// </summary>
        public static readonly MessageStatusEnum Delivered = new(nameof(Delivered), 1);

        /// <summary>
        /// If message was not successful sent
        /// </summary>
        public static readonly MessageStatusEnum Failed = new(nameof(Failed), 2);

        /// <summary>
        /// If message is new, this is useful when receive a new message
        /// </summary>
        public static readonly MessageStatusEnum New = new(nameof(New), 3);

        /// <summary>
        /// The message is pending to define if it is successful sent or failed
        /// </summary>
        public static MessageStatusEnum Pending = new(nameof(Pending), 4);

        /// <summary>
        /// This indicates when the message was read by the client
        /// </summary>
        public static readonly MessageStatusEnum Read = new(nameof(Read), 5);

        /// <summary>
        /// If message was successful sent
        /// </summary>
        public static readonly MessageStatusEnum Success = new(nameof(Success), 6);

        public MessageStatusEnum(string name, int value) : base(name, value)
        {
        }
    }
}
