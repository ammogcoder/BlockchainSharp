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

        [TestMethod]
        public void PutAndGetKeyValues()
        {
            Trie<string> trie = new Trie<string>();

            var trie2 = trie.Put("012", "foo");
            var trie3 = trie2.Put("abc", "bar");

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsNull(trie.Get("abc"));
            Assert.AreEqual("foo", trie2.Get("012"));
            Assert.IsNull(trie2.Get("abc"));

            Assert.IsNotNull(trie3);
            Assert.AreNotSame(trie3, trie2);
            Assert.AreEqual("foo", trie3.Get("012"));
            Assert.AreEqual("bar", trie3.Get("abc"));
        }
    }
}

