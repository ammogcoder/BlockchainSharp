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
            this.balance = balance;
        }

        public BigInteger Balance { get { return this.balance; } }
    }
}
