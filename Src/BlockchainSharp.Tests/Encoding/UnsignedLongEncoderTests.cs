namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Encoding;
    using System.Numerics;

    [TestClass]
    public class UnsignedLongEncoderTests
    {
        [TestMethod]
        public void EncodeDecodeZero()
        {
            byte[] bytes = UnsignedLongEncoder.Instance.Encode(0);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);
            Assert.AreEqual(1, bytes.Length);

            ulong result = UnsignedLongEncoder.Instance.Decode(bytes);

            Assert.AreEqual(0ul, result);
        }

        [TestMethod]
        public void EncodeDecodeOne()
        {
            byte[] bytes = UnsignedLongEncoder.Instance.Encode(1);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);
            Assert.AreEqual(1, bytes.Length);

            ulong result = UnsignedLongEncoder.Instance.Decode(bytes);

            Assert.AreEqual(1ul, result);
        }

        [TestMethod]
        public void EncodeDecode255()
        {
            byte[] bytes = UnsignedLongEncoder.Instance.Encode(255);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);
            Assert.AreEqual(2, bytes.Length);

            ulong result = UnsignedLongEncoder.Instance.Decode(bytes);

            Assert.AreEqual(255ul, result);
        }

        [TestMethod]
        public void EncodeDecodeMaximumValue()
        {
            byte[] bytes = UnsignedLongEncoder.Instance.Encode(ulong.MaxValue);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);
            Assert.AreEqual(9, bytes.Length);

            ulong result = UnsignedLongEncoder.Instance.Decode(bytes);

            Assert.AreEqual(ulong.MaxValue, result);
        }
    }
}
