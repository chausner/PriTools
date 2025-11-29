using System.IO;

namespace PriFormat;

public class UnknownSection : Section
{
    public byte[] SectionContent { get; private set; }

#pragma warning disable CS8618
    internal UnknownSection(string sectionIdentifier, PriFile priFile) : base(sectionIdentifier, priFile)
#pragma warning restore CS8618
    {
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
    {
        int contentLength = (int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position);

        SectionContent = binaryReader.ReadBytes(contentLength);

        return true;
    }
}
