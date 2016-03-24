namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockBranch
    {
        private IList<Block> blocks = new List<Block>();

        public BlockBranch()
        {
        }

        public bool TryToAddFirst(Block block)
        {
            if (blocks.Count == 0)
            {
                blocks.Insert(0, block);
                return true;
            }

            if (!blocks.First().HasParent(block))
                return false;

            blocks.Insert(0, block);

            return true;
        }

        public Block GetBlock(int number)
        {
            return this.blocks.FirstOrDefault(b => b.Number == number);
        }
    }
}
