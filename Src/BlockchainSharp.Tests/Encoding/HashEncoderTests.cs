namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Core;
    using BlockchainSharp.Encoding;

    [TestClass]
    public class HashEncoderTests
    {
        [TestMethod]
        public void EncodeAndDecodeHash()
        {
            Hash hash = new Hash();
            HashEncoder encoder = new HashEncoder();

            byte[] bytes = encoder.Encode(hash);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);

            Hash result = encoder.Decode(bytes);

            Assert.IsNotNull(result);
            Assert.AreEqual(hash, result);
        }
    }
}
