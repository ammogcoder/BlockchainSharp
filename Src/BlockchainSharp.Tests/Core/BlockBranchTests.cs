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
            Block block = new Block(42, new Hash());
            BlockBranch branch = new BlockBranch(block);

            Assert.IsFalse(branch.HasGenesis());
        }

        [TestMethod]
        public void HasGenesis()
        {
            BlockBranch branch = new BlockBranch(new Block(0, null));

            Assert.IsTrue(branch.HasGenesis());
        }

        [TestMethod]
        public void TryToConnectDisconenctedBranches()
        {
            BlockBranch branch1 = new BlockBranch(new Block(42, new Hash()));
            BlockBranch branch2 = new BlockBranch(new Block(40, new Hash()));

            Assert.IsFalse(branch1.IsConnected());
            Assert.IsFalse(branch2.IsConnected());
            Assert.IsFalse(branch1.TryToConnect(branch2));
            Assert.IsFalse(branch1.IsConnected());
        }

        [TestMethod]
        public void ConnectedBranchHasGenesis()
        {
            Block genesis = new Block(0, null);
            Block block = new Block(1, genesis.Hash);

            BlockBranch branch1 = new BlockBranch(genesis);
            BlockBranch branch2 = new BlockBranch(block);

            Assert.IsTrue(branch2.TryToConnect(branch1));
            Assert.IsTrue(branch2.IsConnected());
            Assert.IsTrue(branch2.HasGenesis());
        }

        [TestMethod]
        public void RejectAddFirstBlock()
        {
            Block block = new Block(42, new Hash());
            Block block2 = new Block(41, new Hash());
            BlockBranch branch = new BlockBranch(block);

            Assert.IsFalse(branch.TryToAddFirst(block2));
        }

        [TestMethod]
        public void AddBlockToLast()
        {
            Block block = new Block(42, new Hash());
            Block block2 = new Block(43, block.Hash);

            BlockBranch branch = new BlockBranch(block);

            Assert.IsTrue(branch.TryToAddLast(block2));

            Assert.AreEqual(block, branch.GetBlock(42));
            Assert.AreEqual(block2, branch.GetBlock(43));
        }

        [TestMethod]
        public void BranchWithInitialBlock()
        {
            Block block = new Block(42, new Hash());
            BlockBranch branch = new BlockBranch(block);

            Assert.AreEqual(block, branch.GetBlock(42));
        }

        [TestMethod]
        public void RejectBlockToLast()
        {
            Block block = new Block(42, new Hash());
            Block block2 = new Block(43, new Hash());

            BlockBranch branch = new BlockBranch(block);

            Assert.IsFalse(branch.TryToAddLast(block2));

            Assert.AreEqual(block, branch.GetBlock(42));
            Assert.IsNull(branch.GetBlock(43));
        }

        [TestMethod]
        public void GetBlockByNumber()
        {
            Block block = new Block(42, new Hash());

            BlockBranch branch = new BlockBranch(block);

            Assert.AreEqual(block, branch.GetBlock(42));
            Assert.IsNull(branch.GetBlock(0));
            Assert.IsNull(branch.GetBlock(41));
            Assert.IsNull(branch.GetBlock(43));
        }

        [TestMethod]
        public void ToBlockChain()
        {
            Block genesis = new Block(0, null);
            Block block1 = new Block(1, genesis.Hash);
            Block block2 = new Block(2, block1.Hash);

            BlockBranch branch = new BlockBranch(genesis);

            branch.TryToAddLast(block1);
            branch.TryToAddLast(block2);

            BlockChain chain = branch.ToBlockChain(1);

            Assert.IsNotNull(chain);
            Assert.AreEqual(1, chain.BestBlockNumber);
            Assert.AreEqual(genesis, chain.GetBlock(0));
            Assert.AreEqual(block1, chain.GetBlock(1));
        }

        [TestMethod]
        public void ConnectedBranchToBlockChain()
        {
            Block genesis = new Block(0, null);
            Block block1 = new Block(1, genesis.Hash);
            Block block2 = new Block(2, block1.Hash);
            Block block3 = new Block(3, block2.Hash);

            BlockBranch branch1 = new BlockBranch(genesis);
            BlockBranch branch2 = new BlockBranch(block2);

            branch1.TryToAddLast(block1);
            branch2.TryToAddLast(block3);

            branch2.TryToConnect(branch1);

            BlockChain chain = branch2.ToBlockChain(2);

            Assert.IsNotNull(chain);
            Assert.AreEqual(2, chain.BestBlockNumber);
            Assert.AreEqual(genesis, chain.GetBlock(0));
            Assert.AreEqual(block1, chain.GetBlock(1));
            Assert.AreEqual(block2, chain.GetBlock(2));
        }
    }
}
