using ExplyMe.Modules.Wallet.Domain.Enums;
using System;

namespace ExplyMe.Modules.Wallet.Domain.Entities
{
    public class WalletTransaction
    {
        public WalletTransaction()
        {
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public TransactionStatusEnum Status { get; set; }
        public long? FromId { get; set; }
        public long ToId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public DateTime? VoidedAt { get; set; }
        public long Amount { get; set; }
        public string SoftDescriptor { get; set; }
    }
}
