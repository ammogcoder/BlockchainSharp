namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Linq;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HashTests
    {
        [TestMethod]
        public void CreateWithBytes()
        {
            Hash hash = new Hash();

            Assert.IsNotNull(hash.Bytes);
            Assert.AreEqual(20, hash.Bytes.Length);
            Assert.IsTrue(hash.Bytes.Any(b => b != 0x00));
            Assert.IsTrue(hash.Bytes.Any(b => b != 0xff));
        }
    }
}
