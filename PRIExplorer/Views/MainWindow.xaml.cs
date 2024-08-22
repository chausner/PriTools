using PRIExplorer.ViewModels;
using System.Windows;

namespace PRIExplorer;

/// <summary>
/// Interaktionslogik für MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    MainViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();

        viewModel = new MainViewModel();

        DataContext = viewModel;
    }

    private void resourceMapTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        viewModel.SelectedEntry = (EntryViewModel)e.NewValue;
    }

    private void Window_PreviewDragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(DataFormats.FileDrop) is not string[] paths || paths.Length != 1)
            return;

        e.Effects = DragDropEffects.Copy;
        e.Handled = true;
    }

    private void Window_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(DataFormats.FileDrop) is not string[] paths || paths.Length != 1)
            return;

        viewModel.OpenPriFile(paths[0]);

        e.Handled = true;
    }
}
