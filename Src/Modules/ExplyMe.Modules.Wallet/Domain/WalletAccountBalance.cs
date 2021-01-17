namespace ExplyMe.Modules.Wallet.Domain
{
    public class WalletAccountBalance
    {
        public long AvailableBalance { get; set; }
        public long BlockedBalance { get; set; }
        public long FutureBalance { get; set; }
    }
}
