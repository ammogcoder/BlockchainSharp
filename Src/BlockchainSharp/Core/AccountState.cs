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

        public AccountState(BigInteger balance)
        {
            if (BigInteger.Compare(BigInteger.Zero, balance) > 0)
                throw new InvalidOperationException("Invalid balance");

            this.balance = balance;
        }

        public BigInteger Balance { get { return this.balance; } }

        public AccountState AddToBalance(BigInteger amount)
        {
            return new AccountState(BigInteger.Add(this.balance, amount));
        }

        public AccountState SubtractFromBalance(BigInteger amount)
        {
            return new AccountState(BigInteger.Subtract(this.balance, amount));
        }
    }
}

