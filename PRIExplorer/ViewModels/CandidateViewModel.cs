using Microsoft.Win32;
using PriFormat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PRIExplorer.ViewModels;

public class CandidateViewModel
{
    PriFile priFile;
    Stream priStream;
    string resourceRootPath;
    ResourceMapItem resourceMapItem;

    public Candidate Candidate { get; }

    IReadOnlyList<Qualifier> qualifiers;

    public string QualifiersDescription
    {
        get
        {
            if (qualifiers.Count == 0)
                return "(none)";
            else
                return string.Join(", ", qualifiers.Select(q => q.Type + "=" + q.Value));
        }
    }

    private string FullLocationPath
    {
        get
        {
            string rootPath;

            if (Candidate.SourceFile == null)
                rootPath = resourceRootPath;
            else
                rootPath = Path.GetDirectoryName(priFile.GetReferencedFileByRef(Candidate.SourceFile.Value).FullName);

            return Path.Combine(rootPath, locationPath);
        }
    }

    public bool SourceNotFound { get; private set; }
    public bool LocationNotFound { get; private set; }

    string locationPath;

    public string Location { get; }

    public RelayCommand GoToLocationCommand { get; }
    public RelayCommand SaveAsCommand { get; }

    public CandidateViewModel(PriFile priFile, Stream priStream, string resourceRootPath, ResourceMapItem resourceMapItem, Candidate candidate)
    {
        GoToLocationCommand = new RelayCommand(GoToLocationCommand_CanExecute, GoToLocationCommand_Execute);
        SaveAsCommand = new RelayCommand(SaveAsCommand_CanExecute, SaveAsCommand_Execute);

        this.priFile = priFile;
        this.priStream = priStream;
        this.resourceRootPath = resourceRootPath;
        this.resourceMapItem = resourceMapItem;
        Candidate = candidate;

        DecisionInfoSection decisionInfoSection =
            priFile.GetSectionByRef(priFile.PriDescriptorSection.DecisionInfoSections.First());

        qualifiers = decisionInfoSection.QualifierSets[candidate.QualifierSet].Qualifiers;

        if (candidate.Type == ResourceValueType.AsciiPath ||
            candidate.Type == ResourceValueType.Utf8Path ||
            candidate.Type == ResourceValueType.Path)
        {
            string path = (string)GetData();

            if (path != null)
            {
                locationPath = path;
                Location = path;

                if (!File.Exists(FullLocationPath))
                    LocationNotFound = true;
            }
            else
                Location = "";
        }
        else
            Location = "(embedded)";

        if (candidate.SourceFile != null)
        {
            string sourcePath = priFile.GetReferencedFileByRef(candidate.SourceFile.Value).FullName;

            if (File.Exists(sourcePath))
                Location = sourcePath + ": " + Location;
            else
            {
                Location = sourcePath + " (not found)";
                SourceNotFound = true;
            }
        }
    }

    public object GetData()
    {
        byte[] data;

        if (Candidate.SourceFile == null)
        {
            ByteSpan byteSpan;

            if (Candidate.DataItem != null)
                byteSpan = priFile.GetDataItemByRef(Candidate.DataItem.Value);
            else
                byteSpan = Candidate.Data.Value;

            priStream.Seek(byteSpan.Offset, SeekOrigin.Begin);

            using (BinaryReader binaryReader = new BinaryReader(priStream, Encoding.Default, true))
                data = binaryReader.ReadBytes((int)byteSpan.Length);
        }
        else
        {
            string sourcePath = priFile.GetReferencedFileByRef(Candidate.SourceFile.Value).FullName;

            if (!File.Exists(sourcePath))
                return null;

            using (FileStream fileStream = File.OpenRead(sourcePath))
            {
                PriFile sourcePriFile = PriFile.Parse(fileStream);
                ByteSpan byteSpan = sourcePriFile.GetDataItemByRef(Candidate.DataItem.Value);

                fileStream.Seek(byteSpan.Offset, SeekOrigin.Begin);

                using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.Default, true))
                    data = binaryReader.ReadBytes((int)byteSpan.Length);
            }
        }

        switch (Candidate.Type)
        {
            case ResourceValueType.AsciiPath:
            case ResourceValueType.AsciiString:
                return Encoding.ASCII.GetString(data).TrimEnd('\0');
            case ResourceValueType.Utf8Path:
            case ResourceValueType.Utf8String:
                return Encoding.UTF8.GetString(data).TrimEnd('\0');
            case ResourceValueType.Path:
            case ResourceValueType.String:
                return Encoding.Unicode.GetString(data).TrimEnd('\0');
            case ResourceValueType.EmbeddedData:
                return data;
            default:
                throw new Exception();
        }
    }

    private bool GoToLocationCommand_CanExecute()
    {
        return locationPath != null;
    }

    private void GoToLocationCommand_Execute()
    {
        string fullLocationPath = FullLocationPath;

        Process.Start("explorer.exe", string.Format("/select,\"{0}\"", fullLocationPath));
    }

    private bool SaveAsCommand_CanExecute()
    {
        return true;
    }

    private void SaveAsCommand_Execute()
    {
        object data = GetData();

        if (data == null)
            return;

        byte[] byteData = null;

        string externalFilePath = null;

        switch (Candidate.Type)
        {
            case ResourceValueType.String:
            case ResourceValueType.AsciiString:
            case ResourceValueType.Utf8String:
                byteData = Encoding.UTF8.GetBytes((string)data);
                break;
            case ResourceValueType.Path:
            case ResourceValueType.AsciiPath:
            case ResourceValueType.Utf8Path:
                string rootPath;

                if (Candidate.SourceFile == null)
                    rootPath = resourceRootPath;
                else
                    rootPath = Path.GetDirectoryName(priFile.GetReferencedFileByRef(Candidate.SourceFile.Value).FullName);

                externalFilePath = Path.Combine(rootPath, (string)data);
                break;
            case ResourceValueType.EmbeddedData:
                byteData = (byte[])data;
                break;
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();

        saveFileDialog.Filter = "All files (*.*)|*.*";

        if (externalFilePath != null)
            saveFileDialog.FileName = Path.GetFileName(externalFilePath);
        else if (qualifiers.Any())
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(resourceMapItem.Name) +
                "." + string.Join("_", qualifiers.Select(q => $"{q.Type.ToString().ToLower()}-{q.Value.ToLower()}")) +
                Path.GetExtension(resourceMapItem.Name);
        else
            saveFileDialog.FileName = resourceMapItem.Name;

        if (saveFileDialog.ShowDialog() != true)
            return;

        if (externalFilePath != null)
            File.Copy(externalFilePath, saveFileDialog.FileName, true);
        else
            File.WriteAllBytes(saveFileDialog.FileName, byteData);
    }
}
