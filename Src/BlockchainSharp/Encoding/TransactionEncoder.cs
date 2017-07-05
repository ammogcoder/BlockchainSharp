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
            byte[] senderValue = bigIntegerEncoder.Encode(tx.SenderValue);
            byte[] receiverValue = bigIntegerEncoder.Encode(tx.ReceiverValue);
                
            return Rlp.EncodeList(sender, senderValue, receiver, receiverValue);
        }

        public Transaction Decode(byte[] bytes)
        {
            IList<byte[]> items = Rlp.DecodeList(bytes);

            Address sender = addressEncoder.Decode(items[0]);
            BigInteger senderValue = bigIntegerEncoder.Decode(items[1]);
            Address receiver = addressEncoder.Decode(items[2]);
            BigInteger receiverValue = bigIntegerEncoder.Decode(items[3]);

            return new Transaction(sender, senderValue, receiver, receiverValue);
        }
    }
}
