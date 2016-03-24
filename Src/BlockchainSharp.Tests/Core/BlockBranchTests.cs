namespace BlockchainSharp.Tests.Core
{
    using System;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockBranchTests
    {
        [TestMethod]
        public void CreateAndAddBlock()
        {
            BlockBranch branch = new BlockBranch();
            Block block = new Block(42, new Hash());

            Assert.IsTrue(branch.TryToAddFirst(block));
        }

        [TestMethod]
        public void GetBlockByNumber()
        {
            BlockBranch branch = new BlockBranch();
            Block block = new Block(42, new Hash());
            branch.TryToAddFirst(block);

            Assert.AreEqual(block, branch.GetBlock(42));
            Assert.IsNull(branch.GetBlock(0));
            Assert.IsNull(branch.GetBlock(41));
            Assert.IsNull(branch.GetBlock(43));
        }
    }
}
