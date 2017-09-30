namespace BlockchainSharp.Tries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BytesTrie
    {
        private static BytesTrie empty = new BytesTrie();

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

        public bool IsEmpty()
        {
            return this.value == null && this.leafs == null;
        }

        public BytesTrie Put(string key, byte[] value)
        {
            BytesTrie newtrie = this.Put(key, 0, value);

            if (newtrie == null)
                return empty;

            return newtrie;
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
                return this.ChangeValue(value);

            int offset = GetOffset(key[position]);
            BytesTrie leaf;

            if (this.leafs != null && this.leafs[offset] != null)
                leaf = this.leafs[offset];
            else
                leaf = new BytesTrie();

            BytesTrie newleaf = leaf.Put(key, position + 1, value);

            BytesTrie[] newleafs = this.ChangeLeaf(offset, newleaf);

            if (EmptyLeafs(newleafs) && this.value == null)
                return null;

            if (this.leafs == newleafs)
                return this;

            return new BytesTrie(this.value, newleafs);
        }

        private BytesTrie ChangeValue(byte[] newvalue)
        {
            if (this.SameValue(newvalue))
                return this;

            if (newvalue == null && EmptyLeafs(this.leafs))
                return null;

            return new BytesTrie(newvalue, this.leafs);
        }

        private bool SameValue(byte[] newvalue) {
            if (this.value == newvalue)
                return true;

            if (this.value == null || newvalue == null)
                return false;

            return this.value.SequenceEqual(newvalue);
        }

        private BytesTrie[] CloneLeafs()
        {
            if (this.leafs == null)
                return new BytesTrie[16];

            return (BytesTrie[])this.leafs.Clone();
        }

        private static bool EmptyLeafs(BytesTrie[] leafs)
        {
            if (leafs == null)
                return true;

            for (int k = 0; k < leafs.Length; k++)
                if (leafs[k] != null)
                    return false;

            return true;
        }

        private BytesTrie[] ChangeLeaf(int offset, BytesTrie newleaf)
        {
            if (this.leafs != null && this.leafs[offset] == newleaf)
                return leafs;

            if (EmptyLeafs(this.leafs) && newleaf == null)
                return null;

            BytesTrie[] newleafs = this.CloneLeafs();

            newleafs[offset] = newleaf;

            return newleafs;
        }
    }
}
