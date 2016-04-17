namespace BlockchainSharp.Tests.Compilers
{
    using System;
    using BlockchainSharp.Compilers;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SimpleCompilerTests
    {
        [TestMethod]
        public void CompileEmptyString()
        {
            var compiler = new SimpleCompiler(string.Empty);

            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void CompileNullString()
        {
            var compiler = new SimpleCompiler(null);

            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void CompileArithmeticOperations()
        {
            CompileBytecode("add", Bytecodes.Add);
            CompileBytecode("subtract", Bytecodes.Subtract);
            CompileBytecode("multiply", Bytecodes.Multiply);
            CompileBytecode("divide", Bytecodes.Divide);
        }

        [TestMethod]
        public void CompileSkippingCommentsAndEmptyLines()
        {
            CompileBytecode("add\n", Bytecodes.Add);
            CompileBytecode("add\r\n", Bytecodes.Add);
            CompileBytecode("add\r", Bytecodes.Add);
            CompileBytecode("# comment\nadd\n", Bytecodes.Add);
            CompileBytecode("\n\nadd # comment\n", Bytecodes.Add);
            CompileBytecode("\r\n\r\nadd # comment\n", Bytecodes.Add);
            CompileBytecode("\r\radd # comment\n", Bytecodes.Add);
        }

        private static void CompileBytecode(string text, Bytecodes bc)
        {
            var compiler = new SimpleCompiler(text);
            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual((byte)bc, result[0]);
        }
    }
}
