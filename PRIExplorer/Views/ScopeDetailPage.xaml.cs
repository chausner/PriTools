using PriFormat;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PRIExplorer.Views;

/// <summary>
/// Interaktionslogik für ScopeDetailPage.xaml
/// </summary>
public partial class ScopeDetailPage : Page
{
    PriFile priFile;
    Stream priStream;

    public ScopeDetailPage(PriFile priFile, Stream priStream, ResourceMapScope scope)
    {
        InitializeComponent();

        this.priFile = priFile;
        this.priStream = priStream;

        bool dotAsPathSeparator = scope.FullName.Contains(@"\Resources");

        List<StringResource> stringResources = new List<StringResource>();

        EnumerateStringResources(scope, string.Empty, dotAsPathSeparator, stringResources);

        keyValueListView.ItemsSource = stringResources;
    }

    private void EnumerateStringResources(ResourceMapScope scope, string pathPrefix, bool dotAsPathSeparator, List<StringResource> stringResources)
    {
        foreach (ResourceMapItem childItem in scope.Children.OfType<ResourceMapItem>())
        {
            string path;
            
            if (dotAsPathSeparator)
                path = pathPrefix == string.Empty ? childItem.Name : pathPrefix + "." + childItem.Name;
            else
                path = childItem.FullName;

            AddStringResources(childItem, path, stringResources);
        }

        foreach (ResourceMapScope childScope in scope.Children.OfType<ResourceMapScope>())
        {
            string path = pathPrefix == string.Empty ? childScope.Name : pathPrefix + "." + childScope.Name;

            EnumerateStringResources(childScope, path, dotAsPathSeparator, stringResources);
        }
    }

    private void AddStringResources(ResourceMapItem item, string path, List<StringResource> stringResources)
    {
        ResourceMapSection primaryResourceMapSection =
            priFile.GetSectionByRef(priFile.PriDescriptorSection.PrimaryResourceMapSection.Value);

        DecisionInfoSection decisionInfoSection =
            priFile.GetSectionByRef(priFile.PriDescriptorSection.DecisionInfoSections.First());

        CandidateSet candidateSet;

        if (!primaryResourceMapSection.CandidateSets.TryGetValue(item.Index, out candidateSet))
            return;

        foreach (Candidate candidate in candidateSet.Candidates)
        {
            if (candidate != null)
            {
                string value;

                if (candidate.SourceFile != null)
                    value = "external at " + priFile.GetReferencedFileByRef(candidate.SourceFile.Value).FullName;
                else
                {
                    ByteSpan byteSpan;

                    if (candidate.DataItem != null)
                        byteSpan = priFile.GetDataItemByRef(candidate.DataItem.Value);
                    else
                        byteSpan = candidate.Data.Value;

                    byte[] data;

                    priStream.Seek(byteSpan.Offset, SeekOrigin.Begin);

                    using (BinaryReader binaryReader = new BinaryReader(priStream, Encoding.Default, true))
                        data = binaryReader.ReadBytes((int)byteSpan.Length);

                    value = GetCandidateDataAsString(candidate, data);
                }

                IReadOnlyList<Qualifier> qualifiers = decisionInfoSection.QualifierSets[candidate.QualifierSet].Qualifiers;

                string qualifiersDescription = string.Join(", ", qualifiers.Select(q => q.Type + "=" + q.Value));

                StringResource stringResource = new StringResource(path, value, qualifiersDescription);

                stringResources.Add(stringResource);
            }
        }
    }

    private string GetCandidateDataAsString(Candidate candidate, byte[] data)
    {
        string stringData = null;

        switch (candidate.Type)
        {
            case ResourceValueType.AsciiString:
                stringData = Encoding.ASCII.GetString(data);
                break;
            case ResourceValueType.Utf8String:
                stringData = Encoding.UTF8.GetString(data);
                break;
            case ResourceValueType.String:
                stringData = Encoding.Unicode.GetString(data);
                break;
            case ResourceValueType.AsciiPath:
            case ResourceValueType.Utf8Path:
            case ResourceValueType.Path:
                return "(external)";
            case ResourceValueType.EmbeddedData:
                return "(embedded)";
        }

        stringData = stringData.TrimEnd('\0');

        return stringData;
    }

    class StringResource
    {
        public string Key { get; }
        public string Value { get; }
        public string Qualifiers { get; }

        public StringResource(string key, string value, string qualifiers)
        {
            Key = key;
            Value = value;
            Qualifiers = qualifiers;
        }
    }
}
