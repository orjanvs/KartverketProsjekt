using Microsoft.AspNetCore.Identity;

namespace KartverketProsjekt.Models.DomainModels
{
    /// <summary>
    /// Represents an application user with additional profile data, extending IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}