using System.ComponentModel.DataAnnotations;

namespace KartverketProsjekt.Models.ViewModels
{
    /// <summary>
    /// Represents the data required for a user login, including email and password.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
        [PasswordComplexity]
        public string Password { get; set; }
    }
}
