namespace KartverketProsjekt.Models.DomainModels
{
    public class UserModel
    {
        public int UserId { get; set; } // Primary Key
        public int UserRoleId { get; set; } // Foreign Key (ref. UserRole(UserRoleId))
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property for related UserRole
        public UserRoleModel UserRole { get; set; }
    }
}
