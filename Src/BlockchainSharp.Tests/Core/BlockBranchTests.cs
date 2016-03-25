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
        public void TryToConnectEmptyBranches()
        {
            BlockBranch branch = new BlockBranch();

            Assert.IsFalse(branch.IsConnected());
            Assert.IsFalse(branch.TryToConnect(branch));
            Assert.IsFalse(branch.IsConnected());    
        }

        [TestMethod]
        public void TryToConnectDisconenctedBranches()
        {
            BlockBranch branch1 = new BlockBranch();
            BlockBranch branch2 = new BlockBranch();

            branch1.TryToAddFirst(new Block(40, new Hash()));
            branch2.TryToAddFirst(new Block(42, new Hash()));

            Assert.IsFalse(branch1.IsConnected());
            Assert.IsFalse(branch2.IsConnected());
            Assert.IsFalse(branch1.TryToConnect(branch2));
            Assert.IsFalse(branch1.IsConnected());
        }

        [TestMethod]
        public void ConnectedBranchHasGenesis()
        {
            BlockBranch branch1 = new BlockBranch();
            BlockBranch branch2 = new BlockBranch();

            Block genesis = new Block(0, null);
            Block block = new Block(1, genesis.Hash);

            branch1.TryToAddFirst(genesis);
            branch2.TryToAddFirst(block);

            Assert.IsTrue(branch2.TryToConnect(branch1));
            Assert.IsTrue(branch2.IsConnected());
            Assert.IsTrue(branch2.HasGenesis());
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

        [TestMethod]
        public void ToBlockChain()
        {
            BlockBranch branch = new BlockBranch();
            Block genesis = new Block(0, null);
            Block block1 = new Block(1, genesis.Hash);
            Block block2 = new Block(2, block1.Hash);

            branch.TryToAddFirst(genesis);
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
            BlockBranch branch1 = new BlockBranch();
            BlockBranch branch2 = new BlockBranch();

            Block genesis = new Block(0, null);
            Block block1 = new Block(1, genesis.Hash);
            Block block2 = new Block(2, block1.Hash);
            Block block3 = new Block(3, block2.Hash);

            branch1.TryToAddFirst(genesis);
            branch1.TryToAddLast(block1);
            branch2.TryToAddLast(block2);
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
