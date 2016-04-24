namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Linq;
    using System.Numerics;
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

        [TestMethod]
        public void ExecuteTransaction()
        {
            var transaction = CreateTransaction(100);

            var addr1 = transaction.Inputs.First().Address;
            var addr2 = transaction.Outputs.First().Address;

            var states = new Trie<AccountState>(new AccountState(BigInteger.Zero));

            states = states.Put(addr1.ToString(), new AccountState(new BigInteger(200)));

            var processor = new TransactionProcessor(states);

            Assert.IsTrue(processor.ExecuteTransaction(transaction));

            var newstates = processor.States;

            Assert.IsNotNull(newstates);
            Assert.AreNotSame(states, newstates);

            Assert.AreEqual(new BigInteger(200), states.Get(addr1.ToString()).Balance);
            Assert.AreEqual(BigInteger.Zero, states.Get(addr2.ToString()).Balance);

            Assert.AreEqual(new BigInteger(100), newstates.Get(addr1.ToString()).Balance);
            Assert.AreEqual(new BigInteger(100), newstates.Get(addr2.ToString()).Balance);
        }

        private static Transaction CreateTransaction(int amount)
        {
            Address addr1 = new Address();
            Address addr2 = new Address();
            BigInteger value = new BigInteger(amount);

            AddressValue av1 = new AddressValue(addr1, value);
            AddressValue av2 = new AddressValue(addr2, value);

            return new Transaction(new AddressValue[] { av1 }, new AddressValue[] { av2 });
        }
    }
}
