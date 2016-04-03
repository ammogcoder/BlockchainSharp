namespace BlockchainSharp.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Rlp
    {
        private static byte[] empty = new byte[] { 0x80 };

        public static byte[] Encode(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return empty;

            if (bytes.Length == 1 && bytes[0] < 0x80)
                return bytes;

            var result = new byte[bytes.Length + 1];

            result[0] = (byte)(bytes.Length + 128);

            Array.Copy(bytes, 0, result, 1, bytes.Length);

            return result;
        }
    }
}
