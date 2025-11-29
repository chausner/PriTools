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
        if (numEntries != numScopes + numItems)
            throw new InvalidDataException();
        binaryReader.ExpectUInt32(numItems);
        uint unicodeDataLength = binaryReader.ReadUInt32();
        binaryReader.ReadUInt32(); // meaning unknown

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
        long asciiDataOffset = binaryReader.BaseStream.Position + unicodeDataLength * 2;

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
}
