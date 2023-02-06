using System.ComponentModel.DataAnnotations;

namespace Project.Models.Binance
{
    public class BinanceWithdrawRequest
    {
        [Required]
        public string coin { get; set; }
        [Required]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1} for amount")]
        public double amount { get; set; }
        [Required]
        public string address { get; set; }
    }


    public class BinanceWithdrawResponse
    {
        public string id { get; set; }
    }
}
