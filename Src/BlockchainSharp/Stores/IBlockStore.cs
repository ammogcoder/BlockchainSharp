namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using BlockchainSharp.Core;

    public interface IBlockStore
    {
        Block GetByHash(Hash hash);

        IEnumerable<Block> GetByNumber(long number);

        IEnumerable<Block> GetByParentHash(Hash hash);

        void Save(Block block);
    }
}
