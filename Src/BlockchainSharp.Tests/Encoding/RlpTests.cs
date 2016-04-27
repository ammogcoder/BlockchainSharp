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
        public void DecodeSingleLowBytes()
        {
            for (var k = 0; k < 128; k++)
            {
                var result = Rlp.Decode(new byte[] { (byte)k });

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
        public void DecodeSingleHighBytes()
        {
            for (var k = 128; k < 256; k++)
            {
                var result = Rlp.Decode(new byte[] { 0x81, (byte)k });

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(k, result[0]);
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
        public void DecodeBytesShortLength()
        {
            for (var k = 2; k < 56; k++)
            {
                var bytes = new byte[k + 1];

                bytes[0] = (byte)(k + 0x80);

                for (int j = 0; j < k; j++)
                    bytes[j + 1] = (byte)j;

                var result = Rlp.Decode(bytes);

                Assert.IsNotNull(result);
                Assert.AreEqual(k, result.Length);

                for (var j = 0; j < k; j++)
                    Assert.AreEqual(j, result[j]);
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
        public void DecodeBytesOneByteLength()
        {
            for (var k = 56; k < 256; k++)
            {
                var bytes = new byte[k + 2];

                bytes[0] = 183 + 1;
                bytes[1] = (byte)k;

                for (int j = 0; j < k; j++)
                    bytes[j + 2] = (byte)(j % 256);

                var result = Rlp.Decode(bytes);

                Assert.IsNotNull(result);
                Assert.AreEqual(k, result.Length);

                for (var j = 0; j < k; j++)
                    Assert.AreEqual(j % 256, result[j]);
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
        public void DecodeBytesTwoBytesLength()
        {
            for (var k = 256; k < 1000; k++)
            {
                var bytes = new byte[k + 3];

                bytes[0] = 183 + 2;
                bytes[1] = (byte)(1000 >> 8);
                bytes[2] = 1000 % 256;

                for (int j = 0; j < k; j++)
                    bytes[j + 3] = (byte)(j % 256);

                var result = Rlp.Decode(bytes);

                Assert.IsNotNull(result);
                Assert.AreEqual(k, result.Length);

                for (var j = 0; j < k; j++)
                    Assert.AreEqual(j % 256, result[j]);
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
