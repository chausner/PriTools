using System.IO;
using System.Text;

namespace PriFormat;

public record EnvironmentReference(
    string Name,
    EnvironmentVersionInfo VersionInfo,
    uint QualifierTypeTableOffset,
    uint QualifierTableOffset,
    uint ItemTypeTableOffset,
    uint ResourceValueTypeTableOffset,
    uint ValueLocatorTableOffset,
    uint ConditionOperatorTableOffset)
{
    internal const int RecordSize = 0x22C;

    internal static EnvironmentReference Read(BinaryReader reader)
    {
        string name = reader.ReadString(Encoding.Unicode, 0x100);
        int terminatorIndex = name.IndexOf('\0');
        if (terminatorIndex >= 0)
            name = name[..terminatorIndex];

        ushort majorVersion = reader.ReadUInt16();
        ushort minorVersion = reader.ReadUInt16();
        uint checksum = reader.ReadUInt32();
        ushort numQualifierTypes = reader.ReadUInt16();
        ushort numQualifiers = reader.ReadUInt16();
        ushort numItemTypes = reader.ReadUInt16();
        ushort numValueTypes = reader.ReadUInt16();
        ushort numValueLocators = reader.ReadUInt16();
        ushort numConditionOperators = reader.ReadUInt16();

        uint qualifierTypeTableOffset = reader.ReadUInt32();
        uint qualifierTableOffset = reader.ReadUInt32();
        uint itemTypeTableOffset = reader.ReadUInt32();
        uint resourceValueTypeTableOffset = reader.ReadUInt32();
        uint valueLocatorTableOffset = reader.ReadUInt32();
        uint conditionOperatorTableOffset = reader.ReadUInt32();

        EnvironmentVersionInfo versionInfo = new EnvironmentVersionInfo(
            majorVersion,
            minorVersion,
            checksum,
            numQualifierTypes,
            numQualifiers,
            numItemTypes,
            numValueTypes,
            numValueLocators,
            numConditionOperators);

        return new EnvironmentReference(
            name,
            versionInfo,
            qualifierTypeTableOffset,
            qualifierTableOffset,
            itemTypeTableOffset,
            resourceValueTypeTableOffset,
            valueLocatorTableOffset,
            conditionOperatorTableOffset);
    }
}

public record EnvironmentVersionInfo(
    ushort MajorVersion,
    ushort MinorVersion,
    uint Checksum,
    ushort NumQualifierTypes,
    ushort NumQualifiers,
    ushort NumItemTypes,
    ushort NumResourceValueTypes,
    ushort NumResourceValueLocators,
    ushort NumConditionOperators);
