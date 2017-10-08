namespace BlockchainSharp.Tries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Trie
    {
        private static Trie empty = new Trie();

        private Trie[] leafs;
        private byte[] value;

        public Trie() 
        {
        }

        private Trie(Trie[] leafs)
        {
            this.leafs = leafs;
        }

        private Trie(byte[] value, Trie[] leafs)
        {
            this.value = value;
            this.leafs = leafs;
        }

        public bool IsEmpty()
        {
            return this.value == null && this.leafs == null;
        }

        public Trie Put(string key, byte[] value)
        {
            Trie newtrie = this.Put(ToBytes(key), 0, value);

            if (newtrie == null)
                return empty;

            return newtrie;
        }

        public byte[] Get(string key)
        {
            return this.Get(ToBytes(key), 0);
        }

        public Trie Remove(string key)
        {
            return this.Put(key, null);
        }

        private byte[] Get(byte[] key, int position)
        {
            if (position == key.Length)
                return this.value;

            var offset = key[position];

            if (this.leafs == null)
                return null;

            var trie = this.leafs[offset];

            if (trie == null)
                return null;

            return trie.Get(key, position + 1);
        }

        private Trie Put(byte[] key, int position, byte[] value)
        {
            if (position == key.Length)
                return this.ChangeValue(value);

            int offset = key[position];
 
            Trie newleaf = PutAtNewLeaf(key, position, value, offset);

            Trie[] newleafs = this.ChangeLeaf(offset, newleaf);

            if (EmptyLeafs(newleafs) && this.value == null)
                return null;

            if (this.leafs == newleafs)
                return this;

            return new Trie(this.value, newleafs);
        }

        private Trie PutAtNewLeaf(byte[] key, int position, byte[] value, int offset)
        {
            Trie leaf;

            if (this.leafs != null && this.leafs[offset] != null)
                leaf = this.leafs[offset];
            else
                leaf = new Trie();

            return leaf.Put(key, position + 1, value);
        }

        private Trie ChangeValue(byte[] newvalue)
        {
            if (this.SameValue(newvalue))
                return this;

            if (newvalue == null && EmptyLeafs(this.leafs))
                return null;

            return new Trie(newvalue, this.leafs);
        }

        private bool SameValue(byte[] newvalue) {
            if (this.value == newvalue)
                return true;

            if (this.value == null || newvalue == null)
                return false;

            return this.value.SequenceEqual(newvalue);
        }

        private Trie[] CloneLeafs()
        {
            if (this.leafs == null)
                return new Trie[16];

            return (Trie[])this.leafs.Clone();
        }

        private static bool EmptyLeafs(Trie[] leafs)
        {
            if (leafs == null)
                return true;

            for (int k = 0; k < leafs.Length; k++)
                if (leafs[k] != null)
                    return false;

            return true;
        }

        private Trie[] ChangeLeaf(int offset, Trie newleaf)
        {
            if (this.leafs != null && this.leafs[offset] == newleaf)
                return leafs;

            if (EmptyLeafs(this.leafs) && newleaf == null)
                return null;

            Trie[] newleafs = this.CloneLeafs();

            newleafs[offset] = newleaf;

            return newleafs;
        }

        private static byte[] ToBytes(string key)
        {
            byte[] bytes = new byte[key.Length];

            for (int k = 0; k < bytes.Length; k++)
                bytes[k] = ToByte(key[k]);

            return bytes;
        }

        private static byte ToByte(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return (byte)(ch - '0');

            return (byte)(ch - 'a' + 10);
        }
    }
}
