using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Text;

namespace PriFormat;

public class HierarchicalSchemaSection : Section
{
    public HierarchicalSchemaVersionInfo? Version { get; private set; }
    public string? UniqueName { get; private set; }
    public string? Name { get; private set; }
    public IReadOnlyList<ResourceMapScope> Scopes { get; private set; }
    public IReadOnlyList<ResourceMapItem> Items { get; private set; }

    readonly bool extendedVersion;
    ushort[] scopeIndicesInFileOrder;
    ushort[] itemIndicesInFileOrder;

    internal const string Identifier1 = "[mrm_hschema]  \0";
    internal const string Identifier2 = "[mrm_hschemaex] ";

#pragma warning disable CS8618
    internal HierarchicalSchemaSection(PriFile priFile, bool extendedVersion) : base(extendedVersion ? Identifier2 : Identifier1, priFile)
#pragma warning restore CS8618
    {
        this.extendedVersion = extendedVersion;
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader, long sectionContentPosition)
    {
        if (binaryReader.BaseStream.Length == 0)
        {
            Version = null;
            UniqueName = null;
            Name = null;
            Scopes = [];
            Items = [];
            scopeIndicesInFileOrder = [];
            itemIndicesInFileOrder = [];
            return true;
        }

        binaryReader.ExpectUInt16(1);
        ushort uniqueNameLength = binaryReader.ReadUInt16();
        ushort nameLength = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);

        bool extendedHNames;
        if (extendedVersion)
        {
            extendedHNames = new string(binaryReader.ReadChars(16)) switch
            {
                "[def_hnamesx]  \0" => true,
                "[def_hnames]   \0" => false,
                _ => throw new InvalidDataException(),
            };
        }
        else
            extendedHNames = false;

        // hierarchical schema version info
        ushort majorVersion = binaryReader.ReadUInt16();
        ushort minorVersion = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt32(0);
        uint checksum = binaryReader.ReadUInt32();
        uint numScopes = binaryReader.ReadUInt32();
        uint numItems = binaryReader.ReadUInt32();

        Version = new HierarchicalSchemaVersionInfo(majorVersion, minorVersion, checksum, numScopes, numItems);

        UniqueName = binaryReader.ReadNullTerminatedString(Encoding.Unicode, uniqueNameLength);
        Name = binaryReader.ReadNullTerminatedString(Encoding.Unicode, nameLength);

        binaryReader.ExpectUInt16(0);

        long hNamesDataOffset = binaryReader.BaseStream.Position;

