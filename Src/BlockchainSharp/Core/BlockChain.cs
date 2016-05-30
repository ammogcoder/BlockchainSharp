namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockChain
    {
        private IList<Block> blocks;

        public BlockChain(Block block)
        {
            if (!block.IsGenesis)
                throw new ArgumentException("Initial block should be genesis");
            this.blocks = new List<Block>();
            this.blocks.Add(block);
        }

        public BlockChain(IList<Block> blocks)
        {
            this.blocks = blocks;
        }

        public long BestBlockNumber { get { return this.blocks.Last().Number; } }

        public Block BestBlock { get { return this.blocks.Last(); } }

        public bool TryToAdd(Block block)
        {
            if (this.blocks.Count == 0)
            {
                this.blocks.Add(block);
                return true;
            }

            if (!block.HasParent(this.blocks.Last()))
                return false;

            this.blocks.Add(block);

            return true;
        }

        public Block GetBlock(long n)
        {
            if (n < 0 || n >= this.blocks.Count)
                return null;

            return this.blocks[(int)n];
        }
    }
}
