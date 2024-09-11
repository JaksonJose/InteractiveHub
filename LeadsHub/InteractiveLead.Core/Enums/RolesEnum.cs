
using Ardalis.SmartEnum;

namespace InteractiveLead.Core.Enums
{
    public class RolesEnum : SmartEnum<RolesEnum>
    {
        public static readonly RolesEnum SysAdmin = new(nameof(SysAdmin), 1);
        public static readonly RolesEnum Manager = new(nameof(Manager), 2);
        public static readonly RolesEnum NormalUser = new(nameof(NormalUser), 3);

        public RolesEnum(string name, int value) : base(name, value)
        {
        }
    }
}
