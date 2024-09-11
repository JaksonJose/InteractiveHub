
namespace InteractiveLead.Core.Models
{
    public class UserRegister
    {
        public int CompanyId { get; set; }        

        public bool Enabled { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
