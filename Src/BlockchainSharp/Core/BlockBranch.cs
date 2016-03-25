namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockBranch
    {
        protected IList<Block> blocks = new List<Block>();

        public BlockBranch()
        {
        }

        public long BestBlockNumber { get { return this.blocks.Last().Number; } }

        public bool TryToAddFirst(Block block)
        {
            if (this.blocks.Count == 0)
            {
                this.blocks.Insert(0, block);
                return true;
            }

            if (!this.blocks.First().HasParent(block))
                return false;

            this.blocks.Insert(0, block);

            return true;
        }

        public bool TryToAddLast(Block block)
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

        public virtual Block GetBlock(int number)
        {
            return this.blocks.FirstOrDefault(b => b.Number == number);
        }

        public Block GetBlock(int n, Hash hash)
        {
            Block block = this.GetBlock(n);

            if (block == null || !block.Hash.Equals(hash))
                return null;

            return block;
        }

        public bool HasGenesis()
        {
            return this.blocks.Count > 0 && this.blocks[0].IsGenesis;
        }
    }
}
