namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class AccountState
    {
        private BigInteger balance;
        private ulong nonce;

        public AccountState(BigInteger balance, ulong nonce)
        {
            if (BigInteger.Compare(BigInteger.Zero, balance) > 0)
                throw new InvalidOperationException("Invalid balance");

            this.balance = balance;
            this.nonce = nonce;
        }

        public BigInteger Balance { get { return this.balance; } }

        public ulong Nonce { get { return this.nonce; } }

        public AccountState AddToBalance(BigInteger amount)
        {
            return new AccountState(BigInteger.Add(this.balance, amount), this.nonce);
        }

        public AccountState SubtractFromBalance(BigInteger amount)
        {
            return new AccountState(BigInteger.Subtract(this.balance, amount), this.nonce);
        }
    }
}

