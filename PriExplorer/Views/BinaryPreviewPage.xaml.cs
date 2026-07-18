using PriExplorer.ViewModels;
using System.Windows.Controls;

namespace PriExplorer.Views;

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
