using System.ComponentModel.DataAnnotations;

namespace CurrencyChangerWebProject.Model
{
    public class LogIn
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
