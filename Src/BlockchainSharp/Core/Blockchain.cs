namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockChain
    {
        public long number;

        public BlockChain(Block block)
        {
            if (!block.IsGenesis)
                throw new ArgumentException("Initial block should be genesis");

            this.number = block.Number;
        }

        public long BestBlockNumber { get { return this.number; } }
    }
}
