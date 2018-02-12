namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using System.Numerics;
    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;
    using BlockchainSharp.Encoding;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
