namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockInfo
    {
        private Block block;
        private AccountsState state;

        public BlockInfo(Block block, AccountsState state)
        {
            this.block = block;
            this.state = state;
        }

        public Block Block { get { return this.block; } }

        public AccountsState State { get { return this.state; } } 
    }
}
