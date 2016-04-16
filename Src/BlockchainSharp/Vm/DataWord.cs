namespace BlockchainSharp.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class DataWord
    {
        public static DataWord Zero = new DataWord(0);
        public static DataWord One = new DataWord(1);
        public static DataWord Two = new DataWord(2);
        public static DataWord Three = new DataWord(3);

        private byte[] data;

        public DataWord(int num)
        {
            this.data = new byte[32];
            int value = num;

            for (int k = 0; k < 4; k++)
            {
                this.data[k] = (byte)(value & 0x00ff);
                value >>= 8;
            }

            if (num < 0)
                for (int k = 4; k < 32; k++)
                    this.data[k] = (byte)0xff;
        }

        public DataWord(byte[] bytes)
            : this(bytes, 0, bytes.Length)
        {
        }

        public DataWord(byte[] bytes, int offset, int length)
        {
            this.data = new byte[32];

            for (int k = 0; k < length; k++)
                this.data[k] = bytes[length - k - 1 + offset];

            if ((bytes[0] & 0x80) != 0)
                for (int k = length; k < 32; k++)
                    this.data[k] = 0xff;
        }

        private DataWord(byte[] bytes, bool inverse)
        {
            this.data = new byte[32];

            Array.Copy(bytes, 0, this.data, 0, bytes.Length);

            if ((bytes[bytes.Length - 1] & 0x80) != 0)
                for (int k = bytes.Length; k < 32; k++)
                    this.data[k] = 0xff;
        }

        public DataWord(BigInteger value)
            : this(value.ToByteArray(), true)
        {
        }

        public byte[] Data { get { return this.data.Reverse().ToArray(); } }

        public BigInteger Value { get { return new BigInteger(this.data); } }

        public DataWord Negate()
        {
            return new DataWord(BigInteger.Negate(this.Value));
        }

        public DataWord Add(DataWord dw)
        {
            return new DataWord(BigInteger.Add(this.Value, dw.Value));
        }

        public DataWord Subtract(DataWord dw)
        {
            return new DataWord(BigInteger.Subtract(this.Value, dw.Value));
        }

        public DataWord Multiply(DataWord dw)
        {
            return new DataWord(BigInteger.Multiply(this.Value, dw.Value));
        }

        public DataWord Divide(DataWord dw)
        {
            return new DataWord(BigInteger.Divide(this.Value, dw.Value));
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is DataWord))
                return false;

            return this.Value.Equals(((DataWord)obj).Value);
        }
    }
}
