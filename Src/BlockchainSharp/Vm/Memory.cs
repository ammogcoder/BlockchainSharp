namespace BlockchainSharp.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class Memory
    {
        private IDictionary<BigInteger, byte[]> blocks = new Dictionary<BigInteger, byte[]>();

        public DataWord GetDataWord(DataWord address)
        {
            byte[] bytes = this.GetBlock(address);

            if (bytes != null) 
            {
                var dbytes = new byte[32];
                Array.Copy(bytes, (int)address.Value, dbytes, 0, 32);
                return new DataWord(dbytes);
            }

            return DataWord.Zero;
        }

        public void PutBytes(DataWord address, byte[] values)
        {
            byte[] bytes = this.GetOrCreateBlock(address);

            Array.Copy(values, 0, bytes, (int)address.Value, values.Length);
        }

        public byte GetByte(DataWord address)
        {
            byte[] bytes = this.GetBlock(address);

            if (bytes != null)
                return bytes[(int)address.Value];

            return 0;
        }

        public void PutByte(DataWord address, byte value)
        {
            byte[] bytes = this.GetOrCreateBlock(address);

            bytes[(int)address.Value] = value;
        }

        private byte[] GetBlock(DataWord address) 
        {
            if (this.blocks.ContainsKey(BigInteger.Zero))
                return this.blocks[BigInteger.Zero];

            return null;
        }

        private byte[] GetOrCreateBlock(DataWord address) 
        {
            if (this.blocks.ContainsKey(BigInteger.Zero))
                return this.blocks[BigInteger.Zero];

            this.blocks[BigInteger.Zero] = new byte[1024];

            return this.blocks[BigInteger.Zero];
        }
    }
}
