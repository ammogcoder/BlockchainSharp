namespace BlockchainSharp.Tests.Tries
{
    using System;
    using System.Linq;
    using BlockchainSharp.Tries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void GetUnknowKey()
        {
            Trie trie = new Trie();

            Assert.IsNull(trie.Get("012"));
        }

        [TestMethod]
        public void NewTrieIsEmpty()
        {
            Trie trie = new Trie();

            Assert.IsTrue(trie.IsEmpty());
        }

        [TestMethod]
        public void PutAndGetKeyValue()
        {
            Trie trie = new Trie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsTrue((new byte[] { 1, 2, 3 }).SequenceEqual(trie2.Get("012")));
            Assert.IsFalse(trie2.IsEmpty());
        }

        [TestMethod]
        public void PutKeyValueTwice()
        {
            Trie trie = new Trie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });
            var trie3 = trie2.Put("012", new byte[] { 1, 2, 3 });

            Assert.IsNotNull(trie3);
            Assert.AreEqual(trie2, trie3);
        }

        [TestMethod]
        public void PutAndGetKeyValues()
        {
            Trie trie = new Trie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });
            var trie3 = trie2.Put("abc", new byte[] { 4, 5, 6 });

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsNull(trie.Get("abc"));
            Assert.IsTrue((new byte[] { 1, 2, 3 }).SequenceEqual(trie2.Get("012")));
            Assert.IsNull(trie2.Get("abc"));

            Assert.IsNotNull(trie3);
            Assert.AreNotSame(trie3, trie2);
            Assert.IsTrue((new byte[] { 1, 2, 3 }).SequenceEqual(trie3.Get("012")));
            Assert.IsTrue((new byte[] { 4, 5, 6 }).SequenceEqual(trie3.Get("abc")));
        }

        [TestMethod]
        public void ReplaceValue()
        {
            Trie trie = new Trie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });
            var trie3 = trie2.Put("012", new byte[] { 4, 5, 6 });

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsTrue((new byte[] { 1, 2, 3 }).SequenceEqual(trie2.Get("012")));

            Assert.IsNotNull(trie3);
            Assert.AreNotSame(trie3, trie2);
            Assert.IsTrue((new byte[] { 4, 5, 6 }).SequenceEqual(trie3.Get("012")));
            Assert.IsFalse(trie3.IsEmpty());
        }

        [TestMethod]
        public void RemoveValue()
        {
            Trie trie = new Trie();

            var trie2 = trie.Put("012", new byte[] { 1, 2, 3 });
            var trie3 = trie2.Remove("012");

            Assert.IsNotNull(trie2);
            Assert.AreNotSame(trie2, trie);
            Assert.IsNull(trie.Get("012"));
            Assert.IsTrue((new byte[] { 1, 2, 3 }).SequenceEqual(trie2.Get("012")));

            Assert.IsNotNull(trie3);
            Assert.AreNotSame(trie3, trie2);
            Assert.IsNull(trie3.Get("012"));
            Assert.IsTrue(trie3.IsEmpty());
        }
    }
}

