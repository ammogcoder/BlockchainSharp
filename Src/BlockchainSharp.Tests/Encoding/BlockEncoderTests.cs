namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Encoding;
    using BlockchainSharp.Core;
    using System.Numerics;

    [TestClass]
    public class BlockEncoderTests
    {
        [TestMethod]
        public void EncodeDecodeBlock()
        {
            BlockEncoder encoder = new BlockEncoder();
            Hash hash = new Hash();
            Block block = new Block(1, hash);

            var result = encoder.Decode(encoder.Encode(block));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Number);
            Assert.AreEqual(hash, result.ParentHash);
        }
    }
}
