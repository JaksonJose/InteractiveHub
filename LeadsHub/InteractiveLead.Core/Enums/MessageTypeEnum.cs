
using Ardalis.SmartEnum;

namespace InteractiveLead.Core.Enums
{
    public class MessageTypeEnum : SmartEnum<MessageTypeEnum>
    {
        public static readonly MessageTypeEnum Text = new(nameof(Text), 1);
        public static readonly MessageTypeEnum Template = new(nameof(Template), 2);
        public static readonly MessageTypeEnum Note = new(nameof(Note), 3);

        public MessageTypeEnum(string name, int value) : base(name, value)
        {
        }
    }
}
