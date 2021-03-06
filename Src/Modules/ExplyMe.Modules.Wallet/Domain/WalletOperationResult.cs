﻿using System;

namespace ExplyMe.Modules.Wallet.Domain
{
    public class WalletOperationResult
    {
        public Guid? TransactionId { get; set; }
        public bool Success { get; set; }
        public string Description { get; set; }

        public static WalletOperationResult Ok( Guid? transactionId)
        {
            return new WalletOperationResult
            {
                TransactionId = transactionId,
                Success = true,
                Description = "Sucesso"
            };
        }
    }
}
