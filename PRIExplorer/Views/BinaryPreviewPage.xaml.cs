using PRIExplorer.ViewModels;
using System.Windows.Controls;

namespace PRIExplorer.Views;

/// <summary>
/// Interaktionslogik für BinaryPreviewPage.xaml
/// </summary>
public partial class BinaryPreviewPage : Page
{
    public BinaryPreviewPage(BinaryPreviewViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}
