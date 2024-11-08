using Microsoft.AspNetCore.Identity;

namespace KartverketProsjekt.Models.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
