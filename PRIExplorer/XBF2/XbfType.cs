using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public class XbfType
    {
        public XbfType(XbfReader xbf, BinaryReader reader)
        {
            Flags = (XbfTypeFlags)reader.ReadInt32();
            int namespaceID = reader.ReadInt32();
            Namespace = xbf.TypeNamespaceTable[namespaceID];
            int nameID = reader.ReadInt32();
            Name = xbf.StringTable[nameID];
        }

        public XbfTypeFlags Flags { get; private set; }
        public XbfTypeNamespace Namespace { get; private set; }
        public string Name { get; private set; }
    }

    public enum XbfTypeFlags
    {
        // TODO: These values are from XBF v1 -- are they still correct?
        None = 0,
        IsMarkupDirective = 1,
    }
}
