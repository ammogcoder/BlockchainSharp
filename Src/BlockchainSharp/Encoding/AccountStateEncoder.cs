namespace BlockchainSharp.Encoding
{
    using BlockchainSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AccountStateEncoder
    {
        private static BigIntegerEncoder bigIntegerEncoder = new BigIntegerEncoder();

        public byte[] Encode(AccountState state)
        {
            return Rlp.EncodeList(bigIntegerEncoder.Encode(state.Balance));
        }

        public AccountState Decode(byte[] bytes)
        {
            IList<byte[]> list = Rlp.DecodeList(bytes);

            return new AccountState(bigIntegerEncoder.Decode(list[0]), 0);
        }
    }
}
