namespace Problem4.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StackTests
    {
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void CheckSizeOfEmptyStack(bool useArrayStack)
        {
            var stack = SelectStack<object>(useArrayStack);
            Assert.AreEqual(stack.IsEmpty(), true);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void CheckSizeOfNonEmptyStack(bool useArrayStack)
        {
            var stack = SelectStack<object>(useArrayStack);
            stack.Push(new object());
            Assert.IsFalse(stack.IsEmpty());
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void PushNullMustWorkCorrectly(bool useArrayStack)
        {
            var stack = SelectStack<object>(useArrayStack);
            stack.Push(null);
            Assert.IsNull(stack.Pop());
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void StackMustBeEmptyAfterPushAndPop(bool useArrayStack)
        {
            var stack = SelectStack<object>(useArrayStack);
            stack.Push(new object());
            stack.Pop();
            Assert.IsTrue(stack.IsEmpty());
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SinglePushAndPopMustSaveValue(bool useArrayStack)
        {
            const string testString = "testtesttest";
            var stack = SelectStack<string>(useArrayStack);

            stack.Push(testString);
            Assert.AreEqual(stack.Pop(), testString);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopFromEmptyStackMustThrowAnException(bool useArrayStack)
        {
            var stack = SelectStack<int>(useArrayStack);
            stack.Pop();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void MultiplePushAndPops(bool useArrayStack)
        {
            var stack = SelectStack<int>(useArrayStack);

            for (int i = -150; i <= 150; ++i)
            {
                stack.Push(i);
            }

            for (int i = 150; i >= -150; --i)
            {
                Assert.AreEqual(i, stack.Pop());
            }
        }

        private static StackStuff.IStack<T> SelectStack<T>(bool useArrayStack)
        {
            StackStuff.IStack<T> stack;
            if (useArrayStack)
            {
                stack = new StackStuff.ArrayStack<T>();
            }
            else
            {
                stack = new StackStuff.Stack<T>();
            }

            return stack;
        }
    }
}
