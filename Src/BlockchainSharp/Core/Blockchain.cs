namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockChain
    {
        private IList<Block> blocks = new List<Block>();

        public BlockChain(Block block)
        {
            if (!block.IsGenesis)
                throw new ArgumentException("Initial block should be genesis");

            this.blocks.Add(block);
        }

        public long BestBlockNumber { get { return this.blocks.Last().Number; } }

        public bool TryToAdd(Block block)
        {
            if (!block.HasParent(this.blocks.Last()))
                return false;

            this.blocks.Add(block);
            return true;
        }

        public Block GetBlock(int n)
        {
            if (n < 0 || n >= this.blocks.Count)
                return null;

            return this.blocks[n];
        }
    }
}
