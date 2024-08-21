# PriTools
Tools for parsing and exploring PRI (Package Resource Index) files

Usage
-----
Enumerate all resource map items:
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

License
-------
Apache 2.0, see LICENSE.

XBF2 decompilation code is originally based on the implementation by [misenhower](https://github.com/misenhower/XbfAnalyzer).
