namespace BlockchainSharp.Tries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BytesTrie
    {
        private BytesTrie[] leafs;
        private byte[] value;

        public BytesTrie() 
        {
        }

        private BytesTrie(BytesTrie[] leafs)
        {
            this.leafs = leafs;
        }

        private BytesTrie(byte[] value, BytesTrie[] leafs)
        {
            this.value = value;
            this.leafs = leafs;
        }

        public BytesTrie Put(string key, byte[] value)
        {
            return this.Put(key, 0, value);
        }

        public byte[] Get(string key)
        {
            return this.Get(key, 0);
        }

        public BytesTrie Remove(string key)
        {
            return this.Put(key, null);
        }

        private static int GetOffset(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return ch - '0';

            return ch - 'a' + 10;
        }

        private byte[] Get(string key, int position)
        {
            if (position == key.Length)
                return this.value;

            var offset = GetOffset(key[position]);

            if (this.leafs == null)
                return null;

            var trie = this.leafs[offset];

            if (trie == null)
                return null;

            return trie.Get(key, position + 1);
        }

        private BytesTrie Put(string key, int position, byte[] value)
        {
            if (position == key.Length)
                if (ValuesAreEqual(this.value, value))
                    return this;
                else
                    return new BytesTrie(value, CloneLeafs(this.leafs));

            int offset = GetOffset(key[position]);
            BytesTrie leaf;

            if (this.leafs != null && this.leafs[offset] != null)
                leaf = this.leafs[offset];
            else
                leaf = new BytesTrie();

            BytesTrie newleaf = leaf.Put(key, position + 1, value);

            BytesTrie[] newleafs = PutLeaf(this.leafs, offset, newleaf);

            if (this.leafs == newleafs)
                return this;

            return new BytesTrie(this.value, newleafs);
        }

        private static bool ValuesAreEqual(byte[] value1, byte[] value2) {
            if (value1 == value2)
                return true;

            if (value1 == null || value2 == null)
                return false;

            return value1.SequenceEqual(value2);
        }

        private static BytesTrie[] CloneLeafs(BytesTrie[] leafs)
        {
            if (leafs == null)
                return new BytesTrie[16];

            return (BytesTrie[])leafs.Clone();
        }

        private static BytesTrie[] PutLeaf(BytesTrie[] leafs, int offset, BytesTrie newleaf)
        {
            if (leafs != null && leafs[offset] == newleaf)
                return leafs;

            BytesTrie[] newleafs = CloneLeafs(leafs);

            newleafs[offset] = newleaf;

            return newleafs;
        }
    }
}
