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
        private static TransactionEncoder txEncoder = new TransactionEncoder();

        public static BlockEncoder Instance { get { return instance; } }

        public byte[] Encode(Block block)
        {
            byte[] number = bigIntegerEncoder.Encode(new BigInteger(block.Number));
            byte[] hash = hashEncoder.Encode(block.ParentHash);
            int ntxs = block.Transactions.Count();
            byte[][] txs = new byte[ntxs][];

            for (int k = 0; k < ntxs; k++)
                txs[k] = txEncoder.Encode(block.Transactions[k]);

            return Rlp.EncodeList(number, hash, Rlp.Encode(Rlp.EncodeList(txs)));
        }

        public Block Decode(byte[] bytes)
        {
            IList<byte[]> list = Rlp.DecodeList(bytes);

            long number = (long)bigIntegerEncoder.Decode(list[0]);
            Hash hash = hashEncoder.Decode(list[1]);
            IList<byte[]> btxs = Rlp.DecodeList(list[2]);

            IList<Transaction> txs = new List<Transaction>();

            for (int k = 0; k < btxs.Count; k++)
                txs.Add(txEncoder.Decode(btxs[k]));

            return new Block(number, hash, txs);
        }
    }
}
