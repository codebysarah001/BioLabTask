using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistrationMVC.Models
{
    public class StrongPassword : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validation)
        {
            var password = value as string;

            if (password != null && password.Length >= 8 &&
                Regex.IsMatch(password, @"[A-Z]") &&
                Regex.IsMatch(password, @"[a-z]") &&
                Regex.IsMatch(password, @"[0-9]") &&
                Regex.IsMatch(password, @"[\W_]"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Password must be at least 8 characters long and contain uppercase letters, lowercase letters, numbers, and special characters.");
        }
    }
}