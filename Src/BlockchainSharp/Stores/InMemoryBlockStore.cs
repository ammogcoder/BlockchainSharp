namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;

    public class InMemoryBlockStore
    {
        private IDictionary<Hash, Block> blocks = new Dictionary<Hash, Block>();

        public Block GetByHash(Hash hash)
        {
            if (this.blocks.ContainsKey(hash))
                return this.blocks[hash];

            return null;
        }

        public void Save(Block block)
        {
            this.blocks[block.Hash] = block;
        }
    }
}