        ushort maxFullPathLength = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);
        binaryReader.ExpectUInt32(numScopes + numItems);
        binaryReader.ExpectUInt32(numScopes);
        binaryReader.ExpectUInt32(numItems);
        uint unicodeDataLength = binaryReader.ReadUInt32();
        uint totalHNamesLength = binaryReader.ReadUInt32();
        uint asciiDataLength = extendedHNames ? binaryReader.ReadUInt32() : 0;

        List<ScopeAndItemInfo> scopeAndItemInfos = new((int)(numScopes + numItems));

        for (int i = 0; i < numScopes + numItems; i++)
        {
            ushort parent = binaryReader.ReadUInt16();
            ushort fullPathLength = binaryReader.ReadUInt16();
            char uppercaseFirstChar = (char)binaryReader.ReadUInt16();
            byte nameLength2 = binaryReader.ReadByte();
            byte flags = binaryReader.ReadByte();
            uint nameOffset = binaryReader.ReadUInt16() | (uint)((flags & 0xF) << 16);
            ushort index = binaryReader.ReadUInt16();
            scopeAndItemInfos.Add(new ScopeAndItemInfo(parent, fullPathLength, flags, nameOffset, index));
        }

        List<ScopeExInfo> scopeExInfos = new((int)numScopes);

        for (int i = 0; i < numScopes; i++)
        {
            ushort scopeIndex = binaryReader.ReadUInt16();
            ushort childCount = binaryReader.ReadUInt16();
            ushort firstChildIndex = binaryReader.ReadUInt16();
            binaryReader.ExpectUInt16(0);
            scopeExInfos.Add(new ScopeExInfo(scopeIndex, childCount, firstChildIndex));
        }

        ushort[] itemIndexPropertyToIndex = new ushort[numItems];
        for (int i = 0; i < numItems; i++)
            itemIndexPropertyToIndex[i] = binaryReader.ReadUInt16();

        long unicodeDataOffset = binaryReader.BaseStream.Position;

        // Skip Unicode name block
        binaryReader.BaseStream.Seek(unicodeDataLength * 2, SeekOrigin.Current);

        long asciiDataOffset = binaryReader.BaseStream.Position;

        // Skip ASCII name block
        binaryReader.BaseStream.Seek(asciiDataLength, SeekOrigin.Current);

        if (Math.Align(binaryReader.BaseStream.Position - hNamesDataOffset, 8) != totalHNamesLength)
            throw new InvalidDataException();

        if (Math.Align(hNamesDataOffset + totalHNamesLength, 8) != binaryReader.BaseStream.Length)
            throw new InvalidDataException();

        ResourceMapScope[] scopes = new ResourceMapScope[numScopes];
        ResourceMapItem[] items = new ResourceMapItem[numItems];

        for (int i = 0; i < numScopes + numItems; i++)
        {
            long pos;

            if (scopeAndItemInfos[i].NameInAscii)
                pos = asciiDataOffset + scopeAndItemInfos[i].NameOffset;
            else
                pos = unicodeDataOffset + scopeAndItemInfos[i].NameOffset * 2;

            binaryReader.BaseStream.Seek(pos, SeekOrigin.Begin);

            string name;

            if (scopeAndItemInfos[i].FullPathLength != 0)
                name = binaryReader.ReadNullTerminatedString(scopeAndItemInfos[i].NameInAscii ? Encoding.ASCII : Encoding.Unicode);
            else
                name = string.Empty;

            ushort index = scopeAndItemInfos[i].Index;

            if (scopeAndItemInfos[i].IsScope)
            {
                if (scopes[index] != null)
                    throw new InvalidDataException();

                scopes[index] = new ResourceMapScope(index, null, name);
            }
            else
            {
                if (items[index] != null)
                    throw new InvalidDataException();

                items[index] = new ResourceMapItem(index, null!, name);
            }
        }

        for (int i = 0; i < numScopes + numItems; i++)
        {
            ushort index = scopeAndItemInfos[i].Index;

            ushort parent = scopeAndItemInfos[scopeAndItemInfos[i].Parent].Index;

            if (parent != 0xFFFF)
                if (scopeAndItemInfos[i].IsScope)
                {
                    if (parent != index)
                        scopes[index].Parent = scopes[parent];
                }
                else
                    items[index].Parent = scopes[parent];
        }

        for (int i = 0; i < numScopes; i++)
        {
            List<ResourceMapEntry> children = new(scopeExInfos[i].ChildCount);

            for (int j = 0; j < scopeExInfos[i].ChildCount; j++)
            {
                ScopeAndItemInfo saiInfo = scopeAndItemInfos[scopeExInfos[i].FirstChildIndex + j];

                if (saiInfo.IsScope)
                    children.Add(scopes[saiInfo.Index]);
                else
                    children.Add(items[saiInfo.Index]);
            }

            scopes[i].Children = children;
        }

        Scopes = scopes;
        Items = items;
        scopeIndicesInFileOrder = scopeExInfos
            .Select(scopeExInfo => scopeAndItemInfos[scopeExInfo.ScopeIndex].Index)
            .ToArray();
        itemIndicesInFileOrder = itemIndexPropertyToIndex
            .Select(nodeIndex => scopeAndItemInfos[nodeIndex].Index)
            .ToArray();

        return true;
    }

    private record struct ScopeAndItemInfo
    (
        ushort Parent,
        ushort FullPathLength,
        byte Flags,
        uint NameOffset,
        ushort Index
    )
    {
        public bool IsScope => (Flags & 0x10) != 0;
        public bool NameInAscii => (Flags & 0x20) != 0;
    }

    private record struct ScopeExInfo
    (
        ushort ScopeIndex,
        ushort ChildCount,
        ushort FirstChildIndex
    );

    public uint ComputeHierarchicalSchemaVersionInfoChecksum()
    {
        Crc32 crc32 = new Crc32();

        StringChecksum(crc32, UniqueName);
        StringChecksum(crc32, Name);
        crc32.Append(BitConverter.GetBytes(Version!.MajorVersion));
        crc32.Append(BitConverter.GetBytes(Version.MinorVersion));

        crc32.Append(BitConverter.GetBytes(0));
        crc32.Append(BitConverter.GetBytes(0));
        crc32.Append(BitConverter.GetBytes(1));

        crc32.Append(BitConverter.GetBytes(Version.NumScopes));
        foreach (ushort scopeIndex in scopeIndicesInFileOrder)
            StringChecksum(crc32, Scopes[scopeIndex].FullName.Replace('\\', '/').TrimStart('/'));

        crc32.Append(BitConverter.GetBytes(0));
        crc32.Append(BitConverter.GetBytes(0));
        crc32.Append(BitConverter.GetBytes(1));

        crc32.Append(BitConverter.GetBytes(Version.NumItems));
        foreach (ushort itemIndex in itemIndicesInFileOrder)
            StringChecksum(crc32, Items[itemIndex].FullName.Replace('\\', '/').TrimStart('/'));

        return crc32.GetCurrentHashAsUInt32();

        static void StringChecksum(Crc32 crc32, string? s)
        {
            if (s == null)
            {
                byte[] data = new byte[8];
                crc32.Append(data);
            }
            else
            {
                byte[] data = Encoding.Unicode.GetBytes(ToLowerAscii(s) + '\0');
                byte[] length = BitConverter.GetBytes(data.Length);

                crc32.Append(length);
                crc32.Append(data);
            }
        }

        static string ToLowerAscii(string s)
        {
            StringBuilder result = new(s.Length);

            foreach (char c in s)
                if (c is >= 'A' and <= 'Z')
                    result.Append(char.ToLowerInvariant(c));
                else
                    result.Append(c);

            return result.ToString();
        }
    }
}

