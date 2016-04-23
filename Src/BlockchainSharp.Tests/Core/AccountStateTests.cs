namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Numerics;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccountStateTests
    {
        [TestMethod]
        public void CreateWithBalance()
        {
            var state = new AccountState(BigInteger.One);

            Assert.AreEqual(BigInteger.One, state.Balance);
        }
    }
}
