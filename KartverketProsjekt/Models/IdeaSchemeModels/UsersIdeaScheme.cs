using System.ComponentModel.DataAnnotations;

namespace KartverketProsjekt.Models.IdeaSchemeModels
{
    public class UsersIdeaScheme
    {
        [Key]
        public Guid UserSchemeId { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public string? HighPriority { get; set; }
        public string? MediumPriority { get; set; }
        public string? LowPriority { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? Profession { get; set; }
        public string? Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

    }
}
