namespace BlockchainSharp.Encoding
{
    using BlockchainSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class TransactionEncoder
    {
        private AddressEncoder addressEncoder = new AddressEncoder();

        public byte[] Encode(Transaction tx)
        {
            byte[] sender = AddressEncoder.Instance.Encode(tx.Sender);
            byte[] receiver = AddressEncoder.Instance.Encode(tx.Receiver);
            byte[] value = BigIntegerEncoder.Instance.Encode(tx.Value);
                
            return Rlp.EncodeList(sender, receiver, value);
        }

        public Transaction Decode(byte[] bytes)
        {
            IList<byte[]> items = Rlp.DecodeList(bytes);

            Address sender = AddressEncoder.Instance.Decode(items[0]);
            Address receiver = AddressEncoder.Instance.Decode(items[1]);
            BigInteger value = BigIntegerEncoder.Instance.Decode(items[2]);

            return new Transaction(sender, receiver, value);
        }
    }
}
