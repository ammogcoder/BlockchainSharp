using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainSharp.Stores;
using BlockchainSharp.Core;

namespace BlockchainSharp.Tests.Stores
{
    [TestClass]
    public class InMemoryBlockStoreTests
    {
        [TestMethod]
        public void GetUndefinedBlockByHash()
        {
            var store = new InMemoryBlockStore();

            Assert.IsNull(store.GetByHash(new Hash()));
        }

        [TestMethod]
        public void SaveAndGetBlockByHash()
        {
            var block = new Block(42, new Hash());
            var hash = block.Hash;

            var store = new InMemoryBlockStore();

            store.Save(block);

            var result = store.GetByHash(hash);

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result.Number);
            Assert.AreEqual(hash, result.Hash);
        }
    }
}
