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
        public void GetNoBlocksByNumber()
        {
            var store = new InMemoryBlockStore();

            var result = store.GetByNumber(42);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetBlocksByNumber()
        {
            var block1 = new Block(42, new Hash());
            var block2 = new Block(42, new Hash());

            var store = new InMemoryBlockStore();

            store.Save(block1);
            store.Save(block2);

            var result = store.GetByNumber(42);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(b => b.Hash.Equals(block1.Hash)));
            Assert.IsTrue(result.Any(b => b.Hash.Equals(block2.Hash)));
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
