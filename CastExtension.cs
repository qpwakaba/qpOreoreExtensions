using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class CastExtension
    {
        public static TResult As<TResult>(this object value)
            => (TResult)value;
    }
}
