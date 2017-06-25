namespace BlockchainSharp.Encoding
{
    using BlockchainSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HashEncoder
    {
        public byte[] Encode(Hash hash)
        {
            return Rlp.Encode(hash.Bytes);
        }

        public Hash Decode(byte[] bytes)
        {
            byte[] hash = Rlp.Decode(bytes);

            return new Hash(hash);
        }
    }
}
