using System;
using System.Collections.Generic;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class LinqExtension
    {
        public static void ForEach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (T t in ts) action(t);
        }
        public static void ForEach<T>(this IEnumerable<T> ts, Action<T, int> action)
        {
            int i = 0;
            foreach (T t in ts) action(t, i++);
        }
        public static void ForEach<T, TResult>(this IEnumerable<T> ts, Func<T, TResult> action)
        {
            foreach (T t in ts) action(t);
        }
        public static void ForEach<T, TResult>(this IEnumerable<T> ts, Func<T, int, TResult> action)
        {
            int i = 0;
            foreach (T t in ts) action(t, i++);
        }
    }
}
