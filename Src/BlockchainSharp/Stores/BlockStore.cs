using System;
namespace BlockchainSharp.Stores
{
    interface BlockStore
    {
        BlockchainSharp.Core.Block GetByHash(BlockchainSharp.Core.Hash hash);
        System.Collections.Generic.IEnumerable<BlockchainSharp.Core.Block> GetByNumber(long number);
        void Save(BlockchainSharp.Core.Block block);
    }
}
