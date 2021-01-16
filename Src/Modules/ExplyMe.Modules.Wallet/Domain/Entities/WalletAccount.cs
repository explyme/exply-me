using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.Wallet.Domain.Entities
{
    public class WalletAccount : Entity
    {
        public long Id { get; set; }
        public long AvailableBalance { get; set; }
        public long BlockedBalance { get; set; }
        public long FutureBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
