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
    }
}
