namespace BlockchainSharp.Tests.Vm
{
    using System;
    using System.Numerics;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DataWordTests
    {
        [TestMethod]
        public void CreateDataWordUsingPositiveInteger()
        {
            var dw = new DataWord(1);

            var result = dw.Data;

            Assert.IsNotNull(result);
            Assert.AreEqual(32, result.Length);

            for (int k = 0; k < 31; k++)
                Assert.AreEqual(0x00, result[k]);

            Assert.AreEqual(0x01, result[31]);
        }

        [TestMethod]
        public void CreateDataWordUsingBytes()
        {
            var dw = new DataWord(new byte[] { 0x01, 0x02 });

            Assert.AreEqual(new BigInteger(258), dw.Value);
        }

        [TestMethod]
        public void CreateNegativeDataWordUsingBytes()
        {
            var dw = new DataWord(new byte[] { 0xff, 0xff });

            Assert.AreEqual(new BigInteger(-1), dw.Value);
        }

        [TestMethod]
        public void CreateDataWordUsingNegativeInteger()
        {
            var dw = new DataWord(-1);

            var result = dw.Data;

            Assert.IsNotNull(result);
            Assert.AreEqual(32, result.Length);

            for (int k = 0; k < 32; k++)
                Assert.AreEqual(0xff, result[k]);
        }

        [TestMethod]
        public void Equals()
        {
            var dw1 = new DataWord(1);
            var dw2 = new DataWord(2);
            var dw1b = new DataWord(1);

            Assert.AreEqual(dw1, dw1);
            Assert.AreEqual(dw1, dw1b);
            Assert.AreEqual(dw1b, dw1);

            Assert.IsFalse(dw1.Equals(null));

            Assert.AreNotEqual(dw1, null);
            Assert.AreNotEqual(dw1, "foo");
            Assert.AreNotEqual(dw1, 42);
            Assert.AreNotEqual(dw1, dw2);

            Assert.AreEqual(dw1.GetHashCode(), dw1b.GetHashCode());
        }

        [TestMethod]
        public void NegatePositiveInteger()
        {
            var dw = new DataWord(3);

            var result = dw.Negate();

            Assert.IsNotNull(result);
            Assert.AreEqual(new DataWord(-3), result);
        }

        [TestMethod]
        public void GetSmallIntegerAsBigInteger()
        {
            var dw = new DataWord(1);

            var result = dw.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(BigInteger.One, result);
        }

        [TestMethod]
        public void AddOneToOne()
        {
            var dw = new DataWord(1);

            var result = dw.Add(dw).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(new BigInteger(2), result);
        }

        [TestMethod]
        public void AddOneToMinusOne()
        {
            var dw = new DataWord(1);
            var dw2 = new DataWord(-1);

            var result = dw.Add(dw2).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(BigInteger.Zero, result);
        }

        [TestMethod]
        public void SubtractOneFromTwo()
        {
            var dw = new DataWord(2);

            var result = dw.Subtract(new DataWord(1)).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(BigInteger.One, result);
        }

        [TestMethod]
        public void SubtractMinusOneFromTwo()
        {
            var dw = new DataWord(2);

            var result = dw.Subtract(new DataWord(-1)).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(new BigInteger(3), result);
        }

        [TestMethod]
        public void MultiplyTwoByThree()
        {
            var dw = new DataWord(2);

            var result = dw.Multiply(new DataWord(3)).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(new BigInteger(6), result);
        }

        [TestMethod]
        public void DivideSixIntoThree()
        {
            var dw = new DataWord(6);

            var result = dw.Divide(new DataWord(3)).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(new BigInteger(2), result);
        }
    }
}
