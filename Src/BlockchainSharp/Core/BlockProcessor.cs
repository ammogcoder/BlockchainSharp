namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockProcessor
    {
        private BlockChain2 chain;

        public BlockChain2 BlockChain { get { return this.chain; } }

        public void Process(Block block)
        {
            if (this.chain == null)
                this.chain = new BlockChain2(block);
            else
                this.chain.TryToAdd(block);
        }
    }
}
