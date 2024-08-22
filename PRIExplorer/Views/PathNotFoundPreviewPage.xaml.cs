using PRIExplorer.ViewModels;
using System.Windows.Controls;

namespace PRIExplorer.Views;

/// <summary>
/// Interaktionslogik für PathNotFoundPreviewPage.xaml
/// </summary>
public partial class PathNotFoundPreviewPage : Page
{
    public PathNotFoundPreviewPage(PathNotFoundPreviewViewModel viewModel, MainViewModel mainViewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

        setResourceRootPathButton.DataContext = mainViewModel;
    }
}