public record HierarchicalSchemaVersionInfo
(
    ushort MajorVersion,
    ushort MinorVersion,
    uint Checksum,
    uint NumScopes,
    uint NumItems
);

public abstract class ResourceMapEntry
{
    public ushort Index { get; }
    public ResourceMapScope? Parent { get; internal set; }
    public string Name { get; }

    internal ResourceMapEntry(ushort index, ResourceMapScope? parent, string name)
    {
        Index = index;
        Parent = parent;
        Name = name;
    }

    string? fullName;

    public string FullName
    {
        get
        {
            if (fullName == null)
                if (Parent == null)
                    fullName = Name;
                else
                    fullName = Parent.FullName + "\\" + Name;

            return fullName;
        }
    }
}

public sealed class ResourceMapScope : ResourceMapEntry
{
#pragma warning disable CS8618
    internal ResourceMapScope(ushort index, ResourceMapScope? parent, string name) : base(index, parent, name)
#pragma warning restore CS8618
    {
    }

    public IReadOnlyList<ResourceMapEntry> Children { get; internal set; }

    public override string ToString()
    {
        return $"Scope {Index} {FullName} ({Children.Count} children)";
    }
}

public sealed class ResourceMapItem : ResourceMapEntry
{
    internal ResourceMapItem(ushort index, ResourceMapScope parent, string name) : base(index, parent, name)
    {
    }

    public override string ToString()
    {
        return $"Item {Index} {FullName}";
    }
}
