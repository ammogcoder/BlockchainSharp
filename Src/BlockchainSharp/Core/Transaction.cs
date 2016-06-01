namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Stores;

    public class Transaction
    {
        private Address sender;
        private BigInteger sendervalue;
        private Address receiver;
        private BigInteger receivervalue;
        private AccountStateStore store;

        public Transaction(Address sender, BigInteger sendervalue, Address receiver, BigInteger receivervalue)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.sendervalue = sendervalue;
            this.receivervalue = receivervalue;

            if (BigInteger.Compare(sendervalue, receivervalue) < 0)
                throw new InvalidOperationException("Transaction receiver value is greater than sender value");
        }

        public Address Sender { get { return this.sender; } }

        public BigInteger SenderValue { get { return this.sendervalue; } }

        public Address Receiver { get { return this.receiver; } }

        public BigInteger ReceiverValue { get { return this.receivervalue; } }

        public AccountStateStore Store
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
