using System;
using System.IO;
using XbfAnalyzer.Xbf;

namespace PRIExplorer.ViewModels;

public class XbfPreviewViewModel
{
    public string Xaml { get; private set; }

    public XbfPreviewViewModel(byte[] data)
    {
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            XbfReader xbfReader = new XbfReader(memoryStream);

            if (xbfReader.Header.MajorFileVersion != 2)
                throw new Exception("Only XBF2 files can be decompiled.");

            Xaml = xbfReader.RootObject.ToString();
        }
    }
}
