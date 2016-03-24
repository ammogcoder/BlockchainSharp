namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockProcessorTests
    {
        [TestMethod]
        public void CreateWithNoBlockChain()
        {
            BlockProcessor processor = new BlockProcessor();

            Assert.IsNull(processor.BlockChain);
        }

        [TestMethod]
        public void ProcessGenesisBlock()
        {
            Block genesis = new Block(0, null);
            BlockProcessor processor = new BlockProcessor();

            processor.Process(genesis);

            Assert.IsNotNull(processor.BlockChain);
            Assert.AreEqual(0, processor.BlockChain.BestBlockNumber);
            Assert.AreEqual(genesis, processor.BlockChain.GetBlock(0));
        }

        [TestMethod]
        public void ProcessTwoBlocks()
        {
            Block genesis = new Block(0, null);
            Block block = new Block(1, genesis.Hash);

            BlockProcessor processor = new BlockProcessor();

            processor.Process(genesis);
            processor.Process(block);

            Assert.IsNotNull(processor.BlockChain);
            Assert.AreEqual(1, processor.BlockChain.BestBlockNumber);
            Assert.AreEqual(genesis, processor.BlockChain.GetBlock(0));
            Assert.AreEqual(block, processor.BlockChain.GetBlock(1));
        }
    }
}
