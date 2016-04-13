namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Numerics;
    using System.Linq;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void CreateTransactionWithTotals()
        {
            Transaction transaction = new Transaction(
                new AddressValue[] 
                {
                    new AddressValue(new Address(), new BigInteger(100))
                },
                new AddressValue[] 
                {
                    new AddressValue(new Address(), new BigInteger(50)),
                    new AddressValue(new Address(), new BigInteger(40))
                });

            Assert.IsNotNull(transaction.Inputs);
            Assert.AreEqual(1, transaction.Inputs.Count());

            Assert.IsNotNull(transaction.Outputs);
            Assert.AreEqual(2, transaction.Outputs.Count());

            Assert.AreEqual(new BigInteger(100), transaction.InputsTotal);
            Assert.AreEqual(new BigInteger(90), transaction.OutputsTotal);
        }

        [TestMethod]
        public void RejectTransactionWithOutputTotalsGreaterThanInputTotals()
        {
            try
            {
                new Transaction(
                    new AddressValue[] 
                    {
                        new AddressValue(new Address(), new BigInteger(100))
                    }, 
                    new AddressValue[] 
                    {
                        new AddressValue(new Address(), new BigInteger(50)),
                        new AddressValue(new Address(), new BigInteger(60))
                    });

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.AreEqual("Transaction outputs are greater than inputs", ex.Message);
            }
        }
    }
}
