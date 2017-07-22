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

            Transaction transaction = new Transaction(sender, receiver, new BigInteger(100));

            Assert.IsNotNull(transaction.Receiver);
            Assert.AreEqual(receiver, transaction.Receiver);

            Assert.IsNotNull(transaction.Sender);
            Assert.AreEqual(sender, transaction.Sender);

            Assert.AreEqual(new BigInteger(100), transaction.Value);
        }

        [TestMethod]
        public void RejectTransactionWithNegativeValue()
        {
            var sender = new Address();
            var receiver = new Address();

            try
            {
                new Transaction(sender, receiver, new BigInteger(-100));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.AreEqual("Transaction value is negative", ex.Message);
            }
        }
    }
}
