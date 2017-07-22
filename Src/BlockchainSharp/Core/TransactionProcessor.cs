namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Stores;
    using BlockchainSharp.Tries;

    public class TransactionProcessor
    {
        public AccountsState ExecuteBlock(Block block, AccountsState initialstate)
        {
            if (block.Transactions == null)
                return initialstate;

            var state = initialstate;

            foreach (var tx in block.Transactions)
                this.ExecuteTransaction(tx, state, ref state);

            return state;
        }

        public bool ExecuteTransaction(Transaction transaction, AccountsState initialstate, ref AccountsState newstate)
        {
            var state = initialstate;

            try
            {
                var addr = transaction.Sender;
                var accstate = state.Get(addr);
                var newaccstate = accstate.SubtractFromBalance(transaction.Value);
                state = state.Put(addr, newaccstate);

                addr = transaction.Receiver;
                accstate = state.Get(addr);
                newaccstate = accstate.AddToBalance(transaction.Value);
                state = state.Put(addr, newaccstate);

                newstate = state;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
