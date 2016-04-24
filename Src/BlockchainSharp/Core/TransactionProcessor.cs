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
                foreach (var av in transaction.Inputs)
                {
                    var addr = av.Address.ToString();
                    var state = states.Get(addr);
                    var newstate = state.SubtractFromBalance(av.Value);
                    states = states.Put(addr, newstate);
                }

                foreach (var av in transaction.Outputs)
                {
                    var addr = av.Address.ToString();
                    var state = states.Get(addr);
                    var newstate = state.AddToBalance(av.Value);
                    states = states.Put(addr, newstate);
                }

                this.states = states;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
