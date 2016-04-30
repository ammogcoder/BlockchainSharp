namespace BlockchainSharp.Tests.Core
{
    using System;
    using BlockchainSharp.Core;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StorageStateTests
    {
        [TestMethod]
        public void GetZeroValue()
        {
            var state = new StorageState();

            Assert.AreEqual(DataWord.Zero, state.Get(DataWord.One));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            var state = new StorageState();

            state.Put(DataWord.One, new DataWord(42));

            Assert.AreEqual(DataWord.Zero, state.Get(DataWord.Zero));
            Assert.AreEqual(new DataWord(42), state.Get(DataWord.One));
        }
    }
}
