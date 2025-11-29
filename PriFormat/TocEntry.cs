using System.IO;

namespace PriFormat;

public record class TocEntry
(
    string SectionIdentifier,
    ushort Flags,
    ushort SectionFlags,
    uint SectionQualifier,
    uint SectionOffset,
    uint SectionLength
)
{
    internal static TocEntry Parse(BinaryReader binaryReader)
    {
        return new TocEntry(
            new string(binaryReader.ReadChars(16)),
            binaryReader.ReadUInt16(),
            binaryReader.ReadUInt16(),
            binaryReader.ReadUInt32(),
            binaryReader.ReadUInt32(),
            binaryReader.ReadUInt32());
    }

    public override string ToString()
    {
        return $"{SectionIdentifier.TrimEnd('\0', ' ')} length: {SectionLength}";
    }
}
