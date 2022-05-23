using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExсhanger.Web.Model
{
    public class Role
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
