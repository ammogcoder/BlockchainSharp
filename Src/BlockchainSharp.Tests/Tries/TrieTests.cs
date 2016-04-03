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

            Assert.IsNull(trie.Get("012"));
        }

        [TestMethod]
        public void PutAndGetKeyValue()
        {
            Trie<string> trie = new Trie<string>();

            var trie2 = trie.Put("012", "foo");

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.AreEqual("foo", trie2.Get("012"));
        }
    }
}

