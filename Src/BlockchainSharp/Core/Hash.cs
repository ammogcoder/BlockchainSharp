namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Hash
    {
        private static Random random = new Random();
        private byte[] bytes;

        public Hash()
        {
            this.bytes = new byte[20];
            random.NextBytes(this.bytes);
        }

        public byte[] Bytes { get { return this.bytes; } }
    }
}
