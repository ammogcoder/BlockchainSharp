namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Core;

    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void CreateWithNumber()
        {
            Block block = new Block(42);

            Assert.AreEqual(42, block.Number);
        }
    }
}
