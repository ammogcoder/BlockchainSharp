namespace BlockchainSharp.Core.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Hash
    {
        private static Random random = new Random();
        private byte[] bytes;

        public Hash()
        {
            this.bytes = new byte[20];
            random.NextBytes(this.bytes);
        }

        public Hash(byte[] bytes)
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

            if (!(obj is Hash))
                return false;

            Hash h = (Hash)obj;

            return this.bytes.SequenceEqual(h.bytes);
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
