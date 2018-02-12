namespace BlockchainSharp.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;

    public class BlockHeaderEncoder
    {
        private static BigIntegerEncoder bigIntegerEncoder = new BigIntegerEncoder();
        private static HashEncoder hashEncoder = new HashEncoder();
        private static BlockHeaderEncoder instance = new BlockHeaderEncoder();

        public static BlockHeaderEncoder Instance { get { return instance; } }

        public byte[] Encode(BlockHeader header)
        {
            byte[] number = bigIntegerEncoder.Encode(new BigInteger(header.Number));
            byte[] hash = hashEncoder.Encode(header.ParentHash);

            return Rlp.EncodeList(number, hash);
        }

        public BlockHeader Decode(byte[] bytes)
        {
            IList<byte[]> list = Rlp.DecodeList(bytes);

            long number = (long)bigIntegerEncoder.Decode(list[0]);
            Hash hash = hashEncoder.Decode(list[1]);

            return new BlockHeader(number, hash);
        }
    }
}
