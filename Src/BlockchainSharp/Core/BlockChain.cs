namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockChain
    {
        private IList<BlockInfo> blockinfos;

        public BlockChain(Block block)
        {
            if (!block.IsGenesis)
                throw new ArgumentException("Initial block should be genesis");
            this.blockinfos = new List<BlockInfo>();
            this.blockinfos.Add(new BlockInfo(block, null));
        }

        public BlockChain(IList<BlockInfo> blockinfos)
        {
            this.blockinfos = blockinfos;
        }

        public long BestBlockNumber { get { return this.blockinfos.Last().Block.Number; } }

        public Block BestBlock { get { return this.blockinfos.Last().Block; } }

        public bool TryToAdd(Block block)
        {
            if (this.blockinfos.Count == 0)
            {
                this.blockinfos.Add(new BlockInfo(block, null));
                return true;
            }

            if (!block.HasParent(this.blockinfos.Last().Block))
                return false;

            this.blockinfos.Add(new BlockInfo(block, null));

            return true;
        }

        public Block GetBlock(long n)
        {
            if (n < 0 || n >= this.blockinfos.Count)
                return null;

            return this.blockinfos[(int)n].Block;
        }
    }
}
