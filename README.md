# PriTools

Tools for parsing and exploring PRI (Package Resource Index) files

## PRIExplorer

This sample application allows to open and browse the contents of PRI files.
It is also possible to export individual resources.

The application leverages the [XbfAnalyzer](https://github.com/chausner/XbfAnalyzer)
(originally by [misenhower](https://github.com/misenhower/XbfAnalyzer))
to provide on-the-fly decompilation of XBF2 resources back to XAML.

## PriFormat

This project implements a PRI file reader in C# for low-level access to pretty much all structures in the PRI file format.
Only file reading, not writing, is supported at the moment.

### Sample usage

You can use the following code to open a PRI file and print the names of all resource map items:

```csharp
using (FileStream stream = File.OpenRead(path))
{
    PriFile priFile = PriFile.Parse(stream);

    ResourceMapSection resourceMapSection = priFile.GetSectionByRef(priFile.PriDescriptorSection.PrimaryResourceMapSection.Value);

    foreach (CandidateSet candidateSet in resourceMapSection.CandidateSets.Values)
    {
        ResourceMapItem resourceMapItem = priFile.GetResourceMapItemByRef(candidateSet.ResourceMapItem);

        Console.WriteLine(resourceMapItem.FullName);
    }
}
```

For more sample code, see the [PriInfo project](PriInfo/Program.cs).

## PRI File Format Specification

[Package Resource Index File Format.md](Package%20Resource%20Index%20File%20Format.md) contains a reasonably complete (but unofficial) description of data structures in the PRI file format.

## License

Apache 2.0, see LICENSE.

XBF2 decompilation code is originally based on the implementation by [misenhower](https://github.com/misenhower/XbfAnalyzer).
