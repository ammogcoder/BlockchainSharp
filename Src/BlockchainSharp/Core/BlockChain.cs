namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockChain : BlockBranch
    {
        public BlockChain(Block block)
            : base(block)
        {
            if (!block.IsGenesis)
                throw new ArgumentException("Initial block should be genesis");
        }

        public bool TryToAdd(Block block)
        {
            return this.TryToAddLast(block);
        }

        public override Block GetBlock(long n)
        {
            if (n < 0 || n >= this.Blocks.Count)
                return null;

            return this.Blocks[(int)n];
        }
    }
}
