namespace BlockchainSharp.Tests.Tries
{
    using System;
    using BlockchainSharp.Tries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void GetUnknowKey()
        {
            Trie<string> trie = new Trie<string>();

            Assert.IsNull(trie.Get("foo"));
        }
    }
}
