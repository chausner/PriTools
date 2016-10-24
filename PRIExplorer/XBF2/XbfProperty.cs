using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public class XbfProperty
    {
        public XbfProperty(XbfReader xbf, BinaryReader reader)
        {
            Flags = (XbfPropertyFlags)reader.ReadInt32();
            int typeID = reader.ReadInt32();
            Type = xbf.TypeTable[typeID];
            int nameID = reader.ReadInt32();
            Name = xbf.StringTable[nameID];
        }

        public XbfPropertyFlags Flags { get; private set; }
        public XbfType Type { get; private set; }
        public string Name { get; private set; }
    }

    [Flags]
    public enum XbfPropertyFlags
    {
        // TODO: These values are from XBF v1 -- are they still correct?
        None = 0,
        IsXmlProperty = 1,
        IsMarkupDirective = 2,
        IsImplicitProperty = 4,
    }
}
