namespace BlockchainSharp.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class Memory
    {
        private byte[] bytes;

        public DataWord GetDataWord(DataWord address)
        {
            if (this.bytes != null) {
                var bytes = new byte[32];
                Array.Copy(this.bytes, (int)address.Value, bytes, 0, 32);
                return new DataWord(bytes);
            }

            return DataWord.Zero;
        }

        public void PutBytes(DataWord address, byte[] values)
        {
            if (this.bytes == null)
                this.bytes = new byte[1024];

            Array.Copy(values, 0, this.bytes, (int)address.Value, values.Length);
        }

        public byte GetByte(DataWord address)
        {
            if (this.bytes != null)
                return this.bytes[(int)address.Value];

            return 0;
        }

        public void PutByte(DataWord address, byte value)
        {
            if (this.bytes == null)
                this.bytes = new byte[1024];

            this.bytes[(int)address.Value] = value;
        }
    }
}
