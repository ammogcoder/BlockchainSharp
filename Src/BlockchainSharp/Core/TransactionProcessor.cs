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
        public bool ExecuteTransaction(Transaction transaction, AccountStateStore initialstate, ref AccountStateStore newstate)
        {
            var state = initialstate;

            try
            {
                var addr = transaction.Sender;
                var accstate = state.Get(addr);
                var newaccstate = accstate.SubtractFromBalance(transaction.SenderValue);
                state = state.Put(addr, newaccstate);

                addr = transaction.Receiver;
                accstate = state.Get(addr);
                newaccstate = accstate.AddToBalance(transaction.ReceiverValue);
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
