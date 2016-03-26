namespace BlockchainSharp.Tests.Core
{
    using System;
    using BlockchainSharp.Core;
    using BlockchainSharp.Tests.Dsl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockProcessorDslTests
    {
        [TestMethod]
        public void CreateDsl()
        {
            var processor = new BlockProcessor();
            var dsl = new BlockProcessorDsl(processor);

            Assert.IsNotNull(processor.BlockChain);
            Assert.IsNotNull(processor.BlockChain.BestBlock);
            Assert.IsTrue(processor.BlockChain.BestBlock.IsGenesis);
            Assert.AreEqual(0, processor.BlockChain.BestBlockNumber);
        }

        [TestMethod]
        public void SendBlock()
        {
            var processor = new BlockProcessor();
            var dsl = new BlockProcessorDsl(processor);

            dsl.Run(new string[] {
                "chain g0 b1",
                "send b1",
                "top b1"
            });
        }

        [TestMethod]
        public void SendTwoBlocks()
        {
            var processor = new BlockProcessor();
            var dsl = new BlockProcessorDsl(processor);

            dsl.Run(new string[] {
                "chain g0 b1 b2",
                "send b1 b2",
                "top b2"
            });
        }

        [TestMethod]
        public void SendTwoBlocksInReversedOrder()
        {
            var processor = new BlockProcessor();
            var dsl = new BlockProcessorDsl(processor);

            dsl.Run(new string[] {
                "chain g0 b1 b2",
                "send b2 b1",
                "top b2"
            });
        }

        [TestMethod]
        public void SendTwoBlocksAndTwoUncles()
        {
            var processor = new BlockProcessor();
            var dsl = new BlockProcessorDsl(processor);

            dsl.Run(new string[] {
                "chain g0 b1 b2",
                "chain b1 c2 c3",
                "send b1 b2",
                "send c2 c3",
                "top c3"
            });
        }
    }
}
