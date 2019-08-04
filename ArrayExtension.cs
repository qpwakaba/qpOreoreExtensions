using System;
using System.Collections.Generic;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class ArrayExtension
    {
        public static T[] Concat<T>(this T[] a, T[] b)
        {
            var result = new T[a.Length + b.Length];
            a.CopyTo(result, 0);
            b.CopyTo(result, a.Length);
            return result;
        }
        public static T[] Subarray<T>(this T[] array, int offset)
            => array.Subarray(offset, array.Length - offset);
        public static T[] Subarray<T>(this T[] array, int offset, int length)
        {
            var result = new T[length];
            array.CopyTo(result, offset);
            return result;
        }
    }
}
