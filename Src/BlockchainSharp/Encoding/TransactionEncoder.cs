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
        private BigIntegerEncoder bigIntegerEncoder = new BigIntegerEncoder();

        public byte[] Encode(Transaction tx)
        {
            byte[] sender = addressEncoder.Encode(tx.Sender);
            byte[] receiver = addressEncoder.Encode(tx.Receiver);
            byte[] value = bigIntegerEncoder.Encode(tx.Value);
                
            return Rlp.EncodeList(sender, receiver, value);
        }

        public Transaction Decode(byte[] bytes)
        {
            IList<byte[]> items = Rlp.DecodeList(bytes);

            Address sender = addressEncoder.Decode(items[0]);
            Address receiver = addressEncoder.Decode(items[1]);
            BigInteger value = bigIntegerEncoder.Decode(items[2]);

            return new Transaction(sender, receiver, value);
        }
    }
}
