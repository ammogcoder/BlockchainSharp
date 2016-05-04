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
        public void GetUnitializedDataWords()
        {
            var memory = new Memory();

            Assert.AreEqual(DataWord.Zero, memory.GetDataWord(DataWord.Zero));
            Assert.AreEqual(DataWord.Zero, memory.GetDataWord(DataWord.One));
            Assert.AreEqual(DataWord.Zero, memory.GetDataWord(DataWord.Two));
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

        [TestMethod]
        public void GetDataWord()
        {
            var memory = new Memory();

            memory.PutByte(new DataWord(30), 0x01);

            Assert.AreEqual(new DataWord(256), memory.GetDataWord(DataWord.Zero));
        }

        [TestMethod]
        public void PutBytes()
        {
            var memory = new Memory();

            memory.PutBytes(new DataWord(30), new byte[] { 0x01, 0x02, 0x03 });

            Assert.AreEqual(0x01, memory.GetByte(new DataWord(30)));
            Assert.AreEqual(0x02, memory.GetByte(new DataWord(31)));
            Assert.AreEqual(0x03, memory.GetByte(new DataWord(32)));
        }
    }
}

