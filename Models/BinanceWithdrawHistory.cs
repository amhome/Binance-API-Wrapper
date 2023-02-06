namespace Project.Models.Binance
{
    public class BinanceWithdrawHistory
    {
        public string address { get; set; }
        public string amount { get; set; }
        public string applyTime { get; set; }
        public string coin { get; set; }
        public string id { get; set; }
        public string withdrawOrderId { get; set; }
        public string network { get; set; }
        //"transferType": 0,   // 1 for internal transfer, 0 for external transfer   
        public int status { get; set; } // 0(0:Email Sent,1:Cancelled 2:Awaiting Approval 3:Rejected 4:Processing 5:Failure 6:Completed)
        public string txId { get; set; }
    }

}
