namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Linq;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void CreateWithRandomBytes()
        {
            Address address = new Address();

            Assert.IsNotNull(address.Bytes);
            Assert.AreEqual(20, address.Bytes.Length);
            Assert.IsTrue(address.Bytes.Any(b => b != 0x00));
            Assert.IsTrue(address.Bytes.Any(b => b != 0xff));
        }

        [TestMethod]
        public void CreateWithBytes()
        {
            Address address = new Address(new byte[] { 0x00, 0x01, 0x02, 0x03 });

            Assert.IsNotNull(address.Bytes);
            Assert.AreEqual(4, address.Bytes.Length);

            for (int k = 0; k < address.Bytes.Length; k++)
                Assert.AreEqual((byte)k, address.Bytes[k]);
        }

        [TestMethod]
        public void AddressToString()
        {
            Assert.AreEqual("00010203", new Address(new byte[] { 0x00, 0x01, 0x02, 0x03 }).ToString());
            Assert.AreEqual("abcdef03", new Address(new byte[] { 0xab, 0xcd, 0xef, 0x03 }).ToString());
        }

        [TestMethod]
        public void Equals()
        {
            Address address1 = new Address(new byte[] { 0x00, 0x01, 0x02, 0x03 });
            Address address2 = new Address(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x00 });
            Address address3 = new Address(new byte[] { 0x00, 0x01, 0x02, 0x02 });
            Address address4 = new Address(new byte[] { 0x00, 0x01, 0x02, 0x03 });

            Assert.IsTrue(address1.Equals(address1));
            Assert.IsTrue(address1.Equals(address4));
            Assert.IsFalse(address1.Equals(address2));
            Assert.IsFalse(address1.Equals(address3));
            Assert.IsFalse(address1.Equals(null));
            Assert.IsFalse(address1.Equals(42));
            Assert.IsFalse(address1.Equals("foo"));

            Assert.AreEqual(address1.GetHashCode(), address4.GetHashCode());
        }
    }
}
