
using Ardalis.SmartEnum;

namespace CrossCutting.Enums
{
    public sealed class ChannelTypeEnum : SmartEnum<ChannelTypeEnum>
    {
        public static readonly ChannelTypeEnum MercadoLivre = new(nameof(MercadoLivre), 1);
        public static readonly ChannelTypeEnum Whatsapp = new(nameof(Whatsapp), 2);

        public ChannelTypeEnum(string name, int value) : base(name, value)
        {
        }
    }
}
