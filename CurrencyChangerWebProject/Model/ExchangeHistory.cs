using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Web;

namespace CurrencyExсhanger.Web.Model
{
    public class ExchangeHistory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public Registation FK_User_Id { get; set; }

        [Required]

        public string CurrentCurrencyСС { get; set; }

        [Required]
        public decimal CurrentCurrencyValue { get; set; }
        
        [Required]
        public decimal RateOfExchange { get; set; }

        [Required]
        public string DesiredСurrencyСС { get; set; }

        public decimal DesireCurrencyValue { get; set; }

        [Required] 
        public DateTime time { get; set; } = DateTime.Now;
    }
}
