using System;
using System.Collections.Generic;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class DelegateExtension
    {
        public static void WriteTo(this Action<byte[]> action, byte[] to, int offset)
            => WriteTo(action, to, offset, to.Length - offset);
        public static void WriteTo(this Action<byte[]> action, byte[] to, int offset, int length)
        {
            byte[] buffer = new byte[length];
            action(buffer);
            buffer.CopyTo(to, offset);
        }
    }
}
