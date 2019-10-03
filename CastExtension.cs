using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class CastExtension
    {
        private class DynamicCastCode<T>
        {
            private static class Cache<TResult>
            {
                public static readonly Func<T, TResult> Cast
                    = Expression.Lambda<Func<T, TResult>>(Expression.Convert(Expression.Parameter(typeof(T)), typeof(TResult)), Expression.Parameter(typeof(T))).Compile();
            }
            public static TResult Cast<TResult>(T value) => Cache<TResult>.Cast(value);
        }
        public static TResult As<T, TResult>(this T value)
            => DynamicCastCode<T>.Cast<TResult>(value);
    }
}
