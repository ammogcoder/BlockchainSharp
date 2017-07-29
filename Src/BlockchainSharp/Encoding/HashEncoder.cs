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
            if (hash == null)
                return Rlp.Encode(null);

            return Rlp.Encode(hash.Bytes);
        }

        public Hash Decode(byte[] bytes)
        {
            byte[] hash = Rlp.Decode(bytes);

            if (hash.Length == 0)
                return null;

            return new Hash(hash);
        }
    }
}
