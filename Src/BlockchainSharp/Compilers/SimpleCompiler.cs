namespace BlockchainSharp.Compilers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Vm;

    public class SimpleCompiler
    {
        private BytecodeCompiler compiler = new BytecodeCompiler();

        public SimpleCompiler(string text)
        {
        }

        public byte[] Compile()
        {
            return compiler.ToBytes();
        }
    }
}
