namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Core;

    [TestClass]
    public class BlockInfoTests
    {
        [TestMethod]
        public void CreateBlockInfo()
        {
            Block block = new Block(0, null);
            AccountsState state = new AccountsState();

            BlockInfo binfo = new BlockInfo(block, state);

            Assert.AreSame(block, binfo.Block);
            Assert.AreSame(state, binfo.State);
        }
    }
}
