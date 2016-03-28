namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class AddressValue
    {
        private Address address;
        private BigInteger value;

        public AddressValue(Address address, BigInteger value)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (value.CompareTo(0) < 0)
                throw new ArgumentOutOfRangeException("value", "It should be non negative");

            this.address = address;
            this.value = value;
        }

        public Address Address { get { return this.address; } }

        public BigInteger Value { get { return this.value; } }
    }
}
