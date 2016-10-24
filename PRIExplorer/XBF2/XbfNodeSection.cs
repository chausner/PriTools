using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public class XbfNodeSection
    {
        public XbfNodeSection(XbfReader xbf, BinaryReader reader)
        {
            NodeOffset = reader.ReadInt32();
            PositionalOffset = reader.ReadInt32();
        }

        public int NodeOffset { get; private set; }
        public int PositionalOffset { get; private set; }
    }
}
