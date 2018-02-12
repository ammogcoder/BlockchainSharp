namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Core.Types;
    using BlockchainSharp.Stores;

    public class Transaction
    {
        private Address sender;
        private Address receiver;
        private BigInteger value;
        private AccountsState store;

        public Transaction(Address sender, Address receiver, BigInteger value)
        {
            if (value.CompareTo(BigInteger.Zero) < 0)
                throw new InvalidOperationException("Transaction value is negative");

            this.sender = sender;
            this.receiver = receiver;
            this.value = value;
        }

        public Address Sender { get { return this.sender; } }

        public BigInteger Value { get { return this.value; } }

        public Address Receiver { get { return this.receiver; } }

        public AccountsState Store
        {
            get
            {
                return this.store;
            }

            set
            {
                this.store = value;
            }
        }
    }
}
