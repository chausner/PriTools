using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PRIExplorer.Controls;

/// <summary>
/// Interaktionslogik für CheckerboardImage.xaml
/// </summary>
[ContentProperty("AdditionalContent")]
public partial class CheckerboardImage : UserControl
{
    public object AdditionalContent
    {
        get => GetValue(AdditionalContentProperty);
        set => SetValue(AdditionalContentProperty, value);
    }

    public static readonly DependencyProperty AdditionalContentProperty =
        DependencyProperty.Register("AdditionalContent", typeof(object), typeof(CheckerboardImage), new PropertyMetadata(null));

    public CheckerboardImage()
    {
        InitializeComponent();
    }
}
