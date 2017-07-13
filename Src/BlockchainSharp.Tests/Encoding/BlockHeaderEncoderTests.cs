namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Encoding;
    using BlockchainSharp.Core;
    using System.Numerics;

    [TestClass]
    public class BlockHeaderEncoderTests
    {
        [TestMethod]
        public void EncodeDecodeBlockHeader()
        {
            BlockHeaderEncoder encoder = new BlockHeaderEncoder();
            Hash hash = new Hash();
            BlockHeader header = new BlockHeader(1, hash);

            var result = encoder.Decode(encoder.Encode(header));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Number);
            Assert.AreEqual(hash, result.ParentHash);
        }
    }
}
