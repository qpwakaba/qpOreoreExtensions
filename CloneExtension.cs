using System;
using System.Collections.Generic;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class CloneExtension
    {
        public static T Clone<T>(this T t) where T : ICloneable
            => t.CastClone();
        public static T CastClone<T>(this T t) where T : ICloneable
            => (T)t.Clone();
    }
}
