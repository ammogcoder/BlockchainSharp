namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Tries;
    using BlockchainSharp.Encoding;

    public class AccountsState
    {
        private static AccountStateEncoder encoder = new AccountStateEncoder();
        private static AccountState defaultValue = new AccountState(BigInteger.Zero, 0);

        private Trie states;

        public AccountsState()
            : this(new Trie())
        {
        }

        private AccountsState(Trie states)
        {
            this.states = states;
        }

        public AccountsState Put(Address address, AccountState state)
        {
            return new AccountsState(this.states.Put(address.ToString(), encoder.Encode(state)));
        }

        public AccountState Get(Address address)
        {
            var result = this.states.Get(address.ToString());

            if (result == null)
                return defaultValue;

            return encoder.Decode(result);
        }
    }
}
