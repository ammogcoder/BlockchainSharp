namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Encoding;
    using System.Numerics;

    [TestClass]
    public class BigIntegerEncoderTests
    {
        [TestMethod]
        public void EncodeDecodeBigIntegerOne()
        {
            BigIntegerEncoder encoder = new BigIntegerEncoder();

            byte[] bytes = encoder.Encode(BigInteger.One);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);

            BigInteger result = encoder.Decode(bytes);

            Assert.AreEqual(BigInteger.One, result);
        }
    }
}
