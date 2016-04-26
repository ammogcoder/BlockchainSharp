namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Linq;
    using System.Numerics;
    using BlockchainSharp.Core;
    using BlockchainSharp.Tries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Stores;

    [TestClass]
    public class TransactionProcessorTests
    {
        [TestMethod]
        public void CreateWithAccountStore()
        {
            var store = new AccountStateStore();
            var tp = new TransactionProcessor(store);

            Assert.IsNotNull(tp.Accounts);
            Assert.AreSame(store, tp.Accounts);
        }

        [TestMethod]
        public void ExecuteTransaction()
        {
            var transaction = CreateTransaction(100);

            var addr1 = transaction.Sender;
            var addr2 = transaction.Receiver;

            var store = new AccountStateStore();

            store.Put(addr1, new AccountState(new BigInteger(200)));

            var processor = new TransactionProcessor(store);

            Assert.IsTrue(processor.ExecuteTransaction(transaction));

            var newstore = processor.Accounts;

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

            var accounts = new AccountStateStore();

            var processor = new TransactionProcessor(accounts);

            Assert.IsFalse(processor.ExecuteTransaction(transaction));

            var newaccounts = processor.Accounts;

            Assert.IsNotNull(newaccounts);
            Assert.AreSame(accounts, newaccounts);

            Assert.AreEqual(BigInteger.Zero, accounts.Get(addr1).Balance);
            Assert.AreEqual(BigInteger.Zero, accounts.Get(addr2).Balance);
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
