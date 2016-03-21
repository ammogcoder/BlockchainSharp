namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Core;

    [TestClass]
    public class BlockchainTests
    {
        [TestMethod]
        public void CreateWithInitialBlock()
        {
            Block block = new Block(0);
            Blockchain blockchain = new Blockchain(block);

            Assert.AreEqual(0, blockchain.BestBlockNumber);
        }

        [TestMethod]
        public void RejectNonGenesisInitialBlock()
        {
            Block block = new Block(1);

            try
            {
                new Blockchain(block);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                Assert.AreEqual("Initial block should be genesis", ex.Message);
            }
        }
    }
}
