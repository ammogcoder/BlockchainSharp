namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockProcessor
    {
        private BlockChain chain;

        public BlockChain BlockChain { get { return this.chain; } }

        public void Process(Block block)
        {
            if (this.chain == null)
                this.chain = new BlockChain(block);
            else
                this.chain.TryToAdd(block);
        }
    }
}
