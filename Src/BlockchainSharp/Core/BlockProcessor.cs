namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockProcessor
    {
        private BlockChain chain;
        private IList<BlockBranch> branches = new List<BlockBranch>();

        public BlockChain BlockChain { get { return this.chain; } }

        public void Process(Block block)
        {
            if (this.chain == null)
                this.chain = new BlockChain(block);
            else
                this.chain.TryToAdd(block);

            int nprocessed = 0;

            foreach (var branch in this.branches)
                if (branch.TryToAddFirst(block))
                    nprocessed++;
                else if (branch.TryToAddLast(block))
                    nprocessed++;

            if (nprocessed == 0) 
            {
                BlockBranch branch = new BlockBranch();
                branch.TryToAddFirst(block);
                this.branches.Add(branch);
            }
        }
    }
}
