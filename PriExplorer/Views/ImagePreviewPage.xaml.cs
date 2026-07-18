using PriExplorer.ViewModels;
using System.Windows.Controls;

namespace PriExplorer.Views;

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
