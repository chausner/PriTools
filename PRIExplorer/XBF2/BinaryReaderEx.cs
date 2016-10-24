using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public class BinaryReaderEx : BinaryReader
    {
        public BinaryReaderEx(Stream input) : base(input) { }
        public BinaryReaderEx(Stream input, Encoding encoding) : base(input, encoding) { }
        public BinaryReaderEx(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }

        public new int Read7BitEncodedInt()
        {
            return base.Read7BitEncodedInt();
        }
    }
}
