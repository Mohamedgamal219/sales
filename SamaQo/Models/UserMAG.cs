using Microsoft.AspNetCore.Identity;

namespace SamaQo.Models
{
    public class UserMAG:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
