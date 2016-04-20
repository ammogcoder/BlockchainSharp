namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using BlockchainSharp.Core;

    public interface BlockStore
    {
        Block GetByHash(Hash hash);

        IEnumerable<Block> GetByNumber(long number);

        void Save(Block block);
    }
}
