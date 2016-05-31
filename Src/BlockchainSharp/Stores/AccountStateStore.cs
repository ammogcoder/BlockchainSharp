﻿namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Tries;

    public class AccountStateStore
    {
        private Trie<AccountState> states;

        public AccountStateStore()
            : this(new Trie<AccountState>(new AccountState(BigInteger.Zero)))
        {
        }

        private AccountStateStore(Trie<AccountState> states)
        {
            this.states = states;
        }

        public AccountStateStore Put(Address address, AccountState state)
        {
            return new AccountStateStore(this.states.Put(address.ToString(), state));
        }

        public AccountState Get(Address address)
        {
            return this.states.Get(address.ToString());
        }
    }
}
