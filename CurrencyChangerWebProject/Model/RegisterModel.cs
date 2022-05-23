using System.ComponentModel.DataAnnotations;

namespace CurrencyExсhanger.Web.Model
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter the email address")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter the first name")]
        [StringLength(50,MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter the last name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "Age")]
        [Range(18, 110, ErrorMessage = "Invalid age")]
        public int Age { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{1,16}$")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm the password")]
        [Compare("Password",ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
