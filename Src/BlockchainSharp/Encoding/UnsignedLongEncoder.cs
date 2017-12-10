namespace BlockchainSharp.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class UnsignedLongEncoder
    {
        private static UnsignedLongEncoder instance = new UnsignedLongEncoder();

        public static UnsignedLongEncoder Instance { get { return instance; } }

        public byte[] Encode(ulong value)
        {
            return new BigInteger(value).ToByteArray();
        }

        public ulong Decode(byte[] bytes)
        {
            return (ulong)(new BigInteger(bytes));
        }
    }
}
