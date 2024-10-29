namespace KartverketProsjekt.Models.DomainModels
{
    public class UserRoleModel
    {
        public int UserRoleId { get; set; } // Primary Key
        public string UserRoleName { get; set; }
        public bool IsPrioritised { get; set; } = false; // Default: false, for prioritising reports

    }
}
