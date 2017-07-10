namespace BlockchainSharp.Encoding
{
    using BlockchainSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class BlockEncoder
    {
        private static BigIntegerEncoder bigIntegerEncoder = new BigIntegerEncoder();
        private static HashEncoder hashEncoder = new HashEncoder();
        private static BlockEncoder instance = new BlockEncoder();

        public static BlockEncoder Instance { get { return instance; } }

        public byte[] Encode(Block block)
        {
            byte[] number = bigIntegerEncoder.Encode(new BigInteger(block.Number));
            byte[] hash = hashEncoder.Encode(block.ParentHash);

            return Rlp.EncodeList(number, hash);
        }

        public Block Decode(byte[] bytes)
        {
            IList<byte[]> list = Rlp.DecodeList(bytes);

            long number = (long)bigIntegerEncoder.Decode(list[0]);
            Hash hash = hashEncoder.Decode(list[1]);

            return new Block(number, hash);
        }
    }
}
