namespace BlockchainSharp.Tries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Trie<T>
    {
        private object[] leafs;

        public Trie() 
        {
            this.leafs = new object[16];
        }

        public void Put(string key, T value)
        {
        }

        public T Get(string key)
        {
            return default(T);
        }
    }
}
