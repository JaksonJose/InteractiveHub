using Ardalis.SmartEnum;

namespace InteractiveLead.Core.Enums
{
    /// <summary>
    /// Smart Enum that represents who sent the message
    /// it is usefull to identify from where the message came from 
    /// and keep a history
    /// </summary>
    public sealed class ChatMessageEnum : SmartEnum<ChatMessageEnum>
    {
        /// <summary>Represents a representant of the company</summary>
        public static readonly ChatMessageEnum Consultant = new(nameof(Consultant), 1);

        /// <summary>Represents the client of the company</summary>
        public static readonly ChatMessageEnum Customer = new(nameof(Customer), 2);

        public ChatMessageEnum(string name, int value) : base(name, value)
        {
        }
    }
}
