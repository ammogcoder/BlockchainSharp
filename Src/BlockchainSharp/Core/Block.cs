namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Linq;
    using System.Text;

    using BlockchainSharp.Core.Types;
    using BlockchainSharp.Encoding;
    using Org.BouncyCastle.Crypto.Digests;

    public class Block
    {
        private static Transaction[] emptyTxs = new Transaction[0];

        BlockHeader header;
        private Hash hash;
        private IList<Transaction> transactions;

        public Block(long number, Hash parentHash)
            : this(number, parentHash, emptyTxs)
        {
        }

        public Block(long number, Hash parentHash, IEnumerable<Transaction> transactions)
        {
            if (number == 0 && parentHash != null)
                throw new InvalidOperationException("Genesis block should have no parent");

            this.header = new BlockHeader(number, parentHash);
            this.hash = new Hash();

            this.transactions = new List<Transaction>(transactions);
        }

        public IList<Transaction> Transactions { get { return this.transactions; } }

        public long Number { get { return this.header.Number; } }

        public Hash Hash { get { return this.hash; } }

        public Hash ParentHash { get { return this.header.ParentHash; } }

        public bool IsGenesis { get { return this.header.Number == 0 && this.header.ParentHash == null; } }

        public bool HasParent(Block parent)
        {
            if (parent == null && this.header.ParentHash == null)
                return true;

            if (parent == null)
                return false;

            return parent.Number == this.header.Number - 1 && parent.Hash.Equals(this.header.ParentHash);
        }

        private Hash CalculateHash()
        {
            Sha3Digest digest = new Sha3Digest(256);
            byte[] bytes = BlockEncoder.Instance.Encode(this);
            digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] result = new byte[32];
            digest.DoFinal(result, 0);

            return new Hash(result);
        }
    }
}
