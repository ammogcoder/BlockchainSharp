namespace BlockchainSharp.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;
    using BlockchainSharp.Stores;

    public class BlockProcessor
    {
        private BlockChain chain;
        private IBlockStore store;

        public BlockProcessor()
        {
            this.store = new InMemoryBlockStore();
        }

        public BlockChain BlockChain { get { return this.chain; } }

        public void Process(Block block)
        {
            if (this.store.GetByHash(block.Hash) != null)
                return;

            this.store.Save(block);
            var unknownAncestor = this.GetUnknownAncestor(block);

            if (unknownAncestor != null)
                return;

            if (this.chain == null)
            {
                this.chain = new BlockChain(block);
                return;
            }

            this.chain.TryToAdd(block);

            this.TryConnect(block);
        }

        private void TryConnect(Block block) 
        {
            if (this.chain.BestBlockNumber < block.Number)
                this.chain = this.ToBlockChain(block);

            foreach (var child in this.store.GetByParentHash(block.Hash))
                this.TryConnect(child);
        }

        private BlockChain ToBlockChain(Block block)
        {
            BlockInfo[] blockinfos = new BlockInfo[block.Number + 1];

            long n = block.Number + 1;

            while (n > 0)
            {
                n--;
                blockinfos[n] = new BlockInfo(block, null);

                if (n > 0)
                    block = this.store.GetByHash(block.ParentHash);
            }

            return new BlockChain(blockinfos.ToList());
        }

        private Hash GetUnknownAncestor(Block block)
        {
            var parentHash = block.ParentHash;

            while (block.Number > 0)
            {
                block = this.store.GetByHash(parentHash);

                if (block == null)
                    return parentHash;

                parentHash = block.ParentHash;
            }

            return null;
        }
    }
}
