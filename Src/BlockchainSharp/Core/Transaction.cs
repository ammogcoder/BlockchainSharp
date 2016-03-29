namespace BlockchainSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class Transaction
    {
        private IList<AddressValue> from;
        private IList<AddressValue> to;

        public Transaction(IEnumerable<AddressValue> from, IEnumerable<AddressValue> to)
        {
            this.from = new List<AddressValue>(from);
            this.to = new List<AddressValue>(to);
        }

        public BigInteger TotalFrom { get {
            BigInteger result = BigInteger.Zero;

            foreach (var av in this.from)
                result = BigInteger.Add(result, av.Value);

            return result;
        } }

        public BigInteger TotalTo
        {
            get
            {
                BigInteger result = BigInteger.Zero;

                foreach (var av in this.to)
                    result = BigInteger.Add(result, av.Value);

                return result;
            }
        }
    }
}
