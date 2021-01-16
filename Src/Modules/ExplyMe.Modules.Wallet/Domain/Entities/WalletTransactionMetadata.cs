using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.Wallet.Domain.Entities
{
    public class WalletTransactionMetadata : Entity
    {
        public long Id { get; set; }
        public Guid TransactionId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
