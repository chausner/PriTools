using System.IO;
using System.Windows.Media.Imaging;

namespace PRIExplorer.ViewModels;

public class ImagePreviewViewModel
{
    public BitmapImage Image { get; }
    public string ImageSize { get; }

    public ImagePreviewViewModel(byte[] data)
    {
        BitmapImage bitmapImage = new BitmapImage();

        bitmapImage.BeginInit();
        bitmapImage.StreamSource = new MemoryStream(data, false);
        bitmapImage.EndInit();

        Image = bitmapImage;
        ImageSize = $"Size: {bitmapImage.PixelWidth} x {bitmapImage.PixelHeight}";
    }
}
