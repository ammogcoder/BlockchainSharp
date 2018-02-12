namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core.Types;
    using Org.BouncyCastle.Crypto.Digests;
    using BlockchainSharp.Encoding;

    public class BlockHeader
    {
        private long number;
        private Hash parentHash;
        private Hash hash;

        public BlockHeader(long number, Hash parentHash)
        {
            this.number = number;
            this.parentHash = parentHash;
        }

        public long Number { get { return this.number; } }

        public Hash ParentHash { get { return this.parentHash; } }

        public Hash Hash
        {
            get
            {
                if (this.hash != null)
                    return this.hash;

                this.hash = this.CalculateHash();

                return this.hash;
            }
        }

        private Hash CalculateHash()
        {
            Sha3Digest digest = new Sha3Digest(256);
            byte[] bytes = BlockHeaderEncoder.Instance.Encode(this);
            digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] result = new byte[32];
            digest.DoFinal(result, 0);

            return new Hash(result);
        }
    }
}
