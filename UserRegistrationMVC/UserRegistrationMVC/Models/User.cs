using System.ComponentModel.DataAnnotations;


namespace UserRegistrationMVC.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StrongPassword]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
