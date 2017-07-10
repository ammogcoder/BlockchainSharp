namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Linq;
    using System.Text;

    using BlockchainSharp.Encoding;

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
//            this.hash = this.CalculateHash();
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
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            return new Hash(provider.ComputeHash(BlockEncoder.Instance.Encode(this)));
        }
    }
}
