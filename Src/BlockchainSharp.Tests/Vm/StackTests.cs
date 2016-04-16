namespace BlockchainSharp.Tests.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void PushByteAndPopDataWord()
        {
            var stack = new Stack();

            stack.Push(new DataWord(new byte[] { 0x01 }));

            var result = stack.Pop();

            Assert.IsNotNull(result);
            Assert.AreEqual(new DataWord(1), result);
        }

        [TestMethod]
        public void PushTwoBytesAndPopDataWord()
        {
            var stack = new Stack();

            stack.Push(new DataWord(new byte[] { 0x01, 0x00 }));

            var result = stack.Pop();

            Assert.IsNotNull(result);
            Assert.AreEqual(new DataWord(256), result);
        }

        [TestMethod]
        public void ElementAt()
        {
            var stack = new Stack();

            stack.Push(DataWord.Zero);
            stack.Push(DataWord.One);
            stack.Push(DataWord.Two);
            stack.Push(DataWord.Three);

            Assert.AreEqual(DataWord.Zero, stack.ElementAt(3));
            Assert.AreEqual(DataWord.One, stack.ElementAt(2));
            Assert.AreEqual(DataWord.Two, stack.ElementAt(1));
            Assert.AreEqual(DataWord.Three, stack.ElementAt(0));
        }

        [TestMethod]
        public void Swap()
        {
            var stack = new Stack();

            stack.Push(DataWord.Zero);
            stack.Push(DataWord.One);
            stack.Push(DataWord.Two);
            stack.Push(DataWord.Three);

            stack.Swap(1);

            Assert.AreEqual(DataWord.Zero, stack.ElementAt(3));
            Assert.AreEqual(DataWord.One, stack.ElementAt(2));
            Assert.AreEqual(DataWord.Three, stack.ElementAt(1));
            Assert.AreEqual(DataWord.Two, stack.ElementAt(0));

            stack.Swap(2);

            Assert.AreEqual(DataWord.Zero, stack.ElementAt(3));
            Assert.AreEqual(DataWord.Two, stack.ElementAt(2));
            Assert.AreEqual(DataWord.Three, stack.ElementAt(1));
            Assert.AreEqual(DataWord.One, stack.ElementAt(0));

            stack.Swap(3);

            Assert.AreEqual(DataWord.One, stack.ElementAt(3));
            Assert.AreEqual(DataWord.Two, stack.ElementAt(2));
            Assert.AreEqual(DataWord.Three, stack.ElementAt(1));
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(0));
        }
    }
}
