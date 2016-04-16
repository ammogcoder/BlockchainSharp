namespace BlockchainSharp.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BytecodeCompiler
    {
        private IList<byte> bytes = new List<byte>();

        public void Compile(Bytecodes bytecode, params byte[] args)
        {
            this.bytes.Add((byte)bytecode);

            foreach (var b in args)
                this.bytes.Add(b);
        }

        public void CompileAdjust(Bytecodes bytecode, int adjust, params byte[] args)
        {
            this.bytes.Add((byte)(bytecode + adjust));

            foreach (var b in args)
                this.bytes.Add(b);
        }

        public byte[] ToBytes()
        {
            return this.bytes.ToArray();
        }
    }
}
