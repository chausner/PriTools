using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public class XbfHeader
    {
        public XbfHeader(BinaryReader reader)
        {
            // Verify magic number
            var magicNumber = reader.ReadBytes(4);
            if (magicNumber[0] != 'X' || magicNumber[1] != 'B' || magicNumber[2] != 'F' || magicNumber[3] != 0)
                throw new InvalidDataException("File does not have XBF header");
            MagicNumber = magicNumber;

            MetadataSize = reader.ReadUInt32();
            NodeSize = reader.ReadUInt32();
            MajorFileVersion = reader.ReadUInt32();
            MinorFileVersion = reader.ReadUInt32();
            StringTableOffset = reader.ReadUInt64();
            AssemblyTableOffset = reader.ReadUInt64();
            TypeNamespaceTableOffset = reader.ReadUInt64();
            TypeTableOffset = reader.ReadUInt64();
            PropertyTableOffset = reader.ReadUInt64();
            XmlNamespaceTableOffset = reader.ReadUInt64();
            Hash = reader.ReadChars(32);
        }

        public byte[] MagicNumber { get; private set; }
        public UInt32 MetadataSize { get; private set; }
        public UInt32 NodeSize { get; private set; }
        public UInt32 MajorFileVersion { get; private set; }
        public UInt32 MinorFileVersion { get; private set; }
        public UInt64 StringTableOffset { get; private set; }
        public UInt64 AssemblyTableOffset { get; private set; }
        public UInt64 TypeNamespaceTableOffset { get; private set; }
        public UInt64 TypeTableOffset { get; private set; }
        public UInt64 PropertyTableOffset { get; private set; }
        public UInt64 XmlNamespaceTableOffset { get; private set; }
        public char[] Hash { get; private set; }
    }
}
