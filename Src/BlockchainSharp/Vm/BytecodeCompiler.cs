namespace BlockchainSharp.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class BytecodeCompiler
    {
        private IList<byte> bytes = new List<byte>();

        public void Push(int value)
        {
            var bytes = (new BigInteger(value)).ToByteArray().Reverse().ToArray();
            this.CompileAdjust(Bytecodes.Push1, bytes.Length - 1, bytes);
        }

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
