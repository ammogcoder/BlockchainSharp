namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
