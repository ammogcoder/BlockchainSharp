namespace BlockchainSharp.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Rlp
    {
        private static byte[] emptyarray = new byte[0];
        private static byte[] empty = new byte[] { 0x80 };

        public static byte[] Decode(byte[] bytes)
        {
            if (bytes != null && bytes.Length == 1 && bytes[0] == 0x80)
                return emptyarray;

            if ((bytes[0] & 0x80) != 0)
            {
                int length;
                byte[] newbytes;

                if (bytes[0] > 183)
                {
                    length = bytes[1];
                    newbytes = new byte[length];
                    Array.Copy(bytes, 2, newbytes, 0, length);
                    return newbytes;
                }

                length = bytes[0] - 0x80;
                newbytes = new byte[length];
                Array.Copy(bytes, 1, newbytes, 0, length);
                return newbytes;
            }

            return bytes;
        }

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

            if (bytes.Length < 256)
            {
                result = new byte[bytes.Length + 2];

                result[0] = (byte)(183 + 1);
                result[1] = (byte)bytes.Length;

                Array.Copy(bytes, 0, result, 2, bytes.Length);

                return result;
            }

            result = new byte[bytes.Length + 3];

            result[0] = (byte)(183 + 2);
            result[1] = (byte)(bytes.Length / 256);
            result[2] = (byte)(bytes.Length % 256);

            Array.Copy(bytes, 0, result, 3, bytes.Length);

            return result;
        }
    }
}
