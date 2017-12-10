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
            var state = new AccountState(BigInteger.One, 0);

            Assert.AreEqual(BigInteger.One, state.Balance);
            Assert.AreEqual(0ul, state.Nonce);
        }

        [TestMethod]
        public void CreateWithNonce()
        {
            var state = new AccountState(BigInteger.Zero, 42);

            Assert.AreEqual(BigInteger.Zero, state.Balance);
            Assert.AreEqual(42ul, state.Nonce);
        }

        [TestMethod]
        public void AddToBalance()
        {
            var state = new AccountState(BigInteger.One, 0);

            var result = state.AddToBalance(BigInteger.MinusOne);

            Assert.IsNotNull(result);
            Assert.AreNotSame(state, result);
            Assert.AreEqual(BigInteger.Zero, result.Balance);
            Assert.AreEqual(0ul, result.Nonce);
        }

        [TestMethod]
        public void SubtractFromBalance()
        {
            var state = new AccountState(BigInteger.One, 42);

            var result = state.SubtractFromBalance(BigInteger.One);

            Assert.IsNotNull(result);
            Assert.AreNotSame(state, result);
            Assert.AreEqual(BigInteger.Zero, result.Balance);
            Assert.AreEqual(42ul, result.Nonce);
        }

        [TestMethod]
        public void NegativeBalance()
        {
            var state = new AccountState(BigInteger.Zero, 0);

            try
            {
                state.SubtractFromBalance(BigInteger.One);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.AreEqual("Invalid balance", ex.Message);
            }
        }
    }
}
