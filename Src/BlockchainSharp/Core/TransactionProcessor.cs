namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Tries;
    using BlockchainSharp.Stores;

    public class TransactionProcessor
    {
        private AccountStateStore accounts;

        public TransactionProcessor(AccountStateStore accounts)
        {
            this.accounts = accounts;
        }

        public AccountStateStore Accounts { get { return this.accounts; } }

        public bool ExecuteTransaction(Transaction transaction)
        {
            var accounts = this.accounts.Snapshot();

            try
            {
                var addr = transaction.Sender;
                var state = accounts.Get(addr);
                var newstate = state.SubtractFromBalance(transaction.SenderValue);
                accounts.Put(addr, newstate);

                addr = transaction.Receiver;
                state = accounts.Get(addr);
                newstate = state.AddToBalance(transaction.ReceiverValue);
                accounts.Put(addr, newstate);

                this.accounts = accounts;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
