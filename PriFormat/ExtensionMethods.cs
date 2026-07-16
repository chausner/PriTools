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

            return new string(r.ReadChars(length));
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
}
