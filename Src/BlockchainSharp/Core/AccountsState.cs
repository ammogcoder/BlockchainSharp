namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Tries;

    public class AccountsState
    {
        private Trie<AccountState> states;

        public AccountsState()
            : this(new Trie<AccountState>(new AccountState(BigInteger.Zero)))
        {
        }

        private AccountsState(Trie<AccountState> states)
        {
            this.states = states;
        }

        public AccountsState Put(Address address, AccountState state)
        {
            return new AccountsState(this.states.Put(address.ToString(), state));
        }

        public AccountState Get(Address address)
        {
            return this.states.Get(address.ToString());
        }
    }
}
