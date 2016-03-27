namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Address
    {
        private static Random random = new Random();
        private byte[] bytes;

        public Address()
        {
            this.bytes = new byte[20];
            random.NextBytes(this.bytes);
        }

        public Address(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public byte[] Bytes { get { return this.bytes; } }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (!(obj is Address))
                return false;

            Address h = (Address)obj;

            if (this.bytes.Length != h.bytes.Length)
                return false;

            for (int k = 0; k < this.bytes.Length; k++)
                if (this.bytes[k] != h.bytes[k])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            int value = 0;

            for (int k = 0; k < this.bytes.Length; k++)
            {
                value += this.bytes[k];
                value <<= 1;
            }

            return value;
        }
    }
}
