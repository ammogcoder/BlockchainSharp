namespace BlockchainSharp.Encoding
{
    using BlockchainSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AccountStateEncoder
    {
        public byte[] Encode(AccountState state)
        {
            return Rlp.EncodeList(BigIntegerEncoder.Instance.Encode(state.Balance), UnsignedLongEncoder.Instance.Encode(state.Nonce));
        }

        public AccountState Decode(byte[] bytes)
        {
            IList<byte[]> list = Rlp.DecodeList(bytes);

            return new AccountState(BigIntegerEncoder.Instance.Decode(list[0]), UnsignedLongEncoder.Instance.Decode(list[1]));
        }
    }
}
