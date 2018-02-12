namespace BlockchainSharp.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;

    public class AddressEncoder
    {
        private static AddressEncoder instance = new AddressEncoder();

        public static AddressEncoder Instance { get { return instance; } }

        public byte[] Encode(Address address)
        {
            return Rlp.Encode(address.Bytes);
        }

        public Address Decode(byte[] bytes)
        {
            byte[] addr = Rlp.Decode(bytes);

            return new Address(addr);
        }
    }
}
