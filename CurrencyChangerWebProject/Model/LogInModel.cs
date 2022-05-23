using System.ComponentModel.DataAnnotations;

namespace CurrencyExсhanger.Web.Model
{
    public class LogInModel
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
