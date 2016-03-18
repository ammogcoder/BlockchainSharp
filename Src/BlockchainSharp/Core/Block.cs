namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Block
    {
        private long number;

        public Block(long number)
        {
            this.number = number;
        }

        public long Number { get { return this.number; } }
    }
}
