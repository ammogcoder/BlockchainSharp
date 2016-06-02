namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Linq;
    using System.Numerics;
    using BlockchainSharp.Core;
    using BlockchainSharp.Stores;
    using BlockchainSharp.Tries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransactionProcessorTests
    {
        [TestMethod]
        public void ExecuteTransaction()
        {
            var transaction = CreateTransaction(100);

            var addr1 = transaction.Sender;
            var addr2 = transaction.Receiver;

            var store = new AccountStateStore();
            AccountStateStore newstore = null;

            store = store.Put(addr1, new AccountState(new BigInteger(200)));

            var processor = new TransactionProcessor();

            Assert.IsTrue(processor.ExecuteTransaction(transaction, store, ref newstore));

            Assert.IsNotNull(newstore);
            Assert.AreNotSame(store, newstore);

            Assert.AreEqual(new BigInteger(200), store.Get(addr1).Balance);
            Assert.AreEqual(BigInteger.Zero, store.Get(addr2).Balance);

            Assert.AreEqual(new BigInteger(100), newstore.Get(addr1).Balance);
            Assert.AreEqual(new BigInteger(100), newstore.Get(addr2).Balance);
        }

        [TestMethod]
        public void ExecuteTransactionWithoutFunds()
        {
            var transaction = CreateTransaction(100);

            var addr1 = transaction.Sender;
            var addr2 = transaction.Receiver;

            var store = new AccountStateStore();
            AccountStateStore newstore = null;

            var processor = new TransactionProcessor();

            Assert.IsFalse(processor.ExecuteTransaction(transaction, store, ref newstore));

            Assert.IsNull(newstore);

            Assert.AreEqual(BigInteger.Zero, store.Get(addr1).Balance);
            Assert.AreEqual(BigInteger.Zero, store.Get(addr2).Balance);
        }

        private static Transaction CreateTransaction(int amount)
        {
            Address addr1 = new Address();
            Address addr2 = new Address();
            BigInteger value = new BigInteger(amount);

            return new Transaction(addr1, value, addr2, value);
        }
    }
}
