namespace BlockchainSharp.Tries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Trie<T>
    {
        private Trie<T>[] leafs;
        private T value;

        public Trie() 
        {
        }

        private Trie(Trie<T>[] leafs)
        {
            this.leafs = leafs;
        }

        private Trie(T value, Trie<T>[] leafs)
        {
            this.value = value;
            this.leafs = leafs;
        }

        public Trie<T> Put(string key, T value)
        {
            return this.Put(key, 0, value);
        }

        public T Get(string key)
        {
            return this.Get(key, 0);
        }

        private static int GetOffset(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return ch - '0';

            return ch - 'a' + 10;
        }

        private T Get(string key, int position)
        {
            var offset = GetOffset(key[position]);

            if (position == key.Length)
                return this.value;

            if (this.leafs == null)
                return default(T);

            var trie = (Trie<T>)this.leafs[offset];

            if (trie == null)
                return default(T);

            return trie.Get(key, position + 1);
        }

        private Trie<T> Put(string key, int position, T value)
        {
            Trie<T>[] newleafs;

            if (this.leafs != null)
                newleafs = (Trie<T>[])this.leafs.Clone();
            else
                newleafs = null;

            if (position == key.Length)
                return new Trie<T>(value, newleafs);

            int offset = GetOffset(key[position]);

            if (this.leafs[offset] != null)
            {
                newleafs[offset] = ((Trie<T>)this.leafs[offset]).Put(key, position + 1, value);

                return new Trie<T>(newleafs);
            }

            var newtrie = new Trie<T>();

            newleafs[offset] = newtrie.Put(key, position + 1, value);

            return new Trie<T>(newleafs);
        }
    }
}
