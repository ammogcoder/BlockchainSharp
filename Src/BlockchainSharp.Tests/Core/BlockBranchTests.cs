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
            Assert.IsFalse(branch.HasGenesis());
        }

        [TestMethod]
        public void HasGenesis()
        {
            BlockBranch branch = new BlockBranch();

            Assert.IsFalse(branch.HasGenesis());

            branch.TryToAddFirst(new Block(0, null));

            Assert.IsTrue(branch.HasGenesis());
        }

        [TestMethod]
        public void RejectAddFirstBlock()
        {
            BlockBranch branch = new BlockBranch();
            Block block = new Block(42, new Hash());
            Block block2 = new Block(41, new Hash());

            Assert.IsTrue(branch.TryToAddFirst(block));
            Assert.IsFalse(branch.TryToAddFirst(block2));
        }

        [TestMethod]
        public void AddBlockToLast()
        {
            BlockBranch branch = new BlockBranch();
            Block block = new Block(42, new Hash());
            Block block2 = new Block(43, block.Hash);

            Assert.IsTrue(branch.TryToAddFirst(block));
            Assert.IsTrue(branch.TryToAddLast(block2));

            Assert.AreEqual(block, branch.GetBlock(42));
            Assert.AreEqual(block2, branch.GetBlock(43));
        }

        [TestMethod]
        public void AddBlockToLastInEmptyBranch()
        {
            BlockBranch branch = new BlockBranch();
            Block block = new Block(42, new Hash());

            Assert.IsTrue(branch.TryToAddLast(block));

            Assert.AreEqual(block, branch.GetBlock(42));
        }

        [TestMethod]
        public void RejectBlockToLast()
        {
            BlockBranch branch = new BlockBranch();
            Block block = new Block(42, new Hash());
            Block block2 = new Block(43, new Hash());

            Assert.IsTrue(branch.TryToAddFirst(block));
            Assert.IsFalse(branch.TryToAddLast(block2));

            Assert.AreEqual(block, branch.GetBlock(42));
            Assert.IsNull(branch.GetBlock(43));
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
