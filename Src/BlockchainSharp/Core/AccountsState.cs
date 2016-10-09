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
        private static AccountState defaultValue = new AccountState(BigInteger.Zero);
        private Trie<AccountState> states;

        public AccountsState()
            : this(new Trie<AccountState>())
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
            var result = this.states.Get(address.ToString());

            if (result == null)
                return defaultValue;

            return result;
        }
    }
}
