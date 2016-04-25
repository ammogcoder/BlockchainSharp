namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Tries;

    public class TransactionProcessor
    {
        private Trie<AccountState> states;

        public TransactionProcessor(Trie<AccountState> states)
        {
            this.states = states;
        }

        public Trie<AccountState> States { get { return this.states; } }

        public bool ExecuteTransaction(Transaction transaction)
        {
            var states = this.states;

            try
            {
                var addr = transaction.Sender.ToString();
                var state = states.Get(addr);
                var newstate = state.SubtractFromBalance(transaction.SenderValue);
                states = states.Put(addr, newstate);

                addr = transaction.Receiver.ToString();
                state = states.Get(addr);
                newstate = state.AddToBalance(transaction.ReceiverValue);
                states = states.Put(addr, newstate);

                this.states = states;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
