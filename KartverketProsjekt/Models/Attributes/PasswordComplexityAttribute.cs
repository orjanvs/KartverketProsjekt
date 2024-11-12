using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

/// <summary>
/// Validation attribute to enforce password complexity requirements.
/// </summary>
public class PasswordComplexityAttribute : ValidationAttribute
{
    /// <summary>
    /// Determines whether the specified password meets the complexity requirements.
    /// </summary>
    /// <param name="value">The password value to validate.</param>
    /// <param name="validationContext">The context in which validation is performed.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the password is valid.
    /// </returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;
        if (password == null)
        {
            return new ValidationResult("Password is required");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return new ValidationResult("Password must contain at least one uppercase letter.");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return new ValidationResult("Password must contain at least one lowercase letter.");
        }

        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            return new ValidationResult("Password must contain at least one numeric character.");
        }

        if (!Regex.IsMatch(password, @"[\W_]"))
        {
            return new ValidationResult("Password must contain at least one special character.");
        }

        return ValidationResult.Success;
    }
}