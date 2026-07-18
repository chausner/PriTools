using PriExplorer.ViewModels;
using System.Windows.Controls;

namespace PriExplorer.Views;

/// <summary>
/// Interaktionslogik für XbfPreviewPage.xaml
/// </summary>
public partial class XbfPreviewPage : Page
{
    public XbfPreviewPage(XbfPreviewViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}
