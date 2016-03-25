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
            int nprocessed = 0;

            if (this.chain == null)
            {
                this.chain = new BlockChain(block);
                nprocessed++;
            }
            else if (this.chain.TryToAdd(block))
                nprocessed++;

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

            foreach (var branch in this.branches)
            {
                if (branch.IsConnected())
                    continue;

                if (branch.TryToConnect(this.chain))
                    continue;

                foreach (var branch2 in this.branches)
                    branch.TryToConnect(branch2);
            }

            foreach (var branch in this.branches)
            {
                if (!branch.HasGenesis())
                    continue;

                if (branch.BestBlockNumber > this.chain.BestBlockNumber)
                    this.chain = branch.ToBlockChain(branch.BestBlockNumber);
            }
        }
    }
}
