using System;
using System.IO;
using System.Text;

namespace PriFormat;

public class EnvironmentReference
{
    internal const int RecordSize = 0x22C;
    const int NameBufferLengthBytes = 0x204;

    public string Name { get; }
    public EnvironmentVersionInfo VersionInfo { get; }
    public uint QualifierTypeTableOffset { get; }
    public uint QualifierTableOffset { get; }
    public uint ItemTypeTableOffset { get; }
    public uint ResourceValueTypeTableOffset { get; }
    public uint ValueLocatorTableOffset { get; }
    public uint ConditionOperatorTableOffset { get; }

    internal EnvironmentReference(string name, EnvironmentVersionInfo versionInfo, uint qualifierTypeTableOffset,
        uint qualifierTableOffset, uint itemTypeTableOffset, uint resourceValueTypeTableOffset,
        uint valueLocatorTableOffset, uint conditionOperatorTableOffset)
    {
        Name = name;
        VersionInfo = versionInfo;
        QualifierTypeTableOffset = qualifierTypeTableOffset;
        QualifierTableOffset = qualifierTableOffset;
        ItemTypeTableOffset = itemTypeTableOffset;
        ResourceValueTypeTableOffset = resourceValueTypeTableOffset;
        ValueLocatorTableOffset = valueLocatorTableOffset;
        ConditionOperatorTableOffset = conditionOperatorTableOffset;
    }

    internal static EnvironmentReference Read(BinaryReader reader)
    {
        byte[] nameBuffer = reader.ReadBytes(NameBufferLengthBytes);
        string name = Encoding.Unicode.GetString(nameBuffer);
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

public class EnvironmentVersionInfo
{
    public ushort MajorVersion { get; }
    public ushort MinorVersion { get; }
    public uint Checksum { get; }
    public ushort NumQualifierTypes { get; }
    public ushort NumQualifiers { get; }
    public ushort NumItemTypes { get; }
    public ushort NumResourceValueTypes { get; }
    public ushort NumResourceValueLocators { get; }
    public ushort NumConditionOperators { get; }

    internal EnvironmentVersionInfo(
        ushort majorVersion,
        ushort minorVersion,
        uint checksum,
        ushort numQualifierTypes,
        ushort numQualifiers,
        ushort numItemTypes,
        ushort numResourceValueTypes,
        ushort numResourceValueLocators,
        ushort numConditionOperators)
    {
        MajorVersion = majorVersion;
        MinorVersion = minorVersion;
        Checksum = checksum;
        NumQualifierTypes = numQualifierTypes;
        NumQualifiers = numQualifiers;
        NumItemTypes = numItemTypes;
        NumResourceValueTypes = numResourceValueTypes;
        NumResourceValueLocators = numResourceValueLocators;
        NumConditionOperators = numConditionOperators;
    }
}
