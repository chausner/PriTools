using System;
using System.IO;
using System.Text;

namespace PriFormat;

public abstract class Section
{
    protected PriFile PriFile { get; private set; }
    public string SectionIdentifier { get; private set; }
    public uint SectionQualifier { get; private set; }
    public uint Flags { get; private set; }
    public uint SectionFlags { get; private set; }
    public uint SectionLength { get; private set; }

    protected Section(string sectionIdentifier, PriFile priFile)
    {
        if (sectionIdentifier.Length != 16)
            throw new ArgumentException("Section identifiers need to be exactly 16 characters long.", nameof(sectionIdentifier));

        SectionIdentifier = sectionIdentifier;
        PriFile = priFile;
    }

    internal bool Parse(BinaryReader binaryReader)
    {
        const long SectionHeaderSize = 16 + 4 + 2 + 2 + 4 + 4;
        const long SectionFooterSize = 4 + 4;

        if (new string(binaryReader.ReadChars(16)) != SectionIdentifier)
            throw new InvalidDataException("Unexpected section identifier.");

        SectionQualifier = binaryReader.ReadUInt32();
        Flags = binaryReader.ReadUInt16();
        SectionFlags = binaryReader.ReadUInt16();
        SectionLength = binaryReader.ReadUInt32();
        binaryReader.ExpectUInt32(0);

        long sectionContentPosition = binaryReader.BaseStream.Position;

        binaryReader.BaseStream.Seek(SectionLength - SectionHeaderSize - SectionFooterSize, SeekOrigin.Current);

        binaryReader.ExpectUInt32(0xDEF5FADE);
        binaryReader.ExpectUInt32(SectionLength);

        binaryReader.BaseStream.Seek(sectionContentPosition, SeekOrigin.Begin);

        using (SubStream subStream = new(binaryReader.BaseStream, sectionContentPosition, (int)SectionLength - SectionHeaderSize - SectionFooterSize))
        using (BinaryReader subBinaryReader = new(subStream, Encoding.ASCII))
        {
            return ParseSectionContent(subBinaryReader, sectionContentPosition);
        }
    }

    protected abstract bool ParseSectionContent(BinaryReader binaryReader, long sectionContentPosition);

    public override string ToString()
    {
        return $"{SectionIdentifier.TrimEnd('\0', ' ')} length: {SectionLength}";
    }

    internal static Section CreateForIdentifier(string sectionIdentifier, PriFile priFile)
    {
        return sectionIdentifier switch
        {
            PriDescriptorSection.Identifier => new PriDescriptorSection(priFile),
            HierarchicalSchemaSection.Identifier1 => new HierarchicalSchemaSection(priFile, false),
            HierarchicalSchemaSection.Identifier2 => new HierarchicalSchemaSection(priFile, true),
            DecisionInfoSection.Identifier => new DecisionInfoSection(priFile),
            ResourceMapSection.Identifier1 => new ResourceMapSection(priFile, false),
            ResourceMapSection.Identifier2 => new ResourceMapSection(priFile, true),
            DataItemSection.Identifier => new DataItemSection(priFile),
            ReverseMapSection.Identifier => new ReverseMapSection(priFile),
            ReferencedFileSection.Identifier => new ReferencedFileSection(priFile),
            _ => new UnknownSection(sectionIdentifier, priFile),
        };
    }
}
