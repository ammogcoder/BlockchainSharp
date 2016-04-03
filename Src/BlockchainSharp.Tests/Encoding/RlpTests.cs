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
    }
}
