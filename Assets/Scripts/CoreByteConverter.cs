using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Helpers
{
    //Independent part
    public static partial class ByteConverter
    {
        public static int ToByteArray(byte source, byte[] destination, int offset)
        {
            destination[offset] = source;
            return 1;
        }

        public static int ToByteArray(byte source, Stream destination)
        {
            destination.WriteByte(source);
            return 1;
        }

        public static int ToByteArray(bool source, byte[] destination, int offset)
        {
            destination[offset] = (byte)(source ? 1 : 0);
            return 1;
        }

        public static int ToByteArray(bool source, Stream destination)
        {
            destination.WriteByte((byte)(source ? 1 : 0));
            return 1;
        }

        public static int ToByteArray(long source, byte[] destination, int offset)
        {
            destination[offset] = (byte)(source >> 56);
            destination[offset + 1] = (byte)(source >> 48);
            destination[offset + 2] = (byte)(source >> 40);
            destination[offset + 3] = (byte)(source >> 32);
            destination[offset + 4] = (byte)(source >> 24);
            destination[offset + 5] = (byte)(source >> 16);
            destination[offset + 6] = (byte)(source >> 8);
            destination[offset + 7] = (byte)(source);
            return 8;
        }

        public static int ToByteArray(long source, Stream destination)
        {
            destination.WriteByte((byte)(source >> 56));
            destination.WriteByte((byte)(source >> 48));
            destination.WriteByte((byte)(source >> 40));
            destination.WriteByte((byte)(source >> 32));
            destination.WriteByte((byte)(source >> 24));
            destination.WriteByte((byte)(source >> 16));
            destination.WriteByte((byte)(source >> 8));
            destination.WriteByte((byte)(source));
            return 8;
        }

        public static int ToByteArray(int source, byte[] destination, int offset)
        {
            destination[offset] = (byte)(source >> 24);
            destination[offset + 1] = (byte)(source >> 16);
            destination[offset + 2] = (byte)(source >> 8);
            destination[offset + 3] = (byte)source;
            return 4;
        }

        public static int ToByteArray(uint source, byte[] destination, int offset)
        {
            destination[offset] = (byte)(source >> 24);
            destination[offset + 1] = (byte)(source >> 16);
            destination[offset + 2] = (byte)(source >> 8);
            destination[offset + 3] = (byte)source;
            return 4;
        }

        public static int ToByteArray(int source, Stream destination)
        {
            destination.WriteByte((byte)(source >> 24));
            destination.WriteByte((byte)(source >> 16));
            destination.WriteByte((byte)(source >> 8));
            destination.WriteByte((byte)source);
            return 4;
        }

        public static int ToByteArray(float source, byte[] destination, int offset)
        {
            FloatToByte(destination, offset, source);
            return 4;
        }

        public static int ToByteArray(float source, Stream destination)
        {
            FloatToByte(destination, source);
            return 4;
        }

        public static int ToByteArray(short source, byte[] destination, int offset)
        {
            destination[offset] = (byte)(source >> 8);
            destination[offset + 1] = (byte)(source);
            return 2;
        }

        public static int ToByteArray(ushort source, byte[] destination, int offset)
        {
            destination[offset] = (byte)(source >> 8);
            destination[offset + 1] = (byte)(source);
            return 2;
        }

        public static int ToByteArray(short source, Stream destination, int offset)
        {
            destination.WriteByte((byte)(source >> 8));
            destination.WriteByte((byte)(source));
            return 2;
        }

        public static int ToByteArray(string str, byte[] destination, int offset)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            return Encoding.UTF8.GetBytes(str, 0, str.Length, destination, offset);
        }

        /// <summary>
        /// Same as <see cref="ToByteArray(string, byte[], int)"/> but handles length by itself.<br/>
        /// Should be decoded with <see cref="FromByteArrayWithSize(byte[], int, out string)"/>
        /// </summary>
        public static int ToByteArrayWithSize(string str, byte[] destination, int offset)
        {
            if (string.IsNullOrEmpty(str))
            {
                return ToByteArray(0, destination, offset);
            }

            var length = Encoding.UTF8.GetBytes(str, 0, str.Length, destination, offset + 4);
            return ToByteArray(length, destination, offset) + length;
        }

        public static int ToByteArray(string str, Stream destination)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            var bytes = Encoding.UTF8.GetBytes(str);
            destination.Write(bytes, 0, bytes.Length);
            return bytes.Length;
        }

        public static int ToByteArrayWithSize(string str, Stream destination)
        {
            if (string.IsNullOrEmpty(str))
            {
                return ToByteArray(0, destination);
            }

            var bytes = Encoding.UTF8.GetBytes(str);
            int offset = ToByteArray(bytes.Length, destination);
            destination.Write(bytes, 0, bytes.Length);
            return offset + bytes.Length;
        }

        public static int ToByteArray(double source, byte[] destination, int offset)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(source), 0, destination, offset, sizeof(double));
            return sizeof(double);
        }

        public static int ToByteArray(double source, Stream destination)
        {
            var bytes = BitConverter.GetBytes(source);
            destination.Write(bytes, 0, bytes.Length);
            return sizeof(double);
        }

        public static int ToByteArray(byte[] source, byte[] destination, int offset)
        {
            Array.Copy(source, 0, destination, offset, source.Length);
            return source.Length;
        }

        public static int ToByteArray(byte[] source, int index, int count, Stream destination)
        {
            destination.Write(source, index, count);
            return count;
        }

        public static int ToByteArray(Int2 vector, byte[] destination, int offset)
        {
            int index = offset;
            index += ToByteArray(vector.x, destination, index);
            index += ToByteArray(vector.y, destination, index);
            return index - offset;
        }

        public static int ToByteArray(Guid guid, byte[] destination, int offset)
        {
            return ToByteArray(guid.ToByteArray(), destination, offset);
        }

        public static int ToByteArray(Guid guid, Stream destination)
        {
            var bytes = guid.ToByteArray();
            return ToByteArray(bytes, 0, bytes.Length, destination);
        }

        public static int FromByteArray(byte[] source, int offset, out byte destination)
        {
            destination = source[offset];
            return 1;
        }

        public static int FromByteArray(byte[] source, int offset, out bool destination)
        {
            destination = source[offset] == 1;
            return 1;
        }

        public static int FromByteArray(byte[] source, int offset, out long destination)
        {
            destination = 0;
            destination |= (long)source[offset] << 56;
            destination |= (long)source[offset + 1] << 48;
            destination |= (long)source[offset + 2] << 40;
            destination |= (long)source[offset + 3] << 32;
            destination |= (long)source[offset + 4] << 24;
            destination |= (long)source[offset + 5] << 16;
            destination |= (long)source[offset + 6] << 8;
            destination |= (long)source[offset + 7];
            return 8;
        }

        public static int FromByteArray(byte[] source, int offset, out int destination)
        {
            destination = 0;
            destination |= source[offset] << 24;
            destination |= source[offset + 1] << 16;
            destination |= source[offset + 2] << 8;
            destination |= source[offset + 3];
            return 4;
        }

        public static int FromByteArray(byte[] source, int offset, out uint destination)
        {
            destination = 0;
            destination |= (uint)(source[offset] << 24);
            destination |= (uint)(source[offset + 1] << 16);
            destination |= (uint)(source[offset + 2] << 8);
            destination |= source[offset + 3];
            return 4;
        }

        public static int FromByteArray(byte[] source, int offset, out short destination)
        {
            destination = 0;
            destination |= (short)(source[offset] << 8);
            destination |= (short)source[offset + 1];
            return 2;
        }

        public static int FromByteArray(byte[] source, int offset, out ushort destination)
        {
            destination = 0;
            destination |= (ushort)(source[offset] << 8);
            destination |= source[offset + 1];
            return 2;
        }

        public static int FromByteArray(byte[] source, int offset, out float destination)
        {
            destination = 0;
            destination = BitConverter.ToSingle(source, offset);
            return 4;
        }

        public static int FromByteArray(byte[] source, int offset, int length, out string destination)
        {
            destination = "";
            destination = Encoding.UTF8.GetString(source, offset, length);
            return length;
        }

        /// <summary>
        /// Decodes string which was encoded with <see cref="ToByteArrayWithSize(string, byte[], int)"/>
        /// </summary>
        public static int FromByteArrayWithSize(byte[] source, int offset, out string destination)
        {
            destination = string.Empty;
            int start = offset;
            int length;
            offset += FromByteArray(source, offset, out length);
            if (length > 0)
            {
                offset += FromByteArray(source, offset, length, out destination);
            }

            return offset - start;
        }

        public static int FromByteArray(byte[] source, int offset, out double destination)
        {
            destination = BitConverter.ToDouble(source, offset);
            return sizeof(double);
        }

        public static int FromByteArray(byte[] source, int offset, int size, out byte[] destination)
        {
            destination = new byte[size];
            Array.Copy(source, offset, destination, 0, size);
            return size;
        }

        public static int GetByteCount(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            return Encoding.UTF8.GetByteCount(str);
        }

        public static int FromByteArray(byte[] source, int offset, out Guid destination)
        {
            byte[] bytes;
            var len = FromByteArray(source, offset, 16, out bytes);
            destination = new Guid(bytes);
            return len;
        }

        public static void CopyStream(Stream input, Stream output, byte[] copyBuffer)
        {
            int read;
            while ((read = input.Read(copyBuffer, 0, copyBuffer.Length)) > 0)
            {
                output.Write(copyBuffer, 0, read);
            }
        }

        private static void FloatToByte(byte[] buffer, int offset, float value)
        {
            var tos = new ToSingle { Single = value };

            buffer[offset] = tos.Byte0;
            buffer[offset + 1] = tos.Byte1;
            buffer[offset + 2] = tos.Byte2;
            buffer[offset + 3] = tos.Byte3;
        }

        private static void FloatToByte(Stream buffer, float value)
        {
            var tos = new ToSingle { Single = value };

            buffer.WriteByte(tos.Byte0);
            buffer.WriteByte(tos.Byte1);
            buffer.WriteByte(tos.Byte2);
            buffer.WriteByte(tos.Byte3);
        }

        private static void ByteToFloat(byte[] buffer, int offset, out float value)
        {
            var tos = new ToSingle
            {
                Byte0 = buffer[offset],
                Byte1 = buffer[offset + 1],
                Byte2 = buffer[offset + 2],
                Byte3 = buffer[offset + 3],
            };

            value = tos.Single;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct ToSingle
        {
            [FieldOffset(0)]
            public float Single;

            [FieldOffset(0)]
            public byte Byte0;

            [FieldOffset(1)]
            public byte Byte1;

            [FieldOffset(2)]
            public byte Byte2;

            [FieldOffset(3)]
            public byte Byte3;
        }
    }
}
