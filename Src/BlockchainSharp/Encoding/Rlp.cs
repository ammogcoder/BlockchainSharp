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

            byte[] result;

            if (bytes.Length < 56)
            {
                result = new byte[bytes.Length + 1];

                result[0] = (byte)(bytes.Length + 128);

                Array.Copy(bytes, 0, result, 1, bytes.Length);

                return result;
            }

            result = new byte[bytes.Length + 2];

            result[0] = (byte)(183 + 1);
            result[1] = (byte)bytes.Length;

            Array.Copy(bytes, 0, result, 2, bytes.Length);

            return result;
        }
    }
}
