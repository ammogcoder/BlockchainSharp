namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using System.Numerics;
    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;
    using BlockchainSharp.Encoding;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransactionEncoderTests
    {
        [TestMethod]
        public void EncodeDecodeTransaction()
        {
            TransactionEncoder encoder = new TransactionEncoder();
            var sender = new Address();
            var receiver = new Address();

            Transaction transaction = new Transaction(sender, receiver, new BigInteger(100));

            var result = encoder.Decode(encoder.Encode(transaction));

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Receiver);
            Assert.AreEqual(receiver, result.Receiver);

            Assert.IsNotNull(result.Sender);
            Assert.AreEqual(sender, result.Sender);

            Assert.AreEqual(new BigInteger(100), result.Value);
        }
    }
}
