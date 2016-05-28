namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;

    public class InMemoryBlockStore : IBlockStore
    {
        private IDictionary<Hash, Block> blocks = new Dictionary<Hash, Block>();
        private IDictionary<long, IList<Block>> blocksbynumber = new Dictionary<long, IList<Block>>();
        private IDictionary<Hash, IList<Block>> blocksbyparenthash = new Dictionary<Hash, IList<Block>>();

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

        public IEnumerable<Block> GetByParentHash(Hash hash)
        {
            if (this.blocksbyparenthash.ContainsKey(hash))
                return this.blocksbyparenthash[hash];

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

            IList<Block> bs2
                ;
            if (this.blocksbyparenthash.ContainsKey(block.ParentHash))
                bs2 = this.blocksbyparenthash[block.ParentHash];
            else
            {
                bs2 = new List<Block>();
                this.blocksbyparenthash[block.ParentHash] = bs2;
            }

            bs2.Add(block);
        }
    }
}
