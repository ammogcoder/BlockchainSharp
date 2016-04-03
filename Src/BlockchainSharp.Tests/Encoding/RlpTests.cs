namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using BlockchainSharp.Encoding;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RlpTests
    {
        [TestMethod]
        public void EncodeSingleLowByte()
        {
            var result = Rlp.Encode(new byte[] { 0x01 });

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(0x01, result[0]);
        }

        [TestMethod]
        public void EncodeSingleLowBytes()
        {
            for (var k = 0; k < 128; k++)
            {
                var result = Rlp.Encode(new byte[] { (byte)k });

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(k, result[0]);
            }
        }

        [TestMethod]
        public void EncodeSingleHighBytes()
        {
            for (var k = 128; k < 256; k++)
            {
                var result = Rlp.Encode(new byte[] { (byte)k });

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(0x81, result[0]);
                Assert.AreEqual(k, result[1]);
            }
        }

        [TestMethod]
        public void EncodeEmptyByteArray()
        {
            var result = Rlp.Encode(new byte[0]);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(0x80, result[0]);
        }

        [TestMethod]
        public void EncodeNullByteArray()
        {
            var result = Rlp.Encode((byte[])null);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(0x80, result[0]);
        }
    }
}
