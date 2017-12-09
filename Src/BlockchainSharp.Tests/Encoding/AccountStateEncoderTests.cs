namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Encoding;
    using System.Numerics;
    using BlockchainSharp.Core;

    [TestClass]
    public class AccountStateEncoderTests
    {
        [TestMethod]
        public void EncodeDecodeAccountStateWithBalanceOne()
        {
            AccountStateEncoder encoder = new AccountStateEncoder();

            byte[] bytes = encoder.Encode(new AccountState(BigInteger.One, 0));

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);

            AccountState result = encoder.Decode(bytes);

            Assert.IsNotNull(result);
            Assert.AreEqual(BigInteger.One, result.Balance);
        }
    }
}
