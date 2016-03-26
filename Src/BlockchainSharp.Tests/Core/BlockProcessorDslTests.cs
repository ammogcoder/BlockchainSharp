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
    }
}
