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

        public static IfExpression<T> If<T>(this T t, Predicate<T> condition) => new IfExpression<T>(t, condition(t));
        public static IfExpression<T> If<T>(this T t, bool condition) => new IfExpression<T>(t, condition);

        public readonly ref struct IfExpression<T>
        {
            private readonly T t;
            private readonly bool condition;
            internal IfExpression(T t, bool cond)
            {
                this.t = t;
                this.condition = cond;
            }

            public IfExpression<T> Then(Action<T> action)
            {
                if (condition) action(this.t);
                return this;
            }

            public IfExpression<T> Otherwise(Action<T> action)
            {
                if (condition.Not()) action(this.t);
                return this;
            }

            public T Fi() => t;
        }

    }
}
