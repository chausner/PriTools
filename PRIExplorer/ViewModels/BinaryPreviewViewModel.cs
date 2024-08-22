using System;

namespace PRIExplorer.ViewModels;

public class BinaryPreviewViewModel
{
    public string Length { get; }

    public BinaryPreviewViewModel(byte[] data)
    {
        Length = Convert.ToString(data.Length);
    }
}
