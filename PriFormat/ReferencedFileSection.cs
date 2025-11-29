using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriFormat;

public class ReferencedFileSection : Section
{
    public IReadOnlyList<ReferencedFile> ReferencedFiles { get; private set; }

    internal const string Identifier = "[def_file_list]\0";

#pragma warning disable CS8618
    internal ReferencedFileSection(PriFile priFile) : base(Identifier, priFile)
#pragma warning restore CS8618
    {
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
    {
        ushort numRoots = binaryReader.ReadUInt16();
        ushort numFolders = binaryReader.ReadUInt16();
        ushort numFiles = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);
        uint totalDataLength = binaryReader.ReadUInt32();

        List<FolderInfo> folderInfos = new(numFolders);

        for (int i = 0; i < numFolders; i++)
        {
            binaryReader.ExpectUInt16(0);
            ushort parentFolder = binaryReader.ReadUInt16();
            ushort numFoldersInFolder = binaryReader.ReadUInt16();
            ushort firstFolderInFolder = binaryReader.ReadUInt16();
            ushort numFilesInFolder = binaryReader.ReadUInt16();
            ushort firstFileInFolder = binaryReader.ReadUInt16();
            ushort folderNameLength = binaryReader.ReadUInt16();
            ushort fullPathLength = binaryReader.ReadUInt16();
            uint folderNameOffset = binaryReader.ReadUInt32();
            folderInfos.Add(new FolderInfo(parentFolder, numFoldersInFolder, firstFolderInFolder, numFilesInFolder, firstFileInFolder, folderNameLength, fullPathLength, folderNameOffset));
        }

        List<FileInfo> fileInfos = new(numFiles);

        for (int i = 0; i < numFiles; i++)
        {
            binaryReader.ReadUInt16();
            ushort parentFolder = binaryReader.ReadUInt16();
            ushort fullPathLength = binaryReader.ReadUInt16();
            ushort fileNameLength = binaryReader.ReadUInt16();
            uint fileNameOffset = binaryReader.ReadUInt32();
            fileInfos.Add(new FileInfo(parentFolder, fullPathLength, fileNameLength, fileNameOffset));
        }

        long dataStartPosition = binaryReader.BaseStream.Position;

        List<ReferencedFolder> referencedFolders = new(numFolders);

        for (int i = 0; i < numFolders; i++)
        {
            binaryReader.BaseStream.Seek(dataStartPosition + folderInfos[i].FolderNameOffset * 2, SeekOrigin.Begin);

            string name = binaryReader.ReadString(Encoding.Unicode, folderInfos[i].FolderNameLength);

            referencedFolders.Add(new ReferencedFolder(null, name));
        }

        for (int i = 0; i < numFolders; i++)
            if (folderInfos[i].ParentFolder != 0xFFFF)
                referencedFolders[i].Parent = referencedFolders[folderInfos[i].ParentFolder];

        List<ReferencedFile> referencedFiles = new(numFiles);

        for (int i = 0; i < numFiles; i++)
        {
            binaryReader.BaseStream.Seek(dataStartPosition + fileInfos[i].FileNameOffset * 2, SeekOrigin.Begin);

            string name = binaryReader.ReadString(Encoding.Unicode, fileInfos[i].FileNameLength);

            ReferencedFolder? parentFolder;

            if (fileInfos[i].ParentFolder != 0xFFFF)
                parentFolder = referencedFolders[fileInfos[i].ParentFolder];
            else
                parentFolder = null;

            referencedFiles.Add(new ReferencedFile(parentFolder, name));
        }

        for (int i = 0; i < numFolders; i++)
        {
            List<ReferencedEntry> children = new(folderInfos[i].NumFoldersInFolder + folderInfos[i].NumFilesInFolder);

            for (int j = 0; j < folderInfos[i].NumFoldersInFolder; j++)
                children.Add(referencedFolders[folderInfos[i].FirstFolderInFolder + j]);

            for (int j = 0; j < folderInfos[i].NumFilesInFolder; j++)
                children.Add(referencedFiles[folderInfos[i].FirstFileInFolder + j]);

            referencedFolders[i].Children = children;
        }

        ReferencedFiles = referencedFiles;

        return true;
    }

    private record struct FolderInfo
    (
        ushort ParentFolder,
        ushort NumFoldersInFolder,
        ushort FirstFolderInFolder,
        ushort NumFilesInFolder,
        ushort FirstFileInFolder,
        ushort FolderNameLength,
        ushort FullPathLength,
        uint FolderNameOffset
    );

    private record struct FileInfo
    (
        ushort ParentFolder,
        ushort FullPathLength,
        ushort FileNameLength,
        uint FileNameOffset
    );
}

public abstract class ReferencedEntry
{
    public ReferencedFolder? Parent { get; internal set; }
    public string Name { get; }

    internal ReferencedEntry(ReferencedFolder? parent, string name)
    {
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

public sealed class ReferencedFolder : ReferencedEntry
{
#pragma warning disable CS8618
    internal ReferencedFolder(ReferencedFolder? parent, string name) : base(parent, name)
#pragma warning restore CS8618
    {
    }

    public IReadOnlyList<ReferencedEntry> Children { get; internal set; }
}

public sealed class ReferencedFile : ReferencedEntry
{
    internal ReferencedFile(ReferencedFolder? parent, string name) : base(parent, name)
    {
    }
}

public struct ReferencedFileRef
{
    internal int fileIndex;

    internal ReferencedFileRef(int fileIndex)
    {
        this.fileIndex = fileIndex;
    }
}
