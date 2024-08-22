using PriFormat;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PriInfo;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: PriInfo <path to PRI file>");
            return;
        }

        using (FileStream stream = File.OpenRead(args[0]))
        {
            PriFile priFile = PriFile.Parse(stream);

            Console.WriteLine("Sections:");

            foreach (Section section in priFile.Sections)
                Console.WriteLine("  {0}", section);

            Console.WriteLine();
            Console.WriteLine("Candidates:");

            foreach (var resourceMapSectionRef in priFile.PriDescriptorSection.ResourceMapSections)
            {
                ResourceMapSection resourceMapSection = priFile.GetSectionByRef(resourceMapSectionRef);

                if (resourceMapSection.HierarchicalSchemaReference != null)
                    continue;

                DecisionInfoSection decisionInfoSection = priFile.GetSectionByRef(resourceMapSection.DecisionInfoSection);

                foreach (var candidateSet in resourceMapSection.CandidateSets.Values)
                {
                    ResourceMapItem item = priFile.GetResourceMapItemByRef(candidateSet.ResourceMapItem);

                    Console.WriteLine("  {0}:", item.FullName);

                    foreach (var candidate in candidateSet.Candidates)
                    {
                        string value = null;

                        if (candidate.SourceFile != null)
                            value = string.Format("<external in {0}>", priFile.GetReferencedFileByRef(candidate.SourceFile.Value).FullName);
                        else
                        {
                            ByteSpan byteSpan;

                            if (candidate.DataItem != null)
                                byteSpan = priFile.GetDataItemByRef(candidate.DataItem.Value);
                            else
                                byteSpan = candidate.Data.Value;

                            stream.Seek(byteSpan.Offset, SeekOrigin.Begin);

                            byte[] data;

                            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.Default, true))
                                data = binaryReader.ReadBytes((int)byteSpan.Length);

                            switch (candidate.Type)
                            {
                                case ResourceValueType.AsciiPath:
                                case ResourceValueType.AsciiString:
                                    value = Encoding.ASCII.GetString(data).TrimEnd('\0');
                                    break;
                                case ResourceValueType.Utf8Path:
                                case ResourceValueType.Utf8String:
                                    value = Encoding.UTF8.GetString(data).TrimEnd('\0');
                                    break;
                                case ResourceValueType.Path:
                                case ResourceValueType.String:
                                    value = Encoding.Unicode.GetString(data).TrimEnd('\0');
                                    break;
                                case ResourceValueType.EmbeddedData:
                                    value = string.Format("<{0} bytes>", data.Length);
                                    break;
                            }
                        }

                        QualifierSet qualifierSet = decisionInfoSection.QualifierSets[candidate.QualifierSet];

                        string qualifiers = string.Join(", ", qualifierSet.Qualifiers.Select(q => string.Format("{0}={1}", q.Type, q.Value)));

                        Console.WriteLine("    Candidate {0}: {1}", qualifiers, value);
                    }
                }
            }
        }
    }
}
