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

        public void Add()
        {
            this.Compile(Bytecodes.Add);
        }

        public void Subtract()
        {
            this.Compile(Bytecodes.Subtract);
        }

        public void Multiply()
        {
            this.Compile(Bytecodes.Multiply);
        }

        public void IsZero()
        {
            this.Compile(Bytecodes.IsZero);
        }

        public void Push(int value)
        {
            var bytes = (new BigInteger(value)).ToByteArray().Reverse().ToArray();
            this.CompileAdjust(Bytecodes.Push1, bytes.Length - 1, bytes);
        }

        public void Swap(int n)
        {
            this.CompileAdjust(Bytecodes.Swap1, n - 1);
        }

        public void Dup(int n)
        {
            this.CompileAdjust(Bytecodes.Dup1, n - 1);
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
