using System.ComponentModel.DataAnnotations;

namespace CurrencyExсhanger.Web.Model
{
    public class LogInModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
