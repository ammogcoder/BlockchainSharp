namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockHeaderTests
    {
        [TestMethod]
        public void CreateWithNumber()
        {
            BlockHeader header = new BlockHeader(42, null);

            Assert.AreEqual(42, header.Number);
        }

        [TestMethod]
        public void CreateWithNumberAndParentHash()
        {
            Hash parentHash = new Hash();
            BlockHeader header = new BlockHeader(42, parentHash);

            Assert.AreEqual(42, header.Number);
            Assert.AreEqual(parentHash, header.ParentHash);
        }
    }
}
