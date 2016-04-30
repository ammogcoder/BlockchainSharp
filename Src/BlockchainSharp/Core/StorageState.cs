namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Vm;

    public class StorageState
    {
        private IDictionary<DataWord, DataWord> state = new Dictionary<DataWord, DataWord>();

        public void Put(DataWord key, DataWord value)
        {
            this.state[key] = value;
        }

        public DataWord Get(DataWord key)
        {
            if (this.state.ContainsKey(key))
                return this.state[key];

            return DataWord.Zero;
        }
    }
}
