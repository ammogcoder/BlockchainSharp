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
    }
}
