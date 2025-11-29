using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriFormat;

public class ReverseMapSection : Section
{
    public uint[] Mapping { get; private set; }
    public IReadOnlyList<ResourceMapScope> Scopes { get; private set; }
    public IReadOnlyList<ResourceMapItem> Items { get; private set; }

    internal const string Identifier = "[mrm_rev_map]  \0";

#pragma warning disable CS8618
    internal ReverseMapSection(PriFile priFile) : base(Identifier, priFile)
#pragma warning restore CS8618
    {
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
    {
        uint numItems = binaryReader.ReadUInt32();
        binaryReader.ExpectUInt32((uint)(binaryReader.BaseStream.Length - 8));

        uint[] mapping = new uint[numItems];
        for (int i = 0; i < numItems; i++)
            mapping[i] = binaryReader.ReadUInt32();
        Mapping = mapping;

        ushort maxFullPathLength = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);
        uint numEntries = binaryReader.ReadUInt32();
        uint numScopes = binaryReader.ReadUInt32();
        binaryReader.ExpectUInt32(numItems);
        uint unicodeDataLength = binaryReader.ReadUInt32();
        binaryReader.ReadUInt32();

        List<ScopeAndItemInfo> scopeAndItemInfo = new((int)(numScopes + numItems));

        for (int i = 0; i < numScopes + numItems; i++)
        {
            ushort parent = binaryReader.ReadUInt16();
            ushort fullPathLength = binaryReader.ReadUInt16();
            uint hashCode = binaryReader.ReadUInt32();
            uint nameOffset = binaryReader.ReadUInt16() | (((hashCode >> 24) & 0xF) << 16);
            ushort index = binaryReader.ReadUInt16();
            scopeAndItemInfo.Add(new ScopeAndItemInfo(parent, fullPathLength, hashCode, nameOffset, index));
        }

        List<ScopeExInfo> scopeExInfo = new((int)numScopes);

        for (int i = 0; i < numScopes; i++)
        {
            ushort scopeIndex = binaryReader.ReadUInt16();
            ushort childCount = binaryReader.ReadUInt16();
            ushort firstChildIndex = binaryReader.ReadUInt16();
            binaryReader.ExpectUInt16(0);
            scopeExInfo.Add(new ScopeExInfo(scopeIndex, childCount, firstChildIndex));
        }

        ushort[] itemIndexPropertyToIndex = new ushort[numItems];
        for (int i = 0; i < numItems; i++)
        {
            itemIndexPropertyToIndex[i] = binaryReader.ReadUInt16();
        }

        long unicodeDataOffset = binaryReader.BaseStream.Position;
        long asciiDataOffset = binaryReader.BaseStream.Position + unicodeDataLength * 2;

        ResourceMapScope[] scopes = new ResourceMapScope[numScopes];
        ResourceMapItem[] items = new ResourceMapItem[numItems];

        for (int i = 0; i < numScopes + numItems; i++)
        {
            bool nameInAscii = (scopeAndItemInfo[i].Flags & 0x20000000) != 0;
            long pos = (nameInAscii ? asciiDataOffset : unicodeDataOffset) + (scopeAndItemInfo[i].NameOffset * (nameInAscii ? 1 : 2));

            binaryReader.BaseStream.Seek(pos, SeekOrigin.Begin);

            string name;

            if (scopeAndItemInfo[i].FullPathLength != 0)
                name = binaryReader.ReadNullTerminatedString(nameInAscii ? Encoding.ASCII : Encoding.Unicode);
            else
                name = string.Empty;

            ushort index = scopeAndItemInfo[i].Index;

            bool isScope = (scopeAndItemInfo[i].Flags & 0x10000000) != 0;

            if (isScope)
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
            ushort index = scopeAndItemInfo[i].Index;

            bool isScope = (scopeAndItemInfo[i].Flags & 0x10000000) != 0;

            ushort parent = scopeAndItemInfo[i].Parent;

            parent = scopeAndItemInfo[parent].Index;

            if (parent != 0xFFFF)
                if (isScope)
                {
                    if (parent != index)
                        scopes[index].Parent = scopes[parent];
                }
                else
                    items[index].Parent = scopes[parent];
        }

        for (int i = 0; i < numScopes; i++)
        {
            ResourceMapEntry[] children = new ResourceMapEntry[scopeExInfo[i].ChildCount];

            for (int j = 0; j < children.Length; j++)
            {
                var saiInfo = scopeAndItemInfo[scopeExInfo[i].FirstChildIndex + j];

                bool isScope = (saiInfo.Flags & 0x10000000) != 0;

                if (isScope)
                    children[j] = scopes[saiInfo.Index];
                else
                    children[j] = items[saiInfo.Index];
            }

            scopes[i].Children = children;
        }

        Scopes = scopes;
        Items = items;

        return true;
    }

    private record struct ScopeAndItemInfo(ushort Parent, ushort FullPathLength, uint Flags, uint NameOffset, ushort Index);

    private record struct ScopeExInfo(ushort ScopeIndex, ushort ChildCount, ushort FirstChildIndex);
}
