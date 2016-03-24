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
        public void CreateWithRandomBytes()
        {
            Hash hash = new Hash();

            Assert.IsNotNull(hash.Bytes);
            Assert.AreEqual(20, hash.Bytes.Length);
            Assert.IsTrue(hash.Bytes.Any(b => b != 0x00));
            Assert.IsTrue(hash.Bytes.Any(b => b != 0xff));
        }

        [TestMethod]
        public void CreateWithBytes()
        {
            Hash hash = new Hash(new byte[] { 0x00, 0x01, 0x02, 0x03 });

            Assert.IsNotNull(hash.Bytes);
            Assert.AreEqual(4, hash.Bytes.Length);

            for (int k = 0; k < hash.Bytes.Length; k++)
                Assert.AreEqual((byte)k, hash.Bytes[k]);
        }

        [TestMethod]
        public void Equals()
        {
            Hash hash1 = new Hash(new byte[] { 0x00, 0x01, 0x02, 0x03 });
            Hash hash2 = new Hash(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x00 });
            Hash hash3 = new Hash(new byte[] { 0x00, 0x01, 0x02, 0x02 });
            Hash hash4 = new Hash(new byte[] { 0x00, 0x01, 0x02, 0x03 });

            Assert.IsTrue(hash1.Equals(hash1));
            Assert.IsTrue(hash1.Equals(hash4));
            Assert.IsFalse(hash1.Equals(hash2));
            Assert.IsFalse(hash1.Equals(hash3));
            Assert.IsFalse(hash1.Equals(null));
            Assert.IsFalse(hash1.Equals(42));
            Assert.IsFalse(hash1.Equals("foo"));

            Assert.AreEqual(hash1.GetHashCode(), hash4.GetHashCode());
        }
    }
}
