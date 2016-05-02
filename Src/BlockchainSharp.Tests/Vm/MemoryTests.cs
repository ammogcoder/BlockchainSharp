namespace BlockchainSharp.Tests.Vm
{
    using System;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MemoryTests
    {
        [TestMethod]
        public void GetUnitializedBytes()
        {
            var memory = new Memory();

            Assert.AreEqual(0, memory.GetByte(DataWord.Zero));
            Assert.AreEqual(0, memory.GetByte(DataWord.One));
            Assert.AreEqual(0, memory.GetByte(DataWord.Two));
        }

        [TestMethod]
        public void SetAndGetBytes()
        {
            var memory = new Memory();

            memory.PutByte(DataWord.One, 0x12);

            Assert.AreEqual(0, memory.GetByte(DataWord.Zero));
            Assert.AreEqual(0x12, memory.GetByte(DataWord.One));
            Assert.AreEqual(0, memory.GetByte(DataWord.Two));
        }
    }
}
