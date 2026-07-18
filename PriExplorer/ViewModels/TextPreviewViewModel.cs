using System.Text;

namespace PriExplorer.ViewModels;

public class TextPreviewViewModel
{
    public string Text { get; }

    public TextPreviewViewModel(byte[] data)
    {
        Encoding encoding = data switch
        {
            [0xEF, 0xBB, 0xBF, ..] => Encoding.UTF8,
            [0xEF, 0xFF, ..] or [0xFF, 0xEF, ..] => Encoding.Unicode,
            _ => Encoding.ASCII
        };

        Text = encoding.GetString(data);
    }
}
