namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Block
    {
        private long number;
        private Hash hash;
        private Hash parentHash;

        public Block(long number, Hash parentHash)
        {
            if (number == 0 && parentHash != null)
                throw new InvalidOperationException("Genesis block should have no parent");

            this.number = number;
            this.parentHash = parentHash;
            this.hash = new Hash();
        }

        public long Number { get { return this.number; } }

        public Hash Hash { get { return this.hash; } }

        public bool IsGenesis { get { return this.number == 0 && this.parentHash == null; } }

        public bool HasParent(Block parent)
        {
            if (parent == null && this.parentHash == null)
                return true;

            if (parent == null)
                return false;

            return parent.Number == this.number - 1 && parent.Hash.Equals(this.parentHash);
        }
    }
}
