namespace BlockchainSharp.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class BigIntegerEncoder
    {
        private static BigIntegerEncoder instance = new BigIntegerEncoder();

        public static BigIntegerEncoder Instance { get { return instance; } }

        public byte[] Encode(BigInteger value)
        {
            return Rlp.Encode(value.ToByteArray());
        }

        public BigInteger Decode(byte[] bytes)
        {
            return new BigInteger(Rlp.Decode(bytes));
        }
    }
}
