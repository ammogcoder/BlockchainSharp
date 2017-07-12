namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockHeader
    {
        private long number;
        private Hash hash;
        private Hash parentHash;

        public BlockHeader(long number, Hash parentHash)
        {
            this.number = number;
            this.parentHash = parentHash;
        }

        public long Number { get { return this.number; } }

        public Hash ParentHash { get { return this.parentHash; } }
    }
}
