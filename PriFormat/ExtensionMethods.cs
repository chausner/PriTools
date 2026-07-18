using System;
using System.IO;
using System.Text;

namespace PriFormat;

internal static class ExtensionMethods
{
    extension(BinaryReader reader)
    {
        public string ReadString(Encoding encoding, int length)
        {
            using BinaryReader r = new(reader.BaseStream, encoding, true);

            char[] chars = r.ReadChars(length);
            if (chars.Length != length)
                throw new EndOfStreamException();

            return new string(chars);
        }

        public string ReadNullTerminatedString(Encoding encoding)
        {
            using BinaryReader r = new(reader.BaseStream, encoding, true);

            StringBuilder result = new();

            char c;
            while ((c = r.ReadChar()) != '\0')
                result.Append(c);

            return result.ToString();
        }

        public string ReadNullTerminatedString(Encoding encoding, int length)
        {
            string result = reader.ReadString(encoding, length);

            int terminatorIndex = result.IndexOf('\0');
            if (terminatorIndex != -1)
                result = result[..terminatorIndex];

            return result;
        }

        public void ExpectByte(byte expectedValue)
        {
            if (reader.ReadByte() != expectedValue)
                throw new InvalidDataException("Unexpected value read.");
        }

        public void ExpectUInt16(ushort expectedValue)
        {
            if (reader.ReadUInt16() != expectedValue)
                throw new InvalidDataException("Unexpected value read.");
        }

        public void ExpectUInt32(uint expectedValue)
        {
            if (reader.ReadUInt32() != expectedValue)
                throw new InvalidDataException("Unexpected value read.");
        }

        public void ExpectString(string s)
        {
            if (new string(reader.ReadChars(s.Length)) != s)
                throw new InvalidDataException("Unexpected value read.");
        }
    }

    extension(Math)
    {
        public static long Align(long value, long alignment)
        {
            long remainder = value % alignment;

            if (remainder == 0)
                return value;
            else
                return value + (alignment - remainder);
        }
    }
}
