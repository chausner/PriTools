using PRIExplorer.ViewModels;
using System.Windows.Controls;

namespace PRIExplorer.Views;

/// <summary>
/// Interaktionslogik für TextPreviewPage.xaml
/// </summary>
public partial class TextPreviewPage : Page
{
    public TextPreviewPage(TextPreviewViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}
