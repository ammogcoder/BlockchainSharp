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
                int length = GetLength(bytes, 0);
                int offset = GetOffset(bytes, 0);
                byte[] newbytes;
                newbytes = new byte[length];
                Array.Copy(bytes, offset, newbytes, 0, length);

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

        public static IList<byte[]> DecodeList(byte[] bytes)
        {
            IList<byte[]> items = new List<byte[]>();
            int totallength = bytes[0] - 192 + 1;

            int position = 1;

            while (position < totallength)
            {
                int offset = GetOffset(bytes, position);
                int length = GetLength(bytes, position);

                byte[] item = new byte[offset + length];
                Array.Copy(bytes, position, item, 0, item.Length);

                items.Add(item);

                position += item.Length;
            }

            return items;
        }

        public static byte[] EncodeList(params byte[][] bytes)
        {
            int totallength = bytes.Sum(b => b.Length);
            int resultlength = totallength + 1;
            int offset = 1;

            if (totallength > 55)
            {
                resultlength++;
                offset++;
            }

            byte[] result = new byte[resultlength];

            foreach (byte[] bs in bytes) {
                Array.Copy(bs, 0, result, offset, bs.Length);
                offset += bs.Length;
            }

            if (totallength > 55)
            {
                result[0] = 247 + 1;
                result[1] = (byte)(totallength);
            }
            else
                result[0] = (byte)(totallength + 192);
            
            return result;
        }

        private static int GetOffset(byte[] bytes, int position)
        {
            var b0 = bytes[position];

            if (b0 < 128)
                return 0;

            if (b0 <= 183)
                return 1;

            return b0 - 183 + 1;
        }

        private static int GetLength(byte[] bytes, int position)
        {
            var b0 = bytes[position];

            if (b0 < 128)
                return 1;

            if (b0 > 183)
            {
                var nb = b0 - 183;
                int length = 0;

                for (int k = 0; k < nb; k++)
                {
                    length <<= 8;
                    length += bytes[position + k + 1];
                }

                return length;
            }
            else
                return b0 - 0x80;
        }
    }
}
