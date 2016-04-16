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
        private IDictionary<long, IList<Block>> blocksbynumber = new Dictionary<long, IList<Block>>();

        public Block GetByHash(Hash hash)
        {
            if (this.blocks.ContainsKey(hash))
                return this.blocks[hash];

            return null;
        }

        public IEnumerable<Block> GetByNumber(long number)
        {
            return new List<Block>();
        }

        public void Save(Block block)
        {
            this.blocks[block.Hash] = block;
        }
    }
}
