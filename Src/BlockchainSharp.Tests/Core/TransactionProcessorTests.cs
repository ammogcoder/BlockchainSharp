namespace BlockchainSharp.Tests.Core
{
    using System;
    using BlockchainSharp.Core;
    using BlockchainSharp.Tries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransactionProcessorTests
    {
        [TestMethod]
        public void CreateWithAccountStates()
        {
            var states = new Trie<AccountState>();
            var tp = new TransactionProcessor(states);

            Assert.IsNotNull(tp.States);
            Assert.AreSame(states, tp.States);
        }
    }
}
