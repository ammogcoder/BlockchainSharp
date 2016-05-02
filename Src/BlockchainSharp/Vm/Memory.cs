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
