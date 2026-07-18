using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PriFormat;

public class PriDescriptorSection : Section
{
    public PriDescriptorFlags PriFlags { get; private set; }

    public IReadOnlyList<SectionRef<HierarchicalSchemaSection>> HierarchicalSchemaSections { get; private set; }
    public IReadOnlyList<SectionRef<DecisionInfoSection>> DecisionInfoSections { get; private set; }
    public IReadOnlyList<SectionRef<ResourceMapSection>> ResourceMapSections { get; private set; }
    public IReadOnlyList<SectionRef<ReferencedFileSection>> ReferencedFileSections { get; private set; }
    public IReadOnlyList<SectionRef<DataItemSection>> DataItemSections { get; private set; }

    public SectionRef<ResourceMapSection>? PrimaryResourceMapSection { get; private set; }

    internal const string Identifier = "[mrm_pridescex]\0";

#pragma warning disable CS8618
    internal PriDescriptorSection(PriFile priFile) : base(Identifier, priFile)
#pragma warning restore CS8618
    {
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader, long sectionContentPosition)
    {
        PriFlags = (PriDescriptorFlags)binaryReader.ReadUInt16();
        ushort includedFileListSection = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);
        ushort numHierarchicalSchemaSections = binaryReader.ReadUInt16();
        ushort numDecisionInfoSections = binaryReader.ReadUInt16();
        ushort numResourceMapSections = binaryReader.ReadUInt16();
        ushort primaryResourceMapSection = binaryReader.ReadUInt16();
        if (primaryResourceMapSection != 0xFFFF)
            PrimaryResourceMapSection = new SectionRef<ResourceMapSection>(primaryResourceMapSection);
        else
            PrimaryResourceMapSection = null;
        ushort numReferencedFileSections = binaryReader.ReadUInt16();
        ushort numDataItemSections = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);

        HierarchicalSchemaSections = Enumerable.Range(0, numHierarchicalSchemaSections)
            .Select(_ => new SectionRef<HierarchicalSchemaSection>(binaryReader.ReadUInt16()))
            .ToArray();

        DecisionInfoSections = Enumerable.Range(0, numDecisionInfoSections)
            .Select(_ => new SectionRef<DecisionInfoSection>(binaryReader.ReadUInt16()))
            .ToArray();

        ResourceMapSections = Enumerable.Range(0, numResourceMapSections)
            .Select(_ => new SectionRef<ResourceMapSection>(binaryReader.ReadUInt16()))
            .ToArray();

        ReferencedFileSections = Enumerable.Range(0, numReferencedFileSections)
            .Select(_ => new SectionRef<ReferencedFileSection>(binaryReader.ReadUInt16()))
            .ToArray();

        DataItemSections = Enumerable.Range(0, numDataItemSections)
            .Select(_ => new SectionRef<DataItemSection>(binaryReader.ReadUInt16()))
            .ToArray();

        return true;
    }
}

[Flags]
public enum PriDescriptorFlags : ushort
{
    AutoMerge = 1,
    IsDeploymentMergeable = 2,
    IsDeploymentMergeResult = 4,
    IsAutomergeMergeResult = 8
}
