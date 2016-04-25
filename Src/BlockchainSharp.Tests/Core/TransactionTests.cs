namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Linq;
    using System.Numerics;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void CreateTransaction()
        {
            var sender = new Address();
            var receiver = new Address();

            Transaction transaction = new Transaction(sender, new BigInteger(100), receiver, new BigInteger(90));

            Assert.IsNotNull(transaction.Receiver);
            Assert.AreEqual(receiver, transaction.Receiver);

            Assert.IsNotNull(transaction.Sender);
            Assert.AreEqual(sender, transaction.Sender);

            Assert.AreEqual(new BigInteger(100), transaction.SenderValue);
            Assert.AreEqual(new BigInteger(90), transaction.ReceiverValue);
        }

        [TestMethod]
        public void RejectTransactionWithReceiverValueGreaterThanSenderValue()
        {
            var sender = new Address();
            var receiver = new Address();

            try
            {
                new Transaction(sender, new BigInteger(100), receiver, new BigInteger(110));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.AreEqual("Transaction receiver value is greater than sender value", ex.Message);
            }
        }
    }
}
