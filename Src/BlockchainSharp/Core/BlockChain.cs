namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockChain : BlockBranch
    {
        public BlockChain(Block block)
        {
            if (!block.IsGenesis)
                throw new ArgumentException("Initial block should be genesis");

            this.blocks.Add(block);
        }

        public bool TryToAdd(Block block)
        {
            return this.TryToAddLast(block);
        }

        public override Block GetBlock(int n)
        {
            if (n < 0 || n >= this.blocks.Count)
                return null;

            return this.blocks[n];
        }
    }
}
