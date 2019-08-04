using System;
using System.Collections.Generic;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class ScopeFunctions
    {
        public static R Let<T, R>(this T t, Func<T, R> action) => action(t);
        public static void Let<T>(this T t, Action<T> action) => action(t);
        public static T Also<T, R>(this T t, Func<T, R> action)
        {
            action(t);
            return t;
        }
        public static T Also<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }
        public static T Also<T>(this T t, Action action)
        {
            action();
            return t;
        }

    }
}
