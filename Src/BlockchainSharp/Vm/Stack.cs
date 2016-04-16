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

        public void Swap(int position)
        {
            var top = this.stack.Pop();

            var auxstack = new Stack<DataWord>();

            for (int k = 1; k < position; k++)
                auxstack.Push(this.stack.Pop());

            var newtop = this.stack.Pop();

            this.stack.Push(top);

            for (int k = 1; k < position; k++)
                this.stack.Push(auxstack.Pop());

            this.stack.Push(newtop);
        }
    }
}
