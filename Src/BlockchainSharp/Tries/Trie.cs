namespace BlockchainSharp.Tries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Trie<T>
    {
        private object[] leafs;
        private T defvalue;

        public Trie() 
            : this(default(T))
        {
        }

        public Trie(T defvalue)
        {
            this.leafs = new object[16];
            this.defvalue = defvalue;
        }

        private Trie(T defvalue, object[] leafs)
        {
            this.defvalue = defvalue;
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

            if (position == key.Length - 1)
                return (T)this.leafs[offset];

            var trie = (Trie<T>)this.leafs[offset];

            if (trie == null)
                return this.defvalue;

            return trie.Get(key, position + 1);
        }

        private Trie<T> Put(string key, int position, T value)
        {
            var offset = GetOffset(key[position]);
            var newleafs = (object[])this.leafs.Clone();

            if (position == key.Length - 1)
            {
                newleafs[offset] = value;

                return new Trie<T>(this.defvalue, newleafs);
            }

            if (this.leafs[offset] != null)
            {
                newleafs[offset] = ((Trie<T>)this.leafs[offset]).Put(key, position + 1, value);

                return new Trie<T>(this.defvalue, newleafs);
            }

            var newtrie = new Trie<T>();

            newleafs[offset] = newtrie.Put(key, position + 1, value);

            return new Trie<T>(this.defvalue, newleafs);
        }
    }
}
