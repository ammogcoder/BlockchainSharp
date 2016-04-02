namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class Transaction
    {
        private IList<AddressValue> inputs;
        private IList<AddressValue> outputs;

        public Transaction(IEnumerable<AddressValue> inputs, IEnumerable<AddressValue> outputs)
        {
            this.inputs = new List<AddressValue>(inputs);
            this.outputs = new List<AddressValue>(outputs);

            if (this.InputsTotal.CompareTo(this.OutputsTotal) < 0)
                throw new InvalidOperationException("Transaction outputs are greater than inputs");
        }

        public BigInteger InputsTotal
        { 
            get 
            {
                BigInteger result = BigInteger.Zero;

                foreach (var av in this.inputs)
                    result = BigInteger.Add(result, av.Value);

                return result;
            }
        }

        public BigInteger OutputsTotal
        {
            get
            {
                BigInteger result = BigInteger.Zero;

                foreach (var av in this.outputs)
                    result = BigInteger.Add(result, av.Value);

                return result;
            }
        }
    }
}
