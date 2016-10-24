using PRIExplorer.ViewModels;
using System.Windows.Controls;

namespace PRIExplorer.Views
{
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
}
