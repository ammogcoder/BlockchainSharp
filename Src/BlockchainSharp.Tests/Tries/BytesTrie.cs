namespace BlockchainSharp.Tests.Tries
{
    using System;
    using BlockchainSharp.Tries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BytesTrieTests
    {
        [TestMethod]
        public void GetUnknowKey()
        {
            BytesTrie trie = new BytesTrie();

            Assert.IsNull(trie.Get("012"));
        }

        [TestMethod]
        public void PutAndGetKeyValue()
        {
            BytesTrie trie = new BytesTrie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsTrue(Array.Equals(new byte[] { 1, 2, 3 }, trie2.Get("012")));
        }

        [TestMethod]
        public void PutAndGetKeyValues()
        {
            BytesTrie trie = new BytesTrie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });
            var trie3 = trie2.Put("abc", new byte[] { 4, 5, 6 });

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsNull(trie.Get("abc"));
            Assert.AreEqual(new byte[] { 1, 2, 3 }, trie2.Get("012"));
            Assert.IsNull(trie2.Get("abc"));

            Assert.IsNotNull(trie3);
            Assert.AreNotSame(trie3, trie2);
            Assert.AreEqual(new byte[] { 1, 2, 3 }, trie3.Get("012"));
            Assert.AreEqual(new byte[] { 4, 5, 6 }, trie3.Get("abc"));
        }

        [TestMethod]
        public void ReplaceValue()
        {
            Trie<string> trie = new Trie<string>();

            var trie2 = trie.Put("012", "foo");
            var trie3 = trie2.Put("012", "bar");

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.AreEqual("foo", trie2.Get("012"));

            Assert.IsNotNull(trie3);
            Assert.AreNotSame(trie3, trie2);
            Assert.AreEqual("bar", trie3.Get("012"));
        }
    }
}

