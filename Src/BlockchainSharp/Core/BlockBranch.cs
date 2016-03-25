namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockBranch
    {
        protected IList<Block> blocks = new List<Block>();
        private BlockBranch parent = null;

        public BlockBranch()
        {
        }

        public long BestBlockNumber { get { return this.blocks.Last().Number; } }

        public bool IsConnected()
        {
            return this.parent != null;
        }

        public bool TryToConnect(BlockBranch branch)
        {
            if (this.blocks.Count == 0)
                return false;

            if (branch.blocks.Count == 0)
                return false;

            Block pblock = branch.GetBlock(this.blocks[0].Number - 1, this.blocks[0].ParentHash);

            if (pblock == null)
                return false;

            this.parent = branch;

            return true;
        }

        public bool TryToAddFirst(Block block)
        {
            if (this.blocks.Count == 0)
            {
                this.blocks.Insert(0, block);
                return true;
            }

            if (!this.blocks.First().HasParent(block))
                return false;

            this.blocks.Insert(0, block);

            return true;
        }

        public bool TryToAddLast(Block block)
        {
            if (this.blocks.Count == 0)
            {
                this.blocks.Add(block);
                return true;
            }

            if (!block.HasParent(this.blocks.Last()))
                return false;

            this.blocks.Add(block);

            return true;
        }

        public virtual Block GetBlock(long number)
        {
            return this.blocks.FirstOrDefault(b => b.Number == number);
        }

        public Block GetBlock(long n, Hash hash)
        {
            Block block = this.GetBlock(n);

            if (block == null || !block.Hash.Equals(hash))
                return null;

            return block;
        }

        public bool HasGenesis()
        {
            if (this.blocks.Count == 0)
                return false;

            if (this.blocks[0].IsGenesis)
                return true;

            if (this.parent != null)
                return this.parent.HasGenesis();

            return false;
        }

        public BlockChain ToBlockChain(long nblock)
        {
            if (this.parent != null)
            {
                BlockChain chain = this.parent.ToBlockChain(this.blocks[0].Number - 1);

                foreach (Block block in this.blocks)
                    if (block.Number > nblock)
                        break;
                    else
                        chain.TryToAdd(block);

                return chain;
            }
            else
            {
                BlockChain chain = new BlockChain(this.blocks[0]);

                for (var k = 1; k < this.blocks.Count; k++)
                {
                    Block block = this.blocks[k];

                    if (block.Number > nblock)
                        break;

                    chain.TryToAdd(block);
                }

                return chain;
            }
        }
    }
}
