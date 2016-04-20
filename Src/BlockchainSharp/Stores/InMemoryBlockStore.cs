namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;

    public class InMemoryBlockStore : BlockStore
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
            if (this.blocksbynumber.ContainsKey(number))
                return this.blocksbynumber[number];

            return new List<Block>();
        }

        public void Save(Block block)
        {
            this.blocks[block.Hash] = block;

            IList<Block> bs;

            if (this.blocksbynumber.ContainsKey(block.Number))
                bs = this.blocksbynumber[block.Number];
            else
            {
                bs = new List<Block>();
                this.blocksbynumber[block.Number] = bs;
            }

            bs.Add(block);
        }
    }
}
