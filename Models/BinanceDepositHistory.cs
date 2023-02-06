namespace Project.Models.Binance
{
    public class BinanceDepositHistory
    {
        public string amount { get; set; }
        public string coin { get; set; }
        public string network { get; set; }
        public int status { get; set; } // 0(0:pending,6: credited but cannot withdraw, 1:success)
        public string address { get; set; }
        public string txId { get; set; }
        public long insertTime { get; set; }
        public string confirmTimes { get; set; }
    }

}
