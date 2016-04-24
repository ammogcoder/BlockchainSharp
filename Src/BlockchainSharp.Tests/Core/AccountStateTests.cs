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

        [TestMethod]
        public void AddToBalance()
        {
            var state = new AccountState(BigInteger.One);

            var result = state.AddToBalance(BigInteger.MinusOne);

            Assert.IsNotNull(result);
            Assert.AreNotSame(state, result);
            Assert.AreEqual(BigInteger.Zero, result.Balance);
        }

        [TestMethod]
        public void SubtractFromBalance()
        {
            var state = new AccountState(BigInteger.One);

            var result = state.SubtractFromBalance(BigInteger.One);

            Assert.IsNotNull(result);
            Assert.AreNotSame(state, result);
            Assert.AreEqual(BigInteger.Zero, result.Balance);
        }
    }
}
