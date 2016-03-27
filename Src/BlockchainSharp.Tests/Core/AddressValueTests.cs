namespace BlockchainSharp.Tests.Core
{
    using System;
    using System.Numerics;
    using BlockchainSharp.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddressValueTests
    {
        [TestMethod]
        public void CreateWithAddressAndValue()
        {
            Address address = new Address();
            BigInteger value = new BigInteger(100);

            var av = new AddressValue(address, value);

            Assert.AreEqual(address, av.Address);
            Assert.AreEqual(value, av.Value);
        }
    }
}
