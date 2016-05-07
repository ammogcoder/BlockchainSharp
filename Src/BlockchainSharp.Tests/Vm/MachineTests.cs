namespace BlockchainSharp.Tests.Vm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void PushByte()
        {
            PushPop(new byte[] { 0x01 });
        }

        [TestMethod]
        public void PushBytes()
        {
            for (int k = 1; k <= 32; k++)
                PushPop(k);
        }

        [TestMethod]
        public void PushAndDup()
        {
            PushDupPop(1);
        }

        [TestMethod]
        public void PushTwiceAndDup()
        {
            PushDupPop(2);
        }

        [TestMethod]
        public void PushPushPopPop()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(1);
            compiler.Push(2);
            compiler.Pop();
            compiler.Pop();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(0, stack.Size);
        }

        [TestMethod]
        public void PushThreeValuesAndDup()
        {
            PushDupPop(3);
        }

        [TestMethod]
        public void PushFourValuesAndDup()
        {
            PushDupPop(4);
        }

        [TestMethod]
        public void PushAllValuesAndDup()
        {
            for (int k = 1; k <= 16; k++)
                PushDupPop(k);
        }

        [TestMethod]
        public void PushByteTwice()
        {
            Machine machine = new Machine();

            machine.Execute(new byte[] { (byte)Bytecodes.Push1, 0x01, (byte)Bytecodes.Push1, 0x02 });

            Assert.AreEqual(machine.Stack.Pop(), DataWord.Two);
            Assert.AreEqual(machine.Stack.Pop(), DataWord.One);
            Assert.AreEqual(0, machine.Stack.Size);
        }

        [TestMethod]
        public void PushTwoBytesTwice()
        {
            Machine machine = new Machine();

            machine.Execute(new byte[] { (byte)Bytecodes.Push2, 0x01, 0x02, (byte)Bytecodes.Push2, 0x03, 0x04 });

            Assert.AreEqual(machine.Stack.Pop(), new DataWord((256 * 3) + 4));
            Assert.AreEqual(machine.Stack.Pop(), new DataWord(256 + 2));
        }

        [TestMethod]
        public void PushThreeBytesTwice()
        {
            Machine machine = new Machine();

            machine.Execute(new byte[] { (byte)Bytecodes.Push3, 0x01, 0x02, 0x03, (byte)Bytecodes.Push3, 0x04, 0x05, 0x06 });

            Assert.AreEqual(machine.Stack.Pop(), new DataWord((256 * 256 * 4) + (256 * 5) + 6));
            Assert.AreEqual(machine.Stack.Pop(), new DataWord((256 * 256 * 1) + (256 * 2) + 3));
            Assert.AreEqual(0, machine.Stack.Size);
        }

        [TestMethod]
        public void PushesAndSwap()
        {
            for (int k = 1; k <= 16; k++)
            {
                Machine machine = PushSwap(k);
                Assert.AreEqual(new DataWord(16), machine.Stack.ElementAt(k));
                Assert.AreEqual(new DataWord(16 - k), machine.Stack.Top());
            }
        }

        [TestMethod]
        public void Stop()
        {
            Machine machine = new Machine();
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Stop();
            compiler.Push(1);
            compiler.Push(2);
            compiler.Push(3);

            machine.Execute(compiler.ToBytes());

            Assert.AreEqual(0, machine.Stack.Size);
        }

        [TestMethod]
        public void LessThan()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(2);
            compiler.Push(2);
            compiler.LessThan();
            compiler.Push(0);
            compiler.Push(1);
            compiler.LessThan();
            compiler.Push(1);
            compiler.Push(0);
            compiler.LessThan();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(2));
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(1));
            Assert.AreEqual(DataWord.One, stack.ElementAt(0));
        }

        [TestMethod]
        public void GreaterThan()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(2);
            compiler.Push(2);
            compiler.GreaterThan();
            compiler.Push(1);
            compiler.Push(0);
            compiler.GreaterThan();
            compiler.Push(0);
            compiler.Push(1);
            compiler.GreaterThan();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(2));
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(1));
            Assert.AreEqual(DataWord.One, stack.ElementAt(0));
        }

        [TestMethod]
        public void Equal()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(2);
            compiler.Push(2);
            compiler.Equal();
            compiler.Push(0);
            compiler.Push(1);
            compiler.Equal();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(DataWord.One, stack.ElementAt(1));
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(0));
        }

        [TestMethod]
        public void StorageStoreLoad()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(1);
            compiler.SLoad();
            compiler.Push(2);
            compiler.Push(3);
            compiler.SStore();
            compiler.Push(3);
            compiler.SLoad();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(DataWord.Two, stack.Pop());
            Assert.AreEqual(DataWord.Zero, stack.Pop());
        }

        [TestMethod]
        public void IsZero()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(2);
            compiler.IsZero();
            compiler.Push(0);
            compiler.IsZero();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(DataWord.Zero, stack.ElementAt(1));
            Assert.AreEqual(DataWord.One, stack.ElementAt(0));
        }

        [TestMethod]
        public void AddOneTwo()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(1);
            compiler.Push(2);
            compiler.Add();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(DataWord.Three, stack.Pop());
        }

        [TestMethod]
        public void SubtractThreeOne()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(1);
            compiler.Push(3);
            compiler.Subtract();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(DataWord.Two, stack.Pop());
        }

        [TestMethod]
        public void MultiplyThreeTwo()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(2);
            compiler.Push(3);
            compiler.Multiply();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(new DataWord(6), stack.Pop());
        }

        [TestMethod]
        public void DivideSixTwo()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(2);
            compiler.Push(6);
            compiler.Divide();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(DataWord.Three, stack.Pop());
        }

        [TestMethod]
        public void DivideByZero()
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.Push(0);
            compiler.Push(6);
            compiler.Divide();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.IsNotNull(stack);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(DataWord.Zero, stack.Pop());
        }

        [TestMethod]
        public void StoreByte()
        {
            var compiler = new BytecodeCompiler();
            compiler.Push(257);
            compiler.Push(42);
            compiler.MStore8();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var memory = machine.Memory;

            Assert.IsNotNull(memory);
            Assert.AreEqual(1, memory.GetByte(new DataWord(42)));
            Assert.AreEqual(0, memory.GetByte(new DataWord(41)));
            Assert.AreEqual(0, memory.GetByte(new DataWord(43)));
        }

        [TestMethod]
        public void StoreDataWord()
        {
            var compiler = new BytecodeCompiler();
            compiler.Push(257);
            compiler.Push(42);
            compiler.MStore();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var memory = machine.Memory;

            Assert.IsNotNull(memory);
            Assert.AreEqual(0, memory.GetByte(new DataWord(42)));
            Assert.AreEqual(0, memory.GetByte(new DataWord(41)));
            Assert.AreEqual(0, memory.GetByte(new DataWord(43)));
            Assert.AreEqual(1, memory.GetByte(new DataWord(42 + 30)));
            Assert.AreEqual(1, memory.GetByte(new DataWord(42 + 31)));
        }

        [TestMethod]
        public void StoreAndLoadDataWord()
        {
            var compiler = new BytecodeCompiler();
            compiler.Push(257);
            compiler.Push(42);
            compiler.MStore();
            compiler.Push(43);
            compiler.MLoad();

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            var stack = machine.Stack;

            Assert.AreEqual(new DataWord(257 << 8), stack.Pop());
        }

        private static void PushPop(int times)
        {
            byte[] bytes = new byte[times];

            for (int k = 0; k < times; k++)
                bytes[k] = (byte)(k + 1);

            PushPop(bytes);
        }

        private static void PushPop(byte[] bytes)
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            compiler.CompileAdjust(Bytecodes.Push1, bytes.Length - 1, bytes);

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            Assert.AreEqual(1, machine.Stack.Size);
            Assert.AreEqual(new DataWord(bytes), machine.Stack.Pop());
        }

        private static Machine PushSwap(int nswap)
        {
            BytecodeCompiler compiler = new BytecodeCompiler();

            for (int k = 0; k < 17; k++)
                compiler.Push(k);

            compiler.Swap(nswap);

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            return machine;
        }

        private static void PushDupPop(int times)
        {
            var compiler = new BytecodeCompiler();

            for (int k = 0; k < times; k++)
                compiler.Push(k);

            compiler.Dup(times);

            Machine machine = new Machine();

            machine.Execute(compiler.ToBytes());

            DataWord value = new DataWord(times);

            Assert.AreEqual(DataWord.Zero, machine.Stack.Pop());

            for (int k = 0; k < times; k++)
            {
                value = value.Subtract(DataWord.One);
                Assert.AreEqual(value, machine.Stack.Pop());
            }

            Assert.AreEqual(0, machine.Stack.Size);
        }
    }
}
