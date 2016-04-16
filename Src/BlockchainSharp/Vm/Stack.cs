namespace BlockchainSharp.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Stack
    {
        private Stack<DataWord> stack = new Stack<DataWord>();

        public int Size { get { return this.stack.Count; } }

        public void Push(DataWord value)
        {
            this.stack.Push(value);
        }

        public DataWord Top()
        {
            return this.stack.Peek();
        }

        public DataWord Pop()
        {
            return this.stack.Pop();
        }

        public DataWord ElementAt(int index)
        {
            return this.stack.ElementAt(index);
        }
    }
}
