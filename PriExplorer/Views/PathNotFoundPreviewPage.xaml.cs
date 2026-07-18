using PriExplorer.ViewModels;
using System.Windows.Controls;

namespace PriExplorer.Views;

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
