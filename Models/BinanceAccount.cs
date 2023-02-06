using System.Collections.Generic;

namespace Project.Models.Binance
{
    public class BinanceAccount
    {
        public int makerCommission { get; set; }
        public int takerCommission { get; set; }
        public int buyerCommission { get; set; }
        public int sellerCommission { get; set; }
        public bool canTrade { get; set; }
        public bool canWithdraw { get; set; }
        public bool canDeposit { get; set; }
        public long updateTime { get; set; }
        public string accountType { get; set; } //spot
        public IEnumerable<BinanceAccountBalance> balances { get; set; }
        public IEnumerable<string> permissions { get; set; } //spot
    }

    public class BinanceAccountBalance
    {
        public string asset { get; set; } //BTC
        public double free { get; set; }
        public double locked { get; set; }
    }
}
