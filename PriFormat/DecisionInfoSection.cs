using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriFormat;

public class DecisionInfoSection : Section
{
    public IReadOnlyList<Decision> Decisions { get; private set; }
    public IReadOnlyList<QualifierSet> QualifierSets { get; private set; }
    public IReadOnlyList<Qualifier> Qualifiers { get; private set; }

    internal const string Identifier = "[mrm_decn_info]\0";

#pragma warning disable CS8618
    internal DecisionInfoSection(PriFile priFile) : base(Identifier, priFile)
#pragma warning restore CS8618
    {
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
    {
        ushort numDistinctQualifiers = binaryReader.ReadUInt16();
        ushort numQualifiers = binaryReader.ReadUInt16();
        ushort numQualifierSets = binaryReader.ReadUInt16();
        ushort numDecisions = binaryReader.ReadUInt16();
        ushort numIndexTableEntries = binaryReader.ReadUInt16();
        ushort totalDataLength = binaryReader.ReadUInt16();

        List<DecisionInfo> decisionInfos = new(numDecisions);
        for (int i = 0; i < numDecisions; i++)
        {
            ushort firstQualifierSetIndexIndex = binaryReader.ReadUInt16();
            ushort numQualifierSetsInDecision = binaryReader.ReadUInt16();
            decisionInfos.Add(new DecisionInfo(firstQualifierSetIndexIndex, numQualifierSetsInDecision));
        }

        List<QualifierSetInfo> qualifierSetInfos = new(numQualifierSets);
        for (int i = 0; i < numQualifierSets; i++)
        {
            ushort firstQualifierIndexIndex = binaryReader.ReadUInt16();
            ushort numQualifiersInSet = binaryReader.ReadUInt16();
            qualifierSetInfos.Add(new QualifierSetInfo(firstQualifierIndexIndex, numQualifiersInSet));
        }

        List<QualifierInfo> qualifierInfos = new(numQualifiers);
        for (int i = 0; i < numQualifiers; i++)
        {
            ushort index = binaryReader.ReadUInt16();
            ushort priority = binaryReader.ReadUInt16();
            ushort fallbackScore = binaryReader.ReadUInt16();
            binaryReader.ExpectUInt16(0);
            qualifierInfos.Add(new QualifierInfo(index, priority, fallbackScore));
        }

        List<DistinctQualifierInfo> distinctQualifierInfos = new(numDistinctQualifiers);
        for (int i = 0; i < numDistinctQualifiers; i++)
        {
            binaryReader.ReadUInt16();
            QualifierType qualifierType = (QualifierType)binaryReader.ReadUInt16();
            binaryReader.ReadUInt16();
            binaryReader.ReadUInt16();
            uint operandValueOffset = binaryReader.ReadUInt32();
            distinctQualifierInfos.Add(new DistinctQualifierInfo(qualifierType, operandValueOffset));
        }

        ushort[] indexTable = new ushort[numIndexTableEntries];

        for (int i = 0; i < numIndexTableEntries; i++)
            indexTable[i] = binaryReader.ReadUInt16();

        long dataStartOffset = binaryReader.BaseStream.Position;

        List<Qualifier> qualifiers = new(numQualifiers);

        for (int i = 0; i < numQualifiers; i++)
        {
            DistinctQualifierInfo distinctQualifierInfo = distinctQualifierInfos[qualifierInfos[i].Index];

            binaryReader.BaseStream.Seek(dataStartOffset + distinctQualifierInfo.OperandValueOffset * 2, SeekOrigin.Begin);                

            string value = binaryReader.ReadNullTerminatedString(Encoding.Unicode);

            qualifiers.Add(new Qualifier(
                (ushort)i,
                distinctQualifierInfo.QualifierType,
                qualifierInfos[i].Priority,
                qualifierInfos[i].FallbackScore / 1000f,
                value));
        }

        Qualifiers = qualifiers;

        List<QualifierSet> qualifierSets = new(numQualifierSets);

        for (int i = 0; i < numQualifierSets; i++)
        {
            List<Qualifier> qualifiersInSet = new(qualifierSetInfos[i].NumQualifiersInSet);

            for (int j = 0; j < qualifierSetInfos[i].NumQualifiersInSet; j++)
                qualifiersInSet.Add(qualifiers[indexTable[qualifierSetInfos[i].FirstQualifierIndexIndex + j]]);

            qualifierSets.Add(new QualifierSet((ushort)i, qualifiersInSet));
        }

        QualifierSets = qualifierSets;

        List<Decision> decisions = new(numDecisions);

        for (int i = 0; i < numDecisions; i++)
        {
            List<QualifierSet> qualifierSetsInDecision = new(decisionInfos[i].NumQualifierSetsInDecision);

            for (int j = 0; j < decisionInfos[i].NumQualifierSetsInDecision; j++)
                qualifierSetsInDecision.Add(qualifierSets[indexTable[decisionInfos[i].FirstQualifierSetIndexIndex + j]]);

            decisions.Add(new Decision((ushort)i, qualifierSetsInDecision));
        }

        Decisions = decisions;

        return true;
    }

    private record struct DecisionInfo(ushort FirstQualifierSetIndexIndex, ushort NumQualifierSetsInDecision);

    private record struct QualifierSetInfo(ushort FirstQualifierIndexIndex, ushort NumQualifiersInSet);

    private record struct QualifierInfo(ushort Index, ushort Priority, ushort FallbackScore);

    private record struct DistinctQualifierInfo(QualifierType QualifierType, uint OperandValueOffset);
}

public enum QualifierType
{
    Language,
    Contrast,
    Scale,
    HomeRegion,
    TargetSize,
    LayoutDirection,
    Theme,
    AlternateForm,
    DXFeatureLevel,
    Configuration,
    DeviceFamily,
    Custom
}

public record struct Qualifier(ushort Index, QualifierType Type, ushort Priority, float FallbackScore, string Value);

public record struct QualifierSet(ushort Index, IReadOnlyList<Qualifier> Qualifiers);

public record struct Decision(ushort Index, IReadOnlyList<QualifierSet> QualifierSets);
