using System.Text;

namespace PRIExplorer.ViewModels;

public class TextPreviewViewModel
{
    public string Text { get; }

    public TextPreviewViewModel(byte[] data)
    {
        Encoding encoding;

        if (data.Length >= 3 && data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
            encoding = Encoding.UTF8;
        else if (data.Length >= 2 && data[0] == 0xEF && data[1] == 0xFF ||
            data.Length >= 2 && data[0] == 0xFF && data[1] == 0xEF)
            encoding = Encoding.Unicode;
        else
            encoding = Encoding.ASCII;

         Text = encoding.GetString(data);
    }
}
