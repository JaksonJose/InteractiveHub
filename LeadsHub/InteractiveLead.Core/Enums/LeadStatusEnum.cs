
using Ardalis.SmartEnum;

namespace InteractiveLead.Core.Enums
{
    public sealed class LeadStatusEnum : SmartEnum<LeadStatusEnum>
    {
        public static readonly LeadStatusEnum New = new(nameof(New), 0);
        public static readonly LeadStatusEnum InProgress = new(nameof(InProgress), 1);
        public static readonly LeadStatusEnum Scheduled = new(nameof(Scheduled), 2);
        public static readonly LeadStatusEnum Refused = new(nameof(Refused), 3);
        public static readonly LeadStatusEnum Canceled = new(nameof(Canceled), 4);
        public static readonly LeadStatusEnum Closed = new(nameof(Closed), 5);

        public LeadStatusEnum(string name, int value) : base(name, value)
        {
        }
    }
}
