using System.ComponentModel.DataAnnotations;

namespace CurrencyExсhanger.Web.Model
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the first name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required]
        public string Message{ get; set; }
    }
}
