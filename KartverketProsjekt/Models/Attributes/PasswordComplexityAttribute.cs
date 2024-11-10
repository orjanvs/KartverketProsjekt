using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordComplexityAttribute : ValidationAttribute
{
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
