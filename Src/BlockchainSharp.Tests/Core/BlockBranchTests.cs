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
    }
}
