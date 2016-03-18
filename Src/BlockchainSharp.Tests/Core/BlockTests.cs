namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void CreateWithNumber()
        {
            Block block = new Block(42);

            Assert.AreEqual(42, block.Number);
        }

        [TestMethod]
        public void IsGenesis()
        {
            Block block0 = new Block(0);
            Block block42 = new Block(42);

            Assert.IsTrue(block0.IsGenesis);
            Assert.IsFalse(block42.IsGenesis);
        }
    }
}
