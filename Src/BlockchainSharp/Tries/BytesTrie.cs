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
            BytesTrie[] newleafs;

            if (this.leafs != null)
                newleafs = (BytesTrie[])this.leafs.Clone();
            else
                newleafs = null;

            if (position == key.Length)
                if (ValuesAreEqual(this.value, value))
                    return this;
                else
                    return new BytesTrie(value, newleafs);

            int offset = GetOffset(key[position]);

            if (this.leafs != null && this.leafs[offset] != null)
            {
                BytesTrie newleaf = this.leafs[offset].Put(key, position + 1, value);

                if (this.leafs[offset] == newleaf)
                    return this;

                newleafs[offset] = this.leafs[offset].Put(key, position + 1, value);

                return new BytesTrie(newleafs);
            }

            if (newleafs == null)
                newleafs = new BytesTrie[16];

            var newtrie = new BytesTrie();

            newleafs[offset] = newtrie.Put(key, position + 1, value);

            return new BytesTrie(newleafs);
        }

        private static bool ValuesAreEqual(byte[] value1, byte[] value2) {
            if (value1 == value2)
                return true;

            if (value1 == null || value2 == null)
                return false;

            return value1.SequenceEqual(value2);
        }
    }
}
