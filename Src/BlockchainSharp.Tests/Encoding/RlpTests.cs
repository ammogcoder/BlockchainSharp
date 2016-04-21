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
        public void DecodeSingleLowByte()
        {
            var result = Rlp.Decode(new byte[] { 0x01 });

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
        public void EncodeBytesShortLength()
        {
            for (var k = 2; k < 56; k++)
            {
                var bytes = new byte[k];

                for (int j = 0; j < k; j++)
                    bytes[j] = (byte)j;

                var result = Rlp.Encode(bytes);

                Assert.IsNotNull(result);
                Assert.AreEqual(k + 1, result.Length);
                Assert.AreEqual(0x80 + k, result[0]);

                for (var j = 0; j < k; j++)
                    Assert.AreEqual(j, result[j + 1]);
            }
        }

        [TestMethod]
        public void EncodeBytesOneByteLength()
        {
            for (var k = 56; k < 256; k++)
            {
                var bytes = new byte[k];

                for (int j = 0; j < k; j++)
                    bytes[j] = (byte)(j % 256);

                var result = Rlp.Encode(bytes);

                Assert.IsNotNull(result);
                Assert.AreEqual(k + 2, result.Length);
                Assert.AreEqual(183 + 1, result[0]);
                Assert.AreEqual(k, result[1]);

                for (var j = 0; j < k; j++)
                    Assert.AreEqual(j % 256, result[j + 2]);
            }
        }

        [TestMethod]
        public void EncodeBytesTwoBytesLength()
        {
            for (var k = 256; k < 1000; k++)
            {
                var bytes = new byte[k];

                for (int j = 0; j < k; j++)
                    bytes[j] = (byte)(j % 256);

                var result = Rlp.Encode(bytes);

                Assert.IsNotNull(result);
                Assert.AreEqual(k + 3, result.Length);
                Assert.AreEqual(183 + 2, result[0]);
                Assert.AreEqual(k, (result[1] << 8) + result[2]);

                for (var j = 0; j < k; j++)
                    Assert.AreEqual(j % 256, result[j + 3]);
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

        [TestMethod]
        public void DecodeToEmptyArray()
        {
            var result = Rlp.Decode(new byte[] { 0x80 });

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }
    }
}
