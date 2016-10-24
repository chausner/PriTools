using PRIExplorer.ViewModels;
using System.Windows.Controls;

namespace PRIExplorer.Views
{
    /// <summary>
    /// Interaktionslogik für ImagePreviewPage.xaml
    /// </summary>
    public partial class ImagePreviewPage : Page
    {
        public ImagePreviewPage(ImagePreviewViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
