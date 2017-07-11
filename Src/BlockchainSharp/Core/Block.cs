namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Linq;
    using System.Text;

    using BlockchainSharp.Encoding;
    using Org.BouncyCastle.Crypto.Digests;

    public class Block
    {
        private long number;
        private Hash hash;
        private Hash parentHash;
        private IList<Transaction> transactions;

        public Block(long number, Hash parentHash)
        {
            if (number == 0 && parentHash != null)
                throw new InvalidOperationException("Genesis block should have no parent");

            this.number = number;
            this.parentHash = parentHash;
            this.hash = new Hash();

            if (parentHash != null)
                this.hash = this.CalculateHash();
        }

        public Block(long number, Hash parentHash, IEnumerable<Transaction> transactions)
            : this(number, parentHash)
        {
            this.transactions = new List<Transaction>(transactions);
        }

        public IEnumerable<Transaction> Transactions { get { return this.transactions; } }

        public long Number { get { return this.number; } }

        public Hash Hash { get { return this.hash; } }

        public Hash ParentHash { get { return this.parentHash; } }

        public bool IsGenesis { get { return this.number == 0 && this.parentHash == null; } }

        public bool HasParent(Block parent)
        {
            if (parent == null && this.parentHash == null)
                return true;

            if (parent == null)
                return false;

            return parent.Number == this.number - 1 && parent.Hash.Equals(this.parentHash);
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
