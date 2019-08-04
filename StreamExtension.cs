using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace qpwakaba.Extensions
{
    public static class StreamExtension
    {
        public static int Read(this Stream stream, byte[] buffer)
            => stream.Read(buffer, 0, buffer.Length);
        public static byte[] ReadFully(this Stream stream, int count)
            => new byte[count].Also(stream.ReadFully);
        public static int ReadFully(this Stream stream, byte[] buffer)
            => ReadFully(stream, buffer, 0, buffer.Length);
        public static int ReadFully(this Stream stream, byte[] buffer, int offset, int count)
        {
            int read = 0;
            while (read < count)
            {
                read += stream.Read(buffer, offset + read, count - read);
            }
#if DEBUG
            System.Diagnostics.Debug.Assert(read == count);
#endif
            return count;
        }
        public static Task<int> ReadFullyAsync(this Stream stream, byte[] buffer)
            => ReadFullyAsync(stream, buffer, 0, buffer.Length);
        public static async Task<int> ReadFullyAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            int read = 0;
            while (read < count)
            {
                read += await stream.ReadAsync(buffer, offset + read, count - read);
            }
#if DEBUG
            System.Diagnostics.Debug.Assert(read == count);
#endif
            return count;
        }
        public static void Write(this Stream stream, byte[] data)
            => stream.Write(data, 0, data.Length);
    }
}
