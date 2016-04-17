namespace BlockchainSharp.Tests.Compilers
{
    using System;
    using BlockchainSharp.Compilers;
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
    }
}
