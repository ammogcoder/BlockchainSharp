namespace BlockchainSharp.Tests.Encoding
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BlockchainSharp.Core;
    using BlockchainSharp.Encoding;

    [TestClass]
    public class AddressEncoderTests
    {
        [TestMethod]
        public void EncodeAndDecodeAddress()
        {
            Address address = new Address();
            AddressEncoder encoder = new AddressEncoder();

            byte[] bytes = encoder.Encode(address);

            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);

            Address result = encoder.Decode(bytes);

            Assert.IsNotNull(result);
            Assert.AreEqual(address, result);
        }
    }
}
