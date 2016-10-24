using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public class XbfObjectCollection : List<XbfObject>
    {
        public override string ToString()
        {
            return ToString(0);
        }

        public string ToString(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var obj in this)
                sb.AppendLine(obj.ToString(indentLevel));
            return sb.ToString();
        }
    }
}
