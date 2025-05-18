#region

using System;
using System.Collections.Generic;

#endregion

namespace cope.Extensions
{
    public static class StackExt
    {
        public static IEnumerable<T> PopWhile<T>(this Stack<T> stack, Func<T, bool> cond)
        {
            List<T> list = new List<T>(stack.Count);
            while (stack.Count != 0 && cond(stack.Peek()))
                list.Add(stack.Pop());
            return list;
        }

        public static void Push<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            foreach (T t in items)
                stack.Push(t);
        }

        public static void Push<T>(this Stack<T> stack, params T[] items)
        {
            foreach (T t in items)
                stack.Push(t);
        }
    }
}