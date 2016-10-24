using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbfAnalyzer.Xbf
{
    public static class XbfFrameworkTypes
    {
        public static string GetNameForTypeID(int id)
        {
            if (id - 1 >= 0 && id - 1 < typeNames.Length)
                return typeNames[id - 1];
            else
                return null;
        }

        public static string GetNameForPropertyID(int id)
        {
            if (id - 1 >= 0 && id - 1 < propertyNames.Length)
                return propertyNames[id - 1];
            else
                return null;
        }

        public static string GetNameForEnumValue(int id, int value)
        {
            Dictionary<int, string> ev;

            if (id - 0x023C >= 0 && id - 0x023C < enumValues.Length)
                ev = enumValues[id - 0x023C];
            else
                return null;

            if (ev == null)
                return null;

            string name;

            if (ev.TryGetValue(value, out name))
                return name;
            else
            {
                if (value == 0)
                    return null;
                
                // represent enum value as a combination of flags
                List<string> names = new List<string>();

                int remainingValue = value;

                foreach (int v in ev.Keys)
                    if (v != 0 && (remainingValue & v) == v)
                    {
                        names.Add(ev[v]);
                        remainingValue &= ~v;
                    }

                if (remainingValue != 0)
                    return null;

                return string.Join(",", names);
            }
        }

        // mappings are hard-coded in GenXbf.dll from Windows SDK
        // up-to-date as of Anniversary Update (build 14393)

        private static readonly string[] typeNames = {
            /* 0x0001 */ "Byte", // Windows.Foundation.Byte
            /* 0x0002 */ "Char16", // Windows.Foundation.Char16
            /* 0x0003 */ "DateTime", // Windows.Foundation.DateTime
            /* 0x0004 */ "GeneratorPosition", // Windows.UI.Xaml.Controls.Primitives.GeneratorPosition
            /* 0x0005 */ "Guid", // Windows.Foundation.Guid
            /* 0x0006 */ "Int16", // Windows.Foundation.Int16
            /* 0x0007 */ "Int64", // Windows.Foundation.Int64
            /* 0x0008 */ "Object", // Windows.Foundation.Object
            /* 0x0009 */ "Single", // Windows.Foundation.Single
            /* 0x000A */ "TypeName", // Windows.UI.Xaml.Interop.TypeName
            /* 0x000B */ "UInt16", // Windows.Foundation.UInt16
            /* 0x000C */ "UInt32", // Windows.Foundation.UInt32
            /* 0x000D */ "UInt64", // Windows.Foundation.UInt64
            /* 0x000E */ "AutomationProperties", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000F */ "DataPackage", // Windows.ApplicationModel.DataTransfer.DataPackage
            /* 0x0010 */ "DataTemplateSelector", // Windows.UI.Xaml.Controls.DataTemplateSelector
            /* 0x0011 */ "DependencyObject", // Windows.UI.Xaml.DependencyObject
            /* 0x0012 */ "EventHandler", // Windows.Foundation.EventHandler
            /* 0x0013 */ "GroupStyleCollection", // Windows.UI.Xaml.Controls.GroupStyleCollection
            /* 0x0014 */ "GroupStyleSelector", // Windows.UI.Xaml.Controls.GroupStyleSelector
            /* 0x0015 */ "ItemContainerGenerator", // Windows.UI.Xaml.Controls.ItemContainerGenerator
            /* 0x0016 */ "ListViewPersistenceHelper", // Windows.UI.Xaml.Controls.ListViewPersistenceHelper
            /* 0x0017 */ "StyleSelector", // Windows.UI.Xaml.Controls.StyleSelector
            /* 0x0018 */ "TextOptions", // Windows.UI.Xaml.TextOptions
            /* 0x0019 */ "ToolTipService", // Windows.UI.Xaml.Controls.ToolTipService
            /* 0x001A */ "Typography", // Windows.UI.Xaml.Documents.Typography
            /* 0x001B */ "Uri", // Windows.Foundation.Uri
            /* 0x001C */ "MediaCapture", // Windows.Media.Capture.MediaCapture
            /* 0x001D */ "PlayToSource", // Windows.Media.PlayTo.PlayToSource
            /* 0x001E */ "MediaProtectionManager", // Windows.Media.Protection.MediaProtectionManager
            /* 0x001F */ "Application", // Windows.UI.Xaml.Application
            /* 0x0020 */ "ApplicationBarService", // Windows.UI.Xaml.Controls.ApplicationBarService
            /* 0x0021 */ "AutomationPeer", // Windows.UI.Xaml.Automation.Peers.AutomationPeer
            /* 0x0022 */ "AutoSuggestBoxSuggestionChosenEventArgs", // Windows.UI.Xaml.Controls.AutoSuggestBoxSuggestionChosenEventArgs
            /* 0x0023 */ "AutoSuggestBoxTextChangedEventArgs", // Windows.UI.Xaml.Controls.AutoSuggestBoxTextChangedEventArgs
            /* 0x0024 */ "Boolean", // Windows.Foundation.Boolean
            /* 0x0025 */ "Brush", // Windows.UI.Xaml.Media.Brush
            /* 0x0026 */ "CacheMode", // Windows.UI.Xaml.Media.CacheMode
            /* 0x0027 */ "CollectionView", // Windows.UI.Xaml.Data.CollectionView
            /* 0x0028 */ "CollectionViewGroup", // Windows.UI.Xaml.Data.CollectionViewGroup
            /* 0x0029 */ "CollectionViewSource", // Windows.UI.Xaml.Data.CollectionViewSource
            /* 0x002A */ "Color", // Windows.UI.Color
            /* 0x002B */ "ColorKeyFrame", // Windows.UI.Xaml.Media.Animation.ColorKeyFrame
            /* 0x002C */ "ColumnDefinition", // Windows.UI.Xaml.Controls.ColumnDefinition
            /* 0x002D */ "ComboBoxTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings
            /* 0x002E */ "CornerRadius", // Windows.UI.Xaml.CornerRadius
            /* 0x002F */ null,
            /* 0x0030 */ "DebugSettings", // Windows.UI.Xaml.DebugSettings
            /* 0x0031 */ "DependencyObjectWrapper", // Windows.UI.Xaml.Internal.DependencyObjectWrapper
            /* 0x0032 */ "DependencyProperty", // Windows.UI.Xaml.DependencyProperty
            /* 0x0033 */ "Deployment", // Windows.UI.Xaml.Deployment
            /* 0x0034 */ "Double", // Windows.Foundation.Double
            /* 0x0035 */ "DoubleKeyFrame", // Windows.UI.Xaml.Media.Animation.DoubleKeyFrame
            /* 0x0036 */ null,
            /* 0x0037 */ "EasingFunctionBase", // Windows.UI.Xaml.Media.Animation.EasingFunctionBase
            /* 0x0038 */ "Enumerated", // Windows.Foundation.Enumerated
            /* 0x0039 */ "ExternalObjectReference", // Windows.UI.Xaml.Internal.ExternalObjectReference
            /* 0x003A */ "FlyoutBase", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x003B */ "FontFamily", // Windows.UI.Xaml.Media.FontFamily
            /* 0x003C */ "FontWeight", // Windows.UI.Text.FontWeight
            /* 0x003D */ "FrameworkTemplate", // Windows.UI.Xaml.FrameworkTemplate
            /* 0x003E */ "GeneralTransform", // Windows.UI.Xaml.Media.GeneralTransform
            /* 0x003F */ "Geometry", // Windows.UI.Xaml.Media.Geometry
            /* 0x0040 */ "GradientStop", // Windows.UI.Xaml.Media.GradientStop
            /* 0x0041 */ "GridLength", // Windows.UI.Xaml.GridLength
            /* 0x0042 */ "GroupStyle", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x0043 */ "HWCompNode", // Windows.UI.Xaml.Internal.HWCompNode
            /* 0x0044 */ "ImageSource", // Windows.UI.Xaml.Media.ImageSource
            /* 0x0045 */ "IMECandidateItem", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0046 */ "IMECandidatePage", // Windows.UI.Xaml.Controls.IMECandidatePage
            /* 0x0047 */ "InertiaExpansionBehavior", // Windows.UI.Xaml.Input.InertiaExpansionBehavior
            /* 0x0048 */ "InertiaRotationBehavior", // Windows.UI.Xaml.Input.InertiaRotationBehavior
            /* 0x0049 */ "InertiaTranslationBehavior", // Windows.UI.Xaml.Input.InertiaTranslationBehavior
            /* 0x004A */ "InputScope", // Windows.UI.Xaml.Input.InputScope
            /* 0x004B */ "InputScopeName", // Windows.UI.Xaml.Input.InputScopeName
            /* 0x004C */ "Int32", // Windows.Foundation.Int32
            /* 0x004D */ "IRawElementProviderSimple", // Windows.UI.Xaml.Automation.Provider.IRawElementProviderSimple
            /* 0x004E */ "KeySpline", // Windows.UI.Xaml.Media.Animation.KeySpline
            /* 0x004F */ "LayoutTransitionStaggerItem", // Windows.UI.Xaml.LayoutTransitionStaggerItem
            /* 0x0050 */ "LengthConverter", // Windows.UI.Xaml.LengthConverter
            /* 0x0051 */ "ListViewBaseItemTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ListViewBaseItemTemplateSettings
            /* 0x0052 */ "ManipulationDelta", // Windows.UI.Xaml.Input.ManipulationDelta
            /* 0x0053 */ "ManipulationPivot", // Windows.UI.Xaml.Input.ManipulationPivot
            /* 0x0054 */ "ManipulationVelocities", // Windows.UI.Xaml.Input.ManipulationVelocities
            /* 0x0055 */ "MarkupExtensionBase", // Windows.UI.Xaml.MarkupExtensionBase
            /* 0x0056 */ "Matrix", // Windows.UI.Xaml.Media.Matrix
            /* 0x0057 */ "Matrix3D", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x0058 */ "NavigationTransitionInfo", // Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo
            /* 0x0059 */ "ObjectKeyFrame", // Windows.UI.Xaml.Media.Animation.ObjectKeyFrame
            /* 0x005A */ "PageStackEntry", // Windows.UI.Xaml.Navigation.PageStackEntry
            /* 0x005B */ "ParametricCurve", // Windows.UI.Xaml.Internal.ParametricCurve
            /* 0x005C */ "ParametricCurveSegment", // Windows.UI.Xaml.Internal.ParametricCurveSegment
            /* 0x005D */ "PathFigure", // Windows.UI.Xaml.Media.PathFigure
            /* 0x005E */ "PathSegment", // Windows.UI.Xaml.Media.PathSegment
            /* 0x005F */ "Point", // Windows.Foundation.Point
            /* 0x0060 */ "Pointer", // Windows.UI.Xaml.Input.Pointer
            /* 0x0061 */ "PointerKeyFrame", // Windows.UI.Xaml.Internal.PointerKeyFrame
            /* 0x0062 */ "PointKeyFrame", // Windows.UI.Xaml.Media.Animation.PointKeyFrame
            /* 0x0063 */ "PresentationFrameworkCollection", // Windows.UI.Xaml.Collections.PresentationFrameworkCollection
            /* 0x0064 */ "PrintDocument", // Windows.UI.Xaml.Printing.PrintDocument
            /* 0x0065 */ "ProgressBarTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x0066 */ "ProgressRingTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ProgressRingTemplateSettings
            /* 0x0067 */ "Projection", // Windows.UI.Xaml.Media.Projection
            /* 0x0068 */ "PropertyPath", // Windows.UI.Xaml.PropertyPath
            /* 0x0069 */ "Rect", // Windows.Foundation.Rect
            /* 0x006A */ "RowDefinition", // Windows.UI.Xaml.Controls.RowDefinition
            /* 0x006B */ "SecondaryContentRelationship", // Windows.UI.Xaml.Internal.SecondaryContentRelationship
            /* 0x006C */ "SetterBase", // Windows.UI.Xaml.SetterBase
            /* 0x006D */ "SettingsFlyoutTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x006E */ "Size", // Windows.Foundation.Size
            /* 0x006F */ "SolidColorBrushClone", // Windows.UI.Xaml.Internal.SolidColorBrushClone
            /* 0x0070 */ "StaggerFunctionBase", // Windows.UI.Xaml.StaggerFunctionBase
            /* 0x0071 */ "String", // Windows.Foundation.String
            /* 0x0072 */ "Style", // Windows.UI.Xaml.Style
            /* 0x0073 */ "TemplateContent", // Windows.UI.Xaml.Internal.TemplateContent
            /* 0x0074 */ "TextAdapter", // Windows.UI.Xaml.Automation.Peers.TextAdapter
            /* 0x0075 */ null,
            /* 0x0076 */ "TextDecorationCollection", // Windows.UI.Xaml.TextDecorationCollection
            /* 0x0077 */ "TextElement", // Windows.UI.Xaml.Documents.TextElement
            /* 0x0078 */ "TextPointerWrapper", // Windows.UI.Xaml.Internal.TextPointerWrapper
            /* 0x0079 */ "TextProvider", // MS.Internal.Automation.TextProvider
            /* 0x007A */ "TextRangeAdapter", // Windows.UI.Xaml.Automation.Peers.TextRangeAdapter
            /* 0x007B */ "TextRangeProvider", // MS.Internal.Automation.TextRangeProvider
            /* 0x007C */ "Thickness", // Windows.UI.Xaml.Thickness
            /* 0x007D */ "Timeline", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x007E */ "TimelineMarker", // Windows.UI.Xaml.Media.TimelineMarker
            /* 0x007F */ "TimeSpan", // Windows.Foundation.TimeSpan
            /* 0x0080 */ "ToggleSwitchTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0081 */ "ToolTipTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ToolTipTemplateSettings
            /* 0x0082 */ "Transition", // Windows.UI.Xaml.Media.Animation.Transition
            /* 0x0083 */ "TransitionTarget", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x0084 */ "TriggerAction", // Windows.UI.Xaml.TriggerAction
            /* 0x0085 */ "TriggerBase", // Windows.UI.Xaml.TriggerBase
            /* 0x0086 */ null,
            /* 0x0087 */ "UIElement", // Windows.UI.Xaml.UIElement
            /* 0x0088 */ "UIElementClone", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x0089 */ "VisualState", // Windows.UI.Xaml.VisualState
            /* 0x008A */ "VisualStateGroup", // Windows.UI.Xaml.VisualStateGroup
            /* 0x008B */ "VisualStateManager", // Windows.UI.Xaml.VisualStateManager
            /* 0x008C */ "VisualTransition", // Windows.UI.Xaml.VisualTransition
            /* 0x008D */ "Window", // Windows.UI.Xaml.Window
            /* 0x008E */ null,
            /* 0x008F */ null,
            /* 0x0090 */ null,
            /* 0x0091 */ null,
            /* 0x0092 */ null,
            /* 0x0093 */ null,
            /* 0x0094 */ null,
            /* 0x0095 */ null,
            /* 0x0096 */ null,
            /* 0x0097 */ null,
            /* 0x0098 */ null,
            /* 0x0099 */ null,
            /* 0x009A */ null,
            /* 0x009B */ null,
            /* 0x009C */ null,
            /* 0x009D */ null,
            /* 0x009E */ null,
            /* 0x009F */ null,
            /* 0x00A0 */ null,
            /* 0x00A1 */ null,
            /* 0x00A2 */ null,
            /* 0x00A3 */ null,
            /* 0x00A4 */ null,
            /* 0x00A5 */ null,
            /* 0x00A6 */ null,
            /* 0x00A7 */ null,
            /* 0x00A8 */ null,
            /* 0x00A9 */ null,
            /* 0x00AA */ null,
            /* 0x00AB */ null,
            /* 0x00AC */ null,
            /* 0x00AD */ null,
            /* 0x00AE */ null,
            /* 0x00AF */ null,
            /* 0x00B0 */ null,
            /* 0x00B1 */ "AddDeleteThemeTransition", // Windows.UI.Xaml.Media.Animation.AddDeleteThemeTransition
            /* 0x00B2 */ "ArcSegment", // Windows.UI.Xaml.Media.ArcSegment
            /* 0x00B3 */ "BackEase", // Windows.UI.Xaml.Media.Animation.BackEase
            /* 0x00B4 */ "BeginStoryboard", // Windows.UI.Xaml.Media.Animation.BeginStoryboard
            /* 0x00B5 */ "BezierSegment", // Windows.UI.Xaml.Media.BezierSegment
            /* 0x00B6 */ "BindingBase", // Windows.UI.Xaml.Data.BindingBase
            /* 0x00B7 */ "BitmapCache", // Windows.UI.Xaml.Media.BitmapCache
            /* 0x00B8 */ "BitmapSource", // Windows.UI.Xaml.Media.Imaging.BitmapSource
            /* 0x00B9 */ "Block", // Windows.UI.Xaml.Documents.Block
            /* 0x00BA */ "BounceEase", // Windows.UI.Xaml.Media.Animation.BounceEase
            /* 0x00BB */ "CircleEase", // Windows.UI.Xaml.Media.Animation.CircleEase
            /* 0x00BC */ "ColorAnimation", // Windows.UI.Xaml.Media.Animation.ColorAnimation
            /* 0x00BD */ "ColorAnimationUsingKeyFrames", // Windows.UI.Xaml.Media.Animation.ColorAnimationUsingKeyFrames
            /* 0x00BE */ "ContentThemeTransition", // Windows.UI.Xaml.Media.Animation.ContentThemeTransition
            /* 0x00BF */ "ControlTemplate", // Windows.UI.Xaml.Controls.ControlTemplate
            /* 0x00C0 */ "CubicEase", // Windows.UI.Xaml.Media.Animation.CubicEase
            /* 0x00C1 */ "CustomResource", // Windows.UI.Xaml.CustomResource
            /* 0x00C2 */ "DataTemplate", // Windows.UI.Xaml.DataTemplate
            /* 0x00C3 */ "DiscreteColorKeyFrame", // Windows.UI.Xaml.Media.Animation.DiscreteColorKeyFrame
            /* 0x00C4 */ "DiscreteDoubleKeyFrame", // Windows.UI.Xaml.Media.Animation.DiscreteDoubleKeyFrame
            /* 0x00C5 */ "DiscreteObjectKeyFrame", // Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame
            /* 0x00C6 */ "DiscretePointKeyFrame", // Windows.UI.Xaml.Media.Animation.DiscretePointKeyFrame
            /* 0x00C7 */ "DispatcherTimer", // Windows.UI.Xaml.DispatcherTimer
            /* 0x00C8 */ "DoubleAnimation", // Windows.UI.Xaml.Media.Animation.DoubleAnimation
            /* 0x00C9 */ "DoubleAnimationUsingKeyFrames", // Windows.UI.Xaml.Media.Animation.DoubleAnimationUsingKeyFrames
            /* 0x00CA */ "Duration", // Windows.UI.Xaml.Duration
            /* 0x00CB */ "DynamicTimeline", // Windows.UI.Xaml.Media.Animation.DynamicTimeline
            /* 0x00CC */ "EasingColorKeyFrame", // Windows.UI.Xaml.Media.Animation.EasingColorKeyFrame
            /* 0x00CD */ "EasingDoubleKeyFrame", // Windows.UI.Xaml.Media.Animation.EasingDoubleKeyFrame
            /* 0x00CE */ "EasingPointKeyFrame", // Windows.UI.Xaml.Media.Animation.EasingPointKeyFrame
            /* 0x00CF */ "EdgeUIThemeTransition", // Windows.UI.Xaml.Media.Animation.EdgeUIThemeTransition
            /* 0x00D0 */ "ElasticEase", // Windows.UI.Xaml.Media.Animation.ElasticEase
            /* 0x00D1 */ "EllipseGeometry", // Windows.UI.Xaml.Media.EllipseGeometry
            /* 0x00D2 */ "EntranceThemeTransition", // Windows.UI.Xaml.Media.Animation.EntranceThemeTransition
            /* 0x00D3 */ "EventTrigger", // Windows.UI.Xaml.EventTrigger
            /* 0x00D4 */ "ExponentialEase", // Windows.UI.Xaml.Media.Animation.ExponentialEase
            /* 0x00D5 */ "Flyout", // Windows.UI.Xaml.Controls.Flyout
            /* 0x00D6 */ "FrameworkElement", // Windows.UI.Xaml.FrameworkElement
            /* 0x00D7 */ "FrameworkElementAutomationPeer", // Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer
            /* 0x00D8 */ "GeometryGroup", // Windows.UI.Xaml.Media.GeometryGroup
            /* 0x00D9 */ "GradientBrush", // Windows.UI.Xaml.Media.GradientBrush
            /* 0x00DA */ "GridViewItemTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.GridViewItemTemplateSettings
            /* 0x00DB */ "GroupedDataCollectionView", // Windows.UI.Xaml.Data.GroupedDataCollectionView
            /* 0x00DC */ "HWCompLeafNode", // Windows.UI.Xaml.Internal.HWCompLeafNode
            /* 0x00DD */ "HWCompTreeNode", // Windows.UI.Xaml.Internal.HWCompTreeNode
            /* 0x00DE */ "HyperlinkAutomationPeer", // Windows.UI.Xaml.Automation.Peers.HyperlinkAutomationPeer
            /* 0x00DF */ "Inline", // Windows.UI.Xaml.Documents.Inline
            /* 0x00E0 */ "InputPaneThemeTransition", // Windows.UI.Xaml.Media.Animation.InputPaneThemeTransition
            /* 0x00E1 */ "InternalTransform", // Windows.UI.Xaml.Internal.InternalTransform
            /* 0x00E2 */ "ItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ItemAutomationPeer
            /* 0x00E3 */ "ItemsPanelTemplate", // Windows.UI.Xaml.Controls.ItemsPanelTemplate
            /* 0x00E4 */ "KeyTime", // Windows.UI.Xaml.Media.Animation.KeyTime
            /* 0x00E5 */ "LayoutTransitionElement", // Windows.UI.Xaml.Internal.LayoutTransitionElement
            /* 0x00E6 */ "LinearColorKeyFrame", // Windows.UI.Xaml.Media.Animation.LinearColorKeyFrame
            /* 0x00E7 */ "LinearDoubleKeyFrame", // Windows.UI.Xaml.Media.Animation.LinearDoubleKeyFrame
            /* 0x00E8 */ "LinearPointKeyFrame", // Windows.UI.Xaml.Media.Animation.LinearPointKeyFrame
            /* 0x00E9 */ "LineGeometry", // Windows.UI.Xaml.Media.LineGeometry
            /* 0x00EA */ "LineSegment", // Windows.UI.Xaml.Media.LineSegment
            /* 0x00EB */ "ListViewItemTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.ListViewItemTemplateSettings
            /* 0x00EC */ "Matrix3DProjection", // Windows.UI.Xaml.Media.Matrix3DProjection
            /* 0x00ED */ "MediaSwapChainElement", // Windows.UI.Xaml.Internal.MediaSwapChainElement
            /* 0x00EE */ "MenuFlyout", // Windows.UI.Xaml.Controls.MenuFlyout
            /* 0x00EF */ "NullExtension", // Windows.UI.Xaml.NullExtension
            /* 0x00F0 */ "ObjectAnimationUsingKeyFrames", // Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames
            /* 0x00F1 */ "PaneThemeTransition", // Windows.UI.Xaml.Media.Animation.PaneThemeTransition
            /* 0x00F2 */ "ParallelTimeline", // Windows.UI.Xaml.Media.Animation.ParallelTimeline
            /* 0x00F3 */ "PathGeometry", // Windows.UI.Xaml.Media.PathGeometry
            /* 0x00F4 */ "PlaneProjection", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x00F5 */ "PointAnimation", // Windows.UI.Xaml.Media.Animation.PointAnimation
            /* 0x00F6 */ "PointAnimationUsingKeyFrames", // Windows.UI.Xaml.Media.Animation.PointAnimationUsingKeyFrames
            /* 0x00F7 */ "PointerAnimationUsingKeyFrames", // Windows.UI.Xaml.Internal.PointerAnimationUsingKeyFrames
            /* 0x00F8 */ "PolyBezierSegment", // Windows.UI.Xaml.Media.PolyBezierSegment
            /* 0x00F9 */ "PolyLineSegment", // Windows.UI.Xaml.Media.PolyLineSegment
            /* 0x00FA */ "PolyQuadraticBezierSegment", // Windows.UI.Xaml.Media.PolyQuadraticBezierSegment
            /* 0x00FB */ "PopupThemeTransition", // Windows.UI.Xaml.Media.Animation.PopupThemeTransition
            /* 0x00FC */ "PowerEase", // Windows.UI.Xaml.Media.Animation.PowerEase
            /* 0x00FD */ "PVLStaggerFunction", // Windows.UI.Xaml.PVLStaggerFunction
            /* 0x00FE */ "QuadraticBezierSegment", // Windows.UI.Xaml.Media.QuadraticBezierSegment
            /* 0x00FF */ "QuadraticEase", // Windows.UI.Xaml.Media.Animation.QuadraticEase
            /* 0x0100 */ "QuarticEase", // Windows.UI.Xaml.Media.Animation.QuarticEase
            /* 0x0101 */ "QuinticEase", // Windows.UI.Xaml.Media.Animation.QuinticEase
            /* 0x0102 */ "RectangleGeometry", // Windows.UI.Xaml.Media.RectangleGeometry
            /* 0x0103 */ "RelativeSource", // Windows.UI.Xaml.Data.RelativeSource
            /* 0x0104 */ "RenderTargetBitmap", // Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap
            /* 0x0105 */ "ReorderThemeTransition", // Windows.UI.Xaml.Media.Animation.ReorderThemeTransition
            /* 0x0106 */ "RepositionThemeTransition", // Windows.UI.Xaml.Media.Animation.RepositionThemeTransition
            /* 0x0107 */ "Setter", // Windows.UI.Xaml.Setter
            /* 0x0108 */ "SineEase", // Windows.UI.Xaml.Media.Animation.SineEase
            /* 0x0109 */ "SolidColorBrush", // Windows.UI.Xaml.Media.SolidColorBrush
            /* 0x010A */ "SplineColorKeyFrame", // Windows.UI.Xaml.Media.Animation.SplineColorKeyFrame
            /* 0x010B */ "SplineDoubleKeyFrame", // Windows.UI.Xaml.Media.Animation.SplineDoubleKeyFrame
            /* 0x010C */ "SplinePointKeyFrame", // Windows.UI.Xaml.Media.Animation.SplinePointKeyFrame
            /* 0x010D */ "StaticResource", // Windows.UI.Xaml.StaticResource
            /* 0x010E */ "SurfaceImageSource", // Windows.UI.Xaml.Media.Imaging.SurfaceImageSource
            /* 0x010F */ "SwapChainElement", // Windows.UI.Xaml.Internal.SwapChainElement
            /* 0x0110 */ "TemplateBinding", // Windows.UI.Xaml.Data.TemplateBinding
            /* 0x0111 */ "ThemeResource", // Windows.UI.Xaml.ThemeResource
            /* 0x0112 */ "TileBrush", // Windows.UI.Xaml.Media.TileBrush
            /* 0x0113 */ "Transform", // Windows.UI.Xaml.Media.Transform
            /* 0x0114 */ "VectorCollectionView", // Windows.UI.Xaml.Data.VectorCollectionView
            /* 0x0115 */ "VectorViewCollectionView", // Windows.UI.Xaml.Data.VectorViewCollectionView
            /* 0x0116 */ null,
            /* 0x0117 */ null,
            /* 0x0118 */ null,
            /* 0x0119 */ "AppBarAutomationPeer", // Windows.UI.Xaml.Automation.Peers.AppBarAutomationPeer
            /* 0x011A */ "AppBarLightDismissAutomationPeer", // Windows.UI.Xaml.Automation.Peers.AppBarLightDismissAutomationPeer
            /* 0x011B */ "AutomationPeerCollection", // Windows.UI.Xaml.Automation.Peers.AutomationPeerCollection
            /* 0x011C */ "AutoSuggestBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.AutoSuggestBoxAutomationPeer
            /* 0x011D */ "BitmapImage", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x011E */ "Border", // Windows.UI.Xaml.Controls.Border
            /* 0x011F */ "ButtonBaseAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ButtonBaseAutomationPeer
            /* 0x0120 */ "CaptureElement", // Windows.UI.Xaml.Controls.CaptureElement
            /* 0x0121 */ "CaptureElementAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CaptureElementAutomationPeer
            /* 0x0122 */ "ColorKeyFrameCollection", // Windows.UI.Xaml.Media.Animation.ColorKeyFrameCollection
            /* 0x0123 */ "ColumnDefinitionCollection", // Windows.UI.Xaml.Controls.ColumnDefinitionCollection
            /* 0x0124 */ "ComboBoxItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ComboBoxItemAutomationPeer
            /* 0x0125 */ "ComboBoxLightDismissAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ComboBoxLightDismissAutomationPeer
            /* 0x0126 */ "ComponentHost", // Windows.UI.Xaml.Internal.ComponentHost
            /* 0x0127 */ "CompositeTransform", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0128 */ "ContentPresenter", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0129 */ "Control", // Windows.UI.Xaml.Controls.Control
            /* 0x012A */ "DatePickerAutomationPeer", // Windows.UI.Xaml.Automation.Peers.DatePickerAutomationPeer
            /* 0x012B */ "DisplayMemberTemplate", // Windows.UI.Xaml.Internal.DisplayMemberTemplate
            /* 0x012C */ "DoubleCollection", // Windows.UI.Xaml.Media.DoubleCollection
            /* 0x012D */ "DoubleKeyFrameCollection", // Windows.UI.Xaml.Media.Animation.DoubleKeyFrameCollection
            /* 0x012E */ "DragItemThemeAnimation", // Windows.UI.Xaml.Media.Animation.DragItemThemeAnimation
            /* 0x012F */ "DragOverThemeAnimation", // Windows.UI.Xaml.Media.Animation.DragOverThemeAnimation
            /* 0x0130 */ "DropTargetItemThemeAnimation", // Windows.UI.Xaml.Media.Animation.DropTargetItemThemeAnimation
            /* 0x0131 */ "FaceplateContentPresenterAutomationPeer", // Windows.UI.Xaml.Automation.Peers.FaceplateContentPresenterAutomationPeer
            /* 0x0132 */ "FadeInThemeAnimation", // Windows.UI.Xaml.Media.Animation.FadeInThemeAnimation
            /* 0x0133 */ "FadeOutThemeAnimation", // Windows.UI.Xaml.Media.Animation.FadeOutThemeAnimation
            /* 0x0134 */ "FlipViewItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.FlipViewItemAutomationPeer
            /* 0x0135 */ "FloatCollection", // Windows.UI.Xaml.Media.FloatCollection
            /* 0x0136 */ "FlyoutPresenterAutomationPeer", // Windows.UI.Xaml.Automation.Peers.FlyoutPresenterAutomationPeer
            /* 0x0137 */ "GeometryCollection", // Windows.UI.Xaml.Media.GeometryCollection
            /* 0x0138 */ "Glyphs", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0139 */ "GradientStopCollection", // Windows.UI.Xaml.Media.GradientStopCollection
            /* 0x013A */ "GroupItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.GroupItemAutomationPeer
            /* 0x013B */ "HubAutomationPeer", // Windows.UI.Xaml.Automation.Peers.HubAutomationPeer
            /* 0x013C */ "HubSectionAutomationPeer", // Windows.UI.Xaml.Automation.Peers.HubSectionAutomationPeer
            /* 0x013D */ "HubSectionCollection", // Windows.UI.Xaml.Controls.HubSectionCollection
            /* 0x013E */ "HWCompMediaNode", // Windows.UI.Xaml.Internal.HWCompMediaNode
            /* 0x013F */ "HWCompRenderDataNode", // Windows.UI.Xaml.Internal.HWCompRenderDataNode
            /* 0x0140 */ "HWCompSwapChainNode", // Windows.UI.Xaml.Internal.HWCompSwapChainNode
            /* 0x0141 */ "HWCompTreeYCbCrNode", // Windows.UI.Xaml.Internal.HWCompTreeYCbCrNode
            /* 0x0142 */ "HWCompWebViewNode", // Windows.UI.Xaml.Internal.HWCompWebViewNode
            /* 0x0143 */ "HWCompYCbCrTextureNode", // Windows.UI.Xaml.Internal.HWCompYCbCrTextureNode
            /* 0x0144 */ "HWRedirectedCompTreeNode", // Windows.UI.Xaml.Internal.HWRedirectedCompTreeNode
            /* 0x0145 */ "IconElement", // Windows.UI.Xaml.Controls.IconElement
            /* 0x0146 */ "Image", // Windows.UI.Xaml.Controls.Image
            /* 0x0147 */ "ImageAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ImageAutomationPeer
            /* 0x0148 */ "ImageBrush", // Windows.UI.Xaml.Media.ImageBrush
            /* 0x0149 */ "InlineUIContainer", // Windows.UI.Xaml.Documents.InlineUIContainer
            /* 0x014A */ "InputScopeNameCollection", // Windows.UI.Xaml.Input.InputScopeNameCollection
            /* 0x014B */ "ItemsControlAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ItemsControlAutomationPeer
            /* 0x014C */ "ItemsPresenter", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x014D */ "IterableCollectionView", // Windows.UI.Xaml.Data.IterableCollectionView
            /* 0x014E */ "LinearGradientBrush", // Windows.UI.Xaml.Media.LinearGradientBrush
            /* 0x014F */ "LineBreak", // Windows.UI.Xaml.Documents.LineBreak
            /* 0x0150 */ "ListBoxItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListBoxItemAutomationPeer
            /* 0x0151 */ "ListViewBaseHeaderItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewBaseHeaderItemAutomationPeer
            /* 0x0152 */ "ListViewBaseItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewBaseItemAutomationPeer
            /* 0x0153 */ "ListViewBaseItemSecondaryChrome", // Windows.UI.Xaml.Controls.Primitives.ListViewBaseItemSecondaryChrome
            /* 0x0154 */ "MatrixTransform", // Windows.UI.Xaml.Media.MatrixTransform
            /* 0x0155 */ "MediaBase", // Windows.UI.Xaml.Internal.MediaBase
            /* 0x0156 */ "MediaElement", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0157 */ "MediaElementAutomationPeer", // Windows.UI.Xaml.Automation.Peers.MediaElementAutomationPeer
            /* 0x0158 */ "MediaTransportControlsAutomationPeer", // Windows.UI.Xaml.Automation.Peers.MediaTransportControlsAutomationPeer
            /* 0x0159 */ "MenuFlyoutItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.MenuFlyoutItemAutomationPeer
            /* 0x015A */ "MenuFlyoutItemBaseCollection", // Windows.UI.Xaml.Controls.MenuFlyoutItemBaseCollection
            /* 0x015B */ "ObjectKeyFrameCollection", // Windows.UI.Xaml.Media.Animation.ObjectKeyFrameCollection
            /* 0x015C */ "Panel", // Windows.UI.Xaml.Controls.Panel
            /* 0x015D */ "Paragraph", // Windows.UI.Xaml.Documents.Paragraph
            /* 0x015E */ "ParametricCurveCollection", // Windows.UI.Xaml.Internal.ParametricCurveCollection
            /* 0x015F */ "ParametricCurveSegmentCollection", // Windows.UI.Xaml.Internal.ParametricCurveSegmentCollection
            /* 0x0160 */ "PasswordBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.PasswordBoxAutomationPeer
            /* 0x0161 */ "PathFigureCollection", // Windows.UI.Xaml.Media.PathFigureCollection
            /* 0x0162 */ "PathSegmentCollection", // Windows.UI.Xaml.Media.PathSegmentCollection
            /* 0x0163 */ "PointCollection", // Windows.UI.Xaml.Media.PointCollection
            /* 0x0164 */ "PointerCollection", // Windows.UI.Xaml.Input.PointerCollection
            /* 0x0165 */ "PointerDownThemeAnimation", // Windows.UI.Xaml.Media.Animation.PointerDownThemeAnimation
            /* 0x0166 */ "PointerKeyFrameCollection", // Windows.UI.Xaml.Internal.PointerKeyFrameCollection
            /* 0x0167 */ "PointerUpThemeAnimation", // Windows.UI.Xaml.Media.Animation.PointerUpThemeAnimation
            /* 0x0168 */ "PointKeyFrameCollection", // Windows.UI.Xaml.Media.Animation.PointKeyFrameCollection
            /* 0x0169 */ "PopInThemeAnimation", // Windows.UI.Xaml.Media.Animation.PopInThemeAnimation
            /* 0x016A */ "PopOutThemeAnimation", // Windows.UI.Xaml.Media.Animation.PopOutThemeAnimation
            /* 0x016B */ "Popup", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x016C */ "PopupAutomationPeer", // Windows.UI.Xaml.Automation.Peers.PopupAutomationPeer
            /* 0x016D */ "PopupRootAutomationPeer", // Windows.UI.Xaml.Automation.Peers.PopupRootAutomationPeer
            /* 0x016E */ "ProgressRingAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ProgressRingAutomationPeer
            /* 0x016F */ "RadialGradientBrush", // Windows.UI.Xaml.Media.RadialGradientBrush
            /* 0x0170 */ "RangeBaseAutomationPeer", // Windows.UI.Xaml.Automation.Peers.RangeBaseAutomationPeer
            /* 0x0171 */ "RepeatBehavior", // Windows.UI.Xaml.Media.Animation.RepeatBehavior
            /* 0x0172 */ "RepositionThemeAnimation", // Windows.UI.Xaml.Media.Animation.RepositionThemeAnimation
            /* 0x0173 */ "ResourceDictionary", // Windows.UI.Xaml.ResourceDictionary
            /* 0x0174 */ "ResourceDictionaryCollection", // Windows.UI.Xaml.Internal.ResourceDictionaryCollection
            /* 0x0175 */ "RichEditBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.RichEditBoxAutomationPeer
            /* 0x0176 */ "RichTextBlock", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x0177 */ "RichTextBlockAutomationPeer", // Windows.UI.Xaml.Automation.Peers.RichTextBlockAutomationPeer
            /* 0x0178 */ "RichTextBlockOverflow", // Windows.UI.Xaml.Controls.RichTextBlockOverflow
            /* 0x0179 */ "RichTextBlockOverflowAutomationPeer", // Windows.UI.Xaml.Automation.Peers.RichTextBlockOverflowAutomationPeer
            /* 0x017A */ "RotateTransform", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x017B */ "RowDefinitionCollection", // Windows.UI.Xaml.Controls.RowDefinitionCollection
            /* 0x017C */ "Run", // Windows.UI.Xaml.Documents.Run
            /* 0x017D */ "ScaleTransform", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x017E */ "ScrollViewerAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ScrollViewerAutomationPeer
            /* 0x017F */ "SearchBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SearchBoxAutomationPeer
            /* 0x0180 */ "SelectorItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SelectorItemAutomationPeer
            /* 0x0181 */ "SemanticZoomAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SemanticZoomAutomationPeer
            /* 0x0182 */ "SetterBaseCollection", // Windows.UI.Xaml.SetterBaseCollection
            /* 0x0183 */ "SettingsFlyoutAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SettingsFlyoutAutomationPeer
            /* 0x0184 */ "Shape", // Windows.UI.Xaml.Shapes.Shape
            /* 0x0185 */ "SkewTransform", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x0186 */ "Span", // Windows.UI.Xaml.Documents.Span
            /* 0x0187 */ "SplitCloseThemeAnimation", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x0188 */ "SplitOpenThemeAnimation", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x0189 */ "Storyboard", // Windows.UI.Xaml.Media.Animation.Storyboard
            /* 0x018A */ "SwipeBackThemeAnimation", // Windows.UI.Xaml.Media.Animation.SwipeBackThemeAnimation
            /* 0x018B */ "SwipeHintThemeAnimation", // Windows.UI.Xaml.Media.Animation.SwipeHintThemeAnimation
            /* 0x018C */ "TextBlock", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x018D */ "TextBlockAutomationPeer", // Windows.UI.Xaml.Automation.Peers.TextBlockAutomationPeer
            /* 0x018E */ "TextBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.TextBoxAutomationPeer
            /* 0x018F */ "TextBoxBaseAutomationPeer", // Windows.UI.Xaml.Automation.Peers.TextBoxBaseAutomationPeer
            /* 0x0190 */ "TextBoxView", // Windows.UI.Xaml.Internal.TextBoxView
            /* 0x0191 */ "TextElementCollection", // Windows.UI.Xaml.Documents.TextElementCollection
            /* 0x0192 */ "ThemeAnimationBase", // Windows.UI.Xaml.Media.Animation.ThemeAnimationBase
            /* 0x0193 */ "ThumbAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ThumbAutomationPeer
            /* 0x0194 */ "TimelineCollection", // Windows.UI.Xaml.Media.Animation.TimelineCollection
            /* 0x0195 */ "TimelineMarkerCollection", // Windows.UI.Xaml.Media.TimelineMarkerCollection
            /* 0x0196 */ "TimePickerAutomationPeer", // Windows.UI.Xaml.Automation.Peers.TimePickerAutomationPeer
            /* 0x0197 */ "ToggleMenuFlyoutItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ToggleMenuFlyoutItemAutomationPeer
            /* 0x0198 */ "ToggleSwitchAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ToggleSwitchAutomationPeer
            /* 0x0199 */ "ToolTipAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ToolTipAutomationPeer
            /* 0x019A */ "TransformCollection", // Windows.UI.Xaml.Media.TransformCollection
            /* 0x019B */ "TransformGroup", // Windows.UI.Xaml.Media.TransformGroup
            /* 0x019C */ "TransitionCollection", // Windows.UI.Xaml.Media.Animation.TransitionCollection
            /* 0x019D */ "TranslateTransform", // Windows.UI.Xaml.Media.TranslateTransform
            /* 0x019E */ "TriggerActionCollection", // Windows.UI.Xaml.TriggerActionCollection
            /* 0x019F */ "TriggerCollection", // Windows.UI.Xaml.TriggerCollection
            /* 0x01A0 */ "UIElementCollection", // Windows.UI.Xaml.Controls.UIElementCollection
            /* 0x01A1 */ "Viewbox", // Windows.UI.Xaml.Controls.Viewbox
            /* 0x01A2 */ "VirtualSurfaceImageSource", // Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource
            /* 0x01A3 */ "VisualStateCollection", // Windows.UI.Xaml.Internal.VisualStateCollection
            /* 0x01A4 */ "VisualStateGroupCollection", // Windows.UI.Xaml.Internal.VisualStateGroupCollection
            /* 0x01A5 */ "VisualTransitionCollection", // Windows.UI.Xaml.Internal.VisualTransitionCollection
            /* 0x01A6 */ "WebViewAutomationPeer", // Windows.UI.Xaml.Automation.Peers.WebViewAutomationPeer
            /* 0x01A7 */ "WebViewBrush", // Windows.UI.Xaml.Controls.WebViewBrush
            /* 0x01A8 */ "WriteableBitmap", // Windows.UI.Xaml.Media.Imaging.WriteableBitmap
            /* 0x01A9 */ null,
            /* 0x01AA */ null,
            /* 0x01AB */ "AppBarSeparator", // Windows.UI.Xaml.Controls.AppBarSeparator
            /* 0x01AC */ "BasedOnSetterCollection", // Windows.UI.Xaml.BasedOnSetterCollection
            /* 0x01AD */ "BitmapIcon", // Windows.UI.Xaml.Controls.BitmapIcon
            /* 0x01AE */ "Bold", // Windows.UI.Xaml.Documents.Bold
            /* 0x01AF */ "ButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ButtonAutomationPeer
            /* 0x01B0 */ "Canvas", // Windows.UI.Xaml.Controls.Canvas
            /* 0x01B1 */ "ComboBoxItemDataAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ComboBoxItemDataAutomationPeer
            /* 0x01B2 */ "CommandBarElementCollection", // Windows.UI.Xaml.Controls.CommandBarElementCollection
            /* 0x01B3 */ "ContentControl", // Windows.UI.Xaml.Controls.ContentControl
            /* 0x01B4 */ "DatePicker", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x01B5 */ "DependencyObjectCollection", // Windows.UI.Xaml.DependencyObjectCollection
            /* 0x01B6 */ "Ellipse", // Windows.UI.Xaml.Shapes.Ellipse
            /* 0x01B7 */ "FlipViewItemDataAutomationPeer", // Windows.UI.Xaml.Automation.Peers.FlipViewItemDataAutomationPeer
            /* 0x01B8 */ "FontIcon", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x01B9 */ "FullWindowMediaRoot", // Windows.UI.Xaml.FullWindowMediaRoot
            /* 0x01BA */ "Grid", // Windows.UI.Xaml.Controls.Grid
            /* 0x01BB */ "GridViewHeaderItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.GridViewHeaderItemAutomationPeer
            /* 0x01BC */ "GridViewItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.GridViewItemAutomationPeer
            /* 0x01BD */ "Hub", // Windows.UI.Xaml.Controls.Hub
            /* 0x01BE */ "HubSection", // Windows.UI.Xaml.Controls.HubSection
            /* 0x01BF */ "Hyperlink", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x01C0 */ "HyperlinkButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.HyperlinkButtonAutomationPeer
            /* 0x01C1 */ "Italic", // Windows.UI.Xaml.Documents.Italic
            /* 0x01C2 */ "ItemCollection", // Windows.UI.Xaml.Controls.ItemCollection
            /* 0x01C3 */ "ItemsControl", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x01C4 */ "Line", // Windows.UI.Xaml.Shapes.Line
            /* 0x01C5 */ "ListBoxItemDataAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListBoxItemDataAutomationPeer
            /* 0x01C6 */ "ListViewBaseItemDataAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewBaseItemDataAutomationPeer
            /* 0x01C7 */ "ListViewBaseItemPresenter", // Windows.UI.Xaml.Controls.Primitives.ListViewBaseItemPresenter
            /* 0x01C8 */ "ListViewHeaderItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewHeaderItemAutomationPeer
            /* 0x01C9 */ "ListViewItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewItemAutomationPeer
            /* 0x01CA */ "MediaTransportControls", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x01CB */ "MenuFlyoutItemBase", // Windows.UI.Xaml.Controls.MenuFlyoutItemBase
            /* 0x01CC */ "MenuFlyoutPresenterAutomationPeer", // Windows.UI.Xaml.Automation.Peers.MenuFlyoutPresenterAutomationPeer
            /* 0x01CD */ "ModernCollectionBasePanel", // Windows.UI.Xaml.Controls.ModernCollectionBasePanel
            /* 0x01CE */ "PasswordBox", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x01CF */ "Path", // Windows.UI.Xaml.Shapes.Path
            /* 0x01D0 */ "PathIcon", // Windows.UI.Xaml.Controls.PathIcon
            /* 0x01D1 */ "Polygon", // Windows.UI.Xaml.Shapes.Polygon
            /* 0x01D2 */ "Polyline", // Windows.UI.Xaml.Shapes.Polyline
            /* 0x01D3 */ "ProgressBarAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ProgressBarAutomationPeer
            /* 0x01D4 */ "ProgressRing", // Windows.UI.Xaml.Controls.ProgressRing
            /* 0x01D5 */ "RangeBase", // Windows.UI.Xaml.Controls.Primitives.RangeBase
            /* 0x01D6 */ "Rectangle", // Windows.UI.Xaml.Shapes.Rectangle
            /* 0x01D7 */ "RenderTargetBitmapRoot", // Windows.UI.Xaml.RenderTargetBitmapRoot
            /* 0x01D8 */ "RepeatButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.RepeatButtonAutomationPeer
            /* 0x01D9 */ "RichEditBox", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x01DA */ "RootVisual", // Windows.UI.Xaml.RootVisual
            /* 0x01DB */ "ScrollBarAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ScrollBarAutomationPeer
            /* 0x01DC */ "ScrollContentPresenter", // Windows.UI.Xaml.Controls.ScrollContentPresenter
            /* 0x01DD */ "SearchBox", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x01DE */ "SelectorAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SelectorAutomationPeer
            /* 0x01DF */ "SemanticZoom", // Windows.UI.Xaml.Controls.SemanticZoom
            /* 0x01E0 */ "SliderAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SliderAutomationPeer
            /* 0x01E1 */ "StackPanel", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x01E2 */ "SymbolIcon", // Windows.UI.Xaml.Controls.SymbolIcon
            /* 0x01E3 */ "TextBox", // Windows.UI.Xaml.Controls.TextBox
            /* 0x01E4 */ "TextBoxBase", // Windows.UI.Xaml.Internal.TextBoxBase
            /* 0x01E5 */ "Thumb", // Windows.UI.Xaml.Controls.Primitives.Thumb
            /* 0x01E6 */ "TickBar", // Windows.UI.Xaml.Controls.Primitives.TickBar
            /* 0x01E7 */ "TimePicker", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x01E8 */ "ToggleButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ToggleButtonAutomationPeer
            /* 0x01E9 */ "ToggleSwitch", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x01EA */ "Underline", // Windows.UI.Xaml.Documents.Underline
            /* 0x01EB */ "UserControl", // Windows.UI.Xaml.Controls.UserControl
            /* 0x01EC */ "VariableSizedWrapGrid", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x01ED */ "VirtualizingPanel", // Windows.UI.Xaml.Controls.VirtualizingPanel
            /* 0x01EE */ "WebView", // Windows.UI.Xaml.Controls.WebView
            /* 0x01EF */ "AppBar", // Windows.UI.Xaml.Controls.AppBar
            /* 0x01F0 */ "AppBarButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.AppBarButtonAutomationPeer
            /* 0x01F1 */ "AppBarLightDismiss", // Windows.UI.Xaml.Controls.AppBarLightDismiss
            /* 0x01F2 */ "AppBarToggleButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.AppBarToggleButtonAutomationPeer
            /* 0x01F3 */ "AutoSuggestBox", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x01F4 */ "BlockCollection", // Windows.UI.Xaml.Documents.BlockCollection
            /* 0x01F5 */ "ButtonBase", // Windows.UI.Xaml.Controls.Primitives.ButtonBase
            /* 0x01F6 */ "CarouselPanel", // Windows.UI.Xaml.Controls.Primitives.CarouselPanel
            /* 0x01F7 */ "CheckBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CheckBoxAutomationPeer
            /* 0x01F8 */ "ComboBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ComboBoxAutomationPeer
            /* 0x01F9 */ "ComboBoxLightDismiss", // Windows.UI.Xaml.Controls.ComboBoxLightDismiss
            /* 0x01FA */ "ContentDialog", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x01FB */ "FlipViewAutomationPeer", // Windows.UI.Xaml.Automation.Peers.FlipViewAutomationPeer
            /* 0x01FC */ "FlyoutPresenter", // Windows.UI.Xaml.Controls.FlyoutPresenter
            /* 0x01FD */ "Frame", // Windows.UI.Xaml.Controls.Frame
            /* 0x01FE */ "GridViewItemDataAutomationPeer", // Windows.UI.Xaml.Automation.Peers.GridViewItemDataAutomationPeer
            /* 0x01FF */ "GridViewItemPresenter", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0200 */ "GroupItem", // Windows.UI.Xaml.Controls.GroupItem
            /* 0x0201 */ "InlineCollection", // Windows.UI.Xaml.Documents.InlineCollection
            /* 0x0202 */ "ItemsStackPanel", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x0203 */ "ItemsWrapGrid", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x0204 */ "ListBoxAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListBoxAutomationPeer
            /* 0x0205 */ "ListViewBaseAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewBaseAutomationPeer
            /* 0x0206 */ "ListViewBaseHeaderItem", // Windows.UI.Xaml.Controls.ListViewBaseHeaderItem
            /* 0x0207 */ "ListViewItemDataAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewItemDataAutomationPeer
            /* 0x0208 */ "ListViewItemPresenter", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0209 */ "MenuFlyoutItem", // Windows.UI.Xaml.Controls.MenuFlyoutItem
            /* 0x020A */ "MenuFlyoutPresenter", // Windows.UI.Xaml.Controls.MenuFlyoutPresenter
            /* 0x020B */ "MenuFlyoutSeparator", // Windows.UI.Xaml.Controls.MenuFlyoutSeparator
            /* 0x020C */ "OrientedVirtualizingPanel", // Windows.UI.Xaml.Controls.Primitives.OrientedVirtualizingPanel
            /* 0x020D */ "Page", // Windows.UI.Xaml.Controls.Page
            /* 0x020E */ "PopupRoot", // Windows.UI.Xaml.PopupRoot
            /* 0x020F */ "PrintRoot", // Windows.UI.Xaml.PrintRoot
            /* 0x0210 */ "ProgressBar", // Windows.UI.Xaml.Controls.ProgressBar
            /* 0x0211 */ "RadioButtonAutomationPeer", // Windows.UI.Xaml.Automation.Peers.RadioButtonAutomationPeer
            /* 0x0212 */ "ScrollBar", // Windows.UI.Xaml.Controls.Primitives.ScrollBar
            /* 0x0213 */ "ScrollContentControl", // Windows.UI.Xaml.Controls.ScrollContentControl
            /* 0x0214 */ "SelectorItem", // Windows.UI.Xaml.Controls.Primitives.SelectorItem
            /* 0x0215 */ "SettingsFlyout", // Windows.UI.Xaml.Controls.SettingsFlyout
            /* 0x0216 */ "Slider", // Windows.UI.Xaml.Controls.Slider
            /* 0x0217 */ "SwapChainBackgroundPanel", // Windows.UI.Xaml.Controls.SwapChainBackgroundPanel
            /* 0x0218 */ "SwapChainPanel", // Windows.UI.Xaml.Controls.SwapChainPanel
            /* 0x0219 */ "TextSelectionGripper", // Windows.UI.Xaml.Internal.TextSelectionGripper
            /* 0x021A */ "ToolTip", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x021B */ "TransitionRoot", // Windows.UI.Xaml.TransitionRoot
            /* 0x021C */ "Button", // Windows.UI.Xaml.Controls.Button
            /* 0x021D */ "ComboBoxItem", // Windows.UI.Xaml.Controls.ComboBoxItem
            /* 0x021E */ "CommandBar", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x021F */ "FlipViewItem", // Windows.UI.Xaml.Controls.FlipViewItem
            /* 0x0220 */ "GridViewAutomationPeer", // Windows.UI.Xaml.Automation.Peers.GridViewAutomationPeer
            /* 0x0221 */ "GridViewHeaderItem", // Windows.UI.Xaml.Controls.GridViewHeaderItem
            /* 0x0222 */ "HyperlinkButton", // Windows.UI.Xaml.Controls.HyperlinkButton
            /* 0x0223 */ "ListBoxItem", // Windows.UI.Xaml.Controls.ListBoxItem
            /* 0x0224 */ "ListViewAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ListViewAutomationPeer
            /* 0x0225 */ "ListViewBaseItem", // Windows.UI.Xaml.Controls.ListViewBaseItem
            /* 0x0226 */ "ListViewHeaderItem", // Windows.UI.Xaml.Controls.ListViewHeaderItem
            /* 0x0227 */ "RepeatButton", // Windows.UI.Xaml.Controls.Primitives.RepeatButton
            /* 0x0228 */ "ScrollViewer", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x0229 */ "ToggleButton", // Windows.UI.Xaml.Controls.Primitives.ToggleButton
            /* 0x022A */ "ToggleMenuFlyoutItem", // Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem
            /* 0x022B */ "VirtualizingStackPanel", // Windows.UI.Xaml.Controls.VirtualizingStackPanel
            /* 0x022C */ "WrapGrid", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x022D */ "AppBarButton", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x022E */ "AppBarToggleButton", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x022F */ "CheckBox", // Windows.UI.Xaml.Controls.CheckBox
            /* 0x0230 */ "GridViewItem", // Windows.UI.Xaml.Controls.GridViewItem
            /* 0x0231 */ "ListViewItem", // Windows.UI.Xaml.Controls.ListViewItem
            /* 0x0232 */ "RadioButton", // Windows.UI.Xaml.Controls.RadioButton
            /* 0x0233 */ "RootScrollViewer", // Windows.UI.Xaml.Internal.RootScrollViewer
            /* 0x0234 */ "Binding", // Windows.UI.Xaml.Data.Binding
            /* 0x0235 */ "Selector", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x0236 */ "ComboBox", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x0237 */ "FlipView", // Windows.UI.Xaml.Controls.FlipView
            /* 0x0238 */ "ListBox", // Windows.UI.Xaml.Controls.ListBox
            /* 0x0239 */ "ListViewBase", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x023A */ "GridView", // Windows.UI.Xaml.Controls.GridView
            /* 0x023B */ "ListView", // Windows.UI.Xaml.Controls.ListView
            /* 0x023C */ "AccessibilityView", // Windows.UI.Xaml.Automation.Peers.AccessibilityView
            /* 0x023D */ "AlignmentX", // Windows.UI.Xaml.Media.AlignmentX
            /* 0x023E */ "AlignmentY", // Windows.UI.Xaml.Media.AlignmentY
            /* 0x023F */ "AnimationDirection", // Windows.UI.Xaml.Controls.Primitives.AnimationDirection
            /* 0x0240 */ "AnnotationType", // Windows.UI.Xaml.Automation.AnnotationType
            /* 0x0241 */ "AppBarClosedDisplayMode", // Windows.UI.Xaml.Controls.AppBarClosedDisplayMode
            /* 0x0242 */ "ApplicationTheme", // Windows.UI.Xaml.ApplicationTheme
            /* 0x0243 */ "AudioCategory", // Windows.UI.Xaml.Media.AudioCategory
            /* 0x0244 */ "AudioDeviceType", // Windows.UI.Xaml.Media.AudioDeviceType
            /* 0x0245 */ "AutomationControlType", // Windows.UI.Xaml.Automation.Peers.AutomationControlType
            /* 0x0246 */ "AutomationEvents", // Windows.UI.Xaml.Automation.Peers.AutomationEvents
            /* 0x0247 */ "AutomationLiveSetting", // Windows.UI.Xaml.Automation.Peers.AutomationLiveSetting
            /* 0x0248 */ "AutomationOrientation", // Windows.UI.Xaml.Automation.Peers.AutomationOrientation
            /* 0x0249 */ "AutoSuggestionBoxTextChangeReason", // Windows.UI.Xaml.Controls.AutoSuggestionBoxTextChangeReason
            /* 0x024A */ "BindingMode", // Windows.UI.Xaml.Data.BindingMode
            /* 0x024B */ "BitmapCreateOptions", // Windows.UI.Xaml.Media.Imaging.BitmapCreateOptions
            /* 0x024C */ "BrushMappingMode", // Windows.UI.Xaml.Media.BrushMappingMode
            /* 0x024D */ "ClickMode", // Windows.UI.Xaml.Controls.ClickMode
            /* 0x024E */ "ClockState", // Windows.UI.Xaml.Media.Animation.ClockState
            /* 0x024F */ null,
            /* 0x0250 */ "CollectionChange", // Windows.Foundation.Collections.CollectionChange
            /* 0x0251 */ "ColorInterpolationMode", // Windows.UI.Xaml.Media.ColorInterpolationMode
            /* 0x0252 */ "ComponentResourceLocation", // Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation
            /* 0x0253 */ "ContentDialogResult", // Windows.UI.Xaml.Controls.ContentDialogResult
            /* 0x0254 */ "DecodePixelType", // Windows.UI.Xaml.Media.Imaging.DecodePixelType
            /* 0x0255 */ "DockPosition", // Windows.UI.Xaml.Automation.DockPosition
            /* 0x0256 */ "EasingMode", // Windows.UI.Xaml.Media.Animation.EasingMode
            /* 0x0257 */ "EdgeTransitionLocation", // Windows.UI.Xaml.Controls.Primitives.EdgeTransitionLocation
            /* 0x0258 */ "ElementCompositeMode", // Windows.UI.Xaml.Media.ElementCompositeMode
            /* 0x0259 */ "ElementTheme", // Windows.UI.Xaml.ElementTheme
            /* 0x025A */ "ExpandCollapseState", // Windows.UI.Xaml.Automation.ExpandCollapseState
            /* 0x025B */ "FillBehavior", // Windows.UI.Xaml.Media.Animation.FillBehavior
            /* 0x025C */ "FillRule", // Windows.UI.Xaml.Media.FillRule
            /* 0x025D */ "FlowDirection", // Windows.UI.Xaml.FlowDirection
            /* 0x025E */ "FlyoutPlacementMode", // Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode
            /* 0x025F */ "FocusNavigationDirection", // Windows.UI.Xaml.Input.FocusNavigationDirection
            /* 0x0260 */ "FocusState", // Windows.UI.Xaml.FocusState
            /* 0x0261 */ "FontCapitals", // Windows.UI.Xaml.FontCapitals
            /* 0x0262 */ "FontEastAsianLanguage", // Windows.UI.Xaml.FontEastAsianLanguage
            /* 0x0263 */ "FontEastAsianWidths", // Windows.UI.Xaml.FontEastAsianWidths
            /* 0x0264 */ "FontFraction", // Windows.UI.Xaml.FontFraction
            /* 0x0265 */ "FontNumeralAlignment", // Windows.UI.Xaml.FontNumeralAlignment
            /* 0x0266 */ "FontNumeralStyle", // Windows.UI.Xaml.FontNumeralStyle
            /* 0x0267 */ "FontStretch", // Windows.UI.Text.FontStretch
            /* 0x0268 */ "FontStyle", // Windows.UI.Text.FontStyle
            /* 0x0269 */ "FontVariants", // Windows.UI.Xaml.FontVariants
            /* 0x026A */ "GeneratorDirection", // Windows.UI.Xaml.Controls.Primitives.GeneratorDirection
            /* 0x026B */ "GestureModes", // Windows.UI.Xaml.Input.GestureModes
            /* 0x026C */ "GradientSpreadMethod", // Windows.UI.Xaml.Media.GradientSpreadMethod
            /* 0x026D */ "GridUnitType", // Windows.UI.Xaml.GridUnitType
            /* 0x026E */ "GroupHeaderPlacement", // Windows.UI.Xaml.Controls.Primitives.GroupHeaderPlacement
            /* 0x026F */ "HoldingState", // Windows.UI.Input.HoldingState
            /* 0x0270 */ "HorizontalAlignment", // Windows.UI.Xaml.HorizontalAlignment
            /* 0x0271 */ "IncrementalLoadingTrigger", // Windows.UI.Xaml.Controls.IncrementalLoadingTrigger
            /* 0x0272 */ "InputScopeNameValue", // Windows.UI.Xaml.Input.InputScopeNameValue
            /* 0x0273 */ "ItemsUpdatingScrollMode", // Windows.UI.Xaml.Controls.ItemsUpdatingScrollMode
            /* 0x0274 */ "KeyboardNavigationMode", // Windows.UI.Xaml.Input.KeyboardNavigationMode
            /* 0x0275 */ "LineStackingStrategy", // Windows.UI.Xaml.LineStackingStrategy
            /* 0x0276 */ "ListViewReorderMode", // Windows.UI.Xaml.Controls.ListViewReorderMode
            /* 0x0277 */ "ListViewSelectionMode", // Windows.UI.Xaml.Controls.ListViewSelectionMode
            /* 0x0278 */ "LogicalDirection", // Windows.UI.Xaml.Documents.LogicalDirection
            /* 0x0279 */ "ManipulationModes", // Windows.UI.Xaml.Input.ManipulationModes
            /* 0x027A */ "MarkupExtensionType", // Windows.UI.Xaml.MarkupExtensionType
            /* 0x027B */ "MediaCanPlayResponse", // Windows.UI.Xaml.Media.MediaCanPlayResponse
            /* 0x027C */ "MediaElementState", // Windows.UI.Xaml.Media.MediaElementState
            /* 0x027D */ "NavigationCacheMode", // Windows.UI.Xaml.Navigation.NavigationCacheMode
            /* 0x027E */ "NavigationMode", // Windows.UI.Xaml.Navigation.NavigationMode
            /* 0x027F */ "NotifyCollectionChangedAction", // Windows.UI.Xaml.Interop.NotifyCollectionChangedAction
            /* 0x0280 */ "OpticalMarginAlignment", // Windows.UI.Xaml.OpticalMarginAlignment
            /* 0x0281 */ "Orientation", // Windows.UI.Xaml.Controls.Orientation
            /* 0x0282 */ "PanelScrollingDirection", // Windows.UI.Xaml.Controls.PanelScrollingDirection
            /* 0x0283 */ "PatternInterface", // Windows.UI.Xaml.Automation.Peers.PatternInterface
            /* 0x0284 */ "PenLineCap", // Windows.UI.Xaml.Media.PenLineCap
            /* 0x0285 */ "PenLineJoin", // Windows.UI.Xaml.Media.PenLineJoin
            /* 0x0286 */ "PlacementMode", // Windows.UI.Xaml.Controls.Primitives.PlacementMode
            /* 0x0287 */ "PointerDeviceType", // Windows.Devices.Input.PointerDeviceType
            /* 0x0288 */ "PointerDirection", // Windows.UI.Xaml.Internal.PointerDirection
            /* 0x0289 */ "PreviewPageCountType", // Windows.UI.Xaml.Printing.PreviewPageCountType
            /* 0x028A */ "PrintDocumentFormat", // Windows.UI.Xaml.Printing.PrintDocumentFormat
            /* 0x028B */ "RelativeSourceMode", // Windows.UI.Xaml.Data.RelativeSourceMode
            /* 0x028C */ "RowOrColumnMajor", // Windows.UI.Xaml.Automation.RowOrColumnMajor
            /* 0x028D */ "ScrollAmount", // Windows.UI.Xaml.Automation.ScrollAmount
            /* 0x028E */ "ScrollBarVisibility", // Windows.UI.Xaml.Controls.ScrollBarVisibility
            /* 0x028F */ "ScrollEventType", // Windows.UI.Xaml.Controls.Primitives.ScrollEventType
            /* 0x0290 */ "ScrollingIndicatorMode", // Windows.UI.Xaml.Controls.Primitives.ScrollingIndicatorMode
            /* 0x0291 */ "ScrollIntoViewAlignment", // Windows.UI.Xaml.Controls.ScrollIntoViewAlignment
            /* 0x0292 */ "ScrollMode", // Windows.UI.Xaml.Controls.ScrollMode
            /* 0x0293 */ "SelectionMode", // Windows.UI.Xaml.Controls.SelectionMode
            /* 0x0294 */ "SliderSnapsTo", // Windows.UI.Xaml.Controls.Primitives.SliderSnapsTo
            /* 0x0295 */ "SnapPointsAlignment", // Windows.UI.Xaml.Controls.Primitives.SnapPointsAlignment
            /* 0x0296 */ "SnapPointsType", // Windows.UI.Xaml.Controls.SnapPointsType
            /* 0x0297 */ "Stereo3DVideoPackingMode", // Windows.UI.Xaml.Media.Stereo3DVideoPackingMode
            /* 0x0298 */ "Stereo3DVideoRenderMode", // Windows.UI.Xaml.Media.Stereo3DVideoRenderMode
            /* 0x0299 */ "Stretch", // Windows.UI.Xaml.Media.Stretch
            /* 0x029A */ "StretchDirection", // Windows.UI.Xaml.Controls.StretchDirection
            /* 0x029B */ "StyleSimulations", // Windows.UI.Xaml.Media.StyleSimulations
            /* 0x029C */ "SupportedTextSelection", // Windows.UI.Xaml.Automation.SupportedTextSelection
            /* 0x029D */ "SweepDirection", // Windows.UI.Xaml.Media.SweepDirection
            /* 0x029E */ "Symbol", // Windows.UI.Xaml.Controls.Symbol
            /* 0x029F */ "SynchronizedInputType", // Windows.UI.Xaml.Automation.SynchronizedInputType
            /* 0x02A0 */ "TextAlignment", // Windows.UI.Xaml.TextAlignment
            /* 0x02A1 */ "TextFormattingMode", // Windows.UI.Xaml.Media.TextFormattingMode
            /* 0x02A2 */ "TextHintingMode", // Windows.UI.Xaml.Media.TextHintingMode
            /* 0x02A3 */ "TextLineBounds", // Windows.UI.Xaml.TextLineBounds
            /* 0x02A4 */ "TextReadingOrder", // Windows.UI.Xaml.TextReadingOrder
            /* 0x02A5 */ "TextRenderingMode", // Windows.UI.Xaml.Media.TextRenderingMode
            /* 0x02A6 */ "TextTrimming", // Windows.UI.Xaml.TextTrimming
            /* 0x02A7 */ "TextWrapping", // Windows.UI.Xaml.TextWrapping
            /* 0x02A8 */ "TickPlacement", // Windows.UI.Xaml.Controls.Primitives.TickPlacement
            /* 0x02A9 */ "ToggleState", // Windows.UI.Xaml.Automation.ToggleState
            /* 0x02AA */ "TypeKind", // Windows.UI.Xaml.Interop.TypeKind
            /* 0x02AB */ "UpdateSourceTrigger", // Windows.UI.Xaml.Data.UpdateSourceTrigger
            /* 0x02AC */ "VerticalAlignment", // Windows.UI.Xaml.VerticalAlignment
            /* 0x02AD */ "VirtualizationMode", // Windows.UI.Xaml.Controls.VirtualizationMode
            /* 0x02AE */ "VirtualKey", // Windows.System.VirtualKey
            /* 0x02AF */ "VirtualKeyModifiers", // Windows.System.VirtualKeyModifiers
            /* 0x02B0 */ "Visibility", // Windows.UI.Xaml.Visibility
            /* 0x02B1 */ "WindowInteractionState", // Windows.UI.Xaml.Automation.WindowInteractionState
            /* 0x02B2 */ "WindowVisualState", // Windows.UI.Xaml.Automation.WindowVisualState
            /* 0x02B3 */ "ZoomMode", // Windows.UI.Xaml.Controls.ZoomMode
            /* 0x02B4 */ "ZoomUnit", // Windows.UI.Xaml.Automation.ZoomUnit
            /* 0x02B5 */ null,
            /* 0x02B6 */ null,
            /* 0x02B7 */ null,
            /* 0x02B8 */ null,
            /* 0x02B9 */ null,
            /* 0x02BA */ null,
            /* 0x02BB */ null,
            /* 0x02BC */ null,
            /* 0x02BD */ "StoryboardCollection", // Windows.UI.Xaml.Internal.StoryboardCollection
            /* 0x02BE */ "PluggableLayoutPanel", // Windows.UI.Xaml.Controls.PluggableLayoutPanel
            /* 0x02BF */ "AutomationNavigationDirection", // Windows.UI.Xaml.Automation.Peers.AutomationNavigationDirection
            /* 0x02C0 */ null,
            /* 0x02C1 */ null,
            /* 0x02C2 */ "CalendarViewTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x02C3 */ "CalendarView", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x02C4 */ "CalendarViewBaseItem", // Windows.UI.Xaml.Controls.Primitives.CalendarViewBaseItem
            /* 0x02C5 */ "CalendarViewDayItem", // Windows.UI.Xaml.Controls.CalendarViewDayItem
            /* 0x02C6 */ "CalendarViewItem", // Windows.UI.Xaml.Controls.Primitives.CalendarViewItem
            /* 0x02C7 */ "CalendarViewDisplayMode", // Windows.UI.Xaml.Controls.CalendarViewDisplayMode
            /* 0x02C8 */ "CalendarViewSelectionMode", // Windows.UI.Xaml.Controls.CalendarViewSelectionMode
            /* 0x02C9 */ "DayOfWeek", // Windows.Globalization.DayOfWeek
            /* 0x02CA */ "TileGrid", // Windows.UI.Xaml.Controls.TileGrid
            /* 0x02CB */ "TileGridNestedPanel", // Windows.UI.Xaml.Controls.TileGridNestedPanel
            /* 0x02CC */ "DataPackageOperation", // Windows.ApplicationModel.DataTransfer.DataPackageOperation
            /* 0x02CD */ null,
            /* 0x02CE */ null,
            /* 0x02CF */ null,
            /* 0x02D0 */ null,
            /* 0x02D1 */ null,
            /* 0x02D2 */ null,
            /* 0x02D3 */ "CalendarPanel", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x02D4 */ null,
            /* 0x02D5 */ null,
            /* 0x02D6 */ null,
            /* 0x02D7 */ "SplitViewTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x02D8 */ "SplitView", // Windows.UI.Xaml.Controls.SplitView
            /* 0x02D9 */ "SplitViewDisplayMode", // Windows.UI.Xaml.Controls.SplitViewDisplayMode
            /* 0x02DA */ "SplitViewPanePlacement", // Windows.UI.Xaml.Controls.SplitViewPanePlacement
            /* 0x02DB */ "Transform3D", // Windows.UI.Xaml.Media.Media3D.Transform3D
            /* 0x02DC */ "CompositeTransform3D", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x02DD */ "PerspectiveTransform3D", // Windows.UI.Xaml.Media.Media3D.PerspectiveTransform3D
            /* 0x02DE */ "AutomationActiveEnd", // Windows.UI.Xaml.Automation.AutomationActiveEnd
            /* 0x02DF */ "AutomationAnimationStyle", // Windows.UI.Xaml.Automation.AutomationAnimationStyle
            /* 0x02E0 */ "AutomationBulletStyle", // Windows.UI.Xaml.Automation.AutomationBulletStyle
            /* 0x02E1 */ "AutomationCaretBidiMode", // Windows.UI.Xaml.Automation.AutomationCaretBidiMode
            /* 0x02E2 */ "AutomationCaretPosition", // Windows.UI.Xaml.Automation.AutomationCaretPosition
            /* 0x02E3 */ "AutomationFlowDirections", // Windows.UI.Xaml.Automation.AutomationFlowDirections
            /* 0x02E4 */ "AutomationOutlineStyles", // Windows.UI.Xaml.Automation.AutomationOutlineStyles
            /* 0x02E5 */ "AutomationStyleId", // Windows.UI.Xaml.Automation.AutomationStyleId
            /* 0x02E6 */ "AutomationTextDecorationLineStyle", // Windows.UI.Xaml.Automation.AutomationTextDecorationLineStyle
            /* 0x02E7 */ "AutomationTextEditChangeType", // Windows.UI.Xaml.Automation.AutomationTextEditChangeType
            /* 0x02E8 */ "RelativePanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x02E9 */ null,
            /* 0x02EA */ "DeferredElement", // Windows.UI.Xaml.Internal.DeferredElement
            /* 0x02EB */ "HWCompInkCanvasNode", // Windows.UI.Xaml.Internal.HWCompInkCanvasNode
            /* 0x02EC */ "InkCanvas", // Windows.UI.Xaml.Controls.InkCanvas
            /* 0x02ED */ "MenuFlyoutSubItem", // Windows.UI.Xaml.Controls.MenuFlyoutSubItem
            /* 0x02EE */ "AutomationStructureChangeType", // Windows.UI.Xaml.Automation.Peers.AutomationStructureChangeType
            /* 0x02EF */ "PasswordRevealMode", // Windows.UI.Xaml.Controls.PasswordRevealMode
            /* 0x02F0 */ null,
            /* 0x02F1 */ "FailedMediaStreamKind", // Windows.Media.Playback.FailedMediaStreamKind
            /* 0x02F2 */ null,
            /* 0x02F3 */ "TargetPropertyPath", // Windows.UI.Xaml.TargetPropertyPath
            /* 0x02F4 */ null,
            /* 0x02F5 */ "AdaptiveTrigger", // Windows.UI.Xaml.AdaptiveTrigger
            /* 0x02F6 */ "StateTriggerCollection", // Windows.UI.Xaml.Internal.StateTriggerCollection
            /* 0x02F7 */ "HWWindowedPopupCompTreeNode", // Windows.UI.Xaml.Internal.HWWindowedPopupCompTreeNode
            /* 0x02F8 */ "ListViewItemPresenterCheckMode", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenterCheckMode
            /* 0x02F9 */ "SoftwareBitmapSource", // Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource
            /* 0x02FA */ null,
            /* 0x02FB */ null,
            /* 0x02FC */ "StateTriggerBase", // Windows.UI.Xaml.StateTriggerBase
            /* 0x02FD */ null,
            /* 0x02FE */ "MenuPopupThemeTransition", // Windows.UI.Xaml.Media.Animation.MenuPopupThemeTransition
            /* 0x02FF */ "StateTrigger", // Windows.UI.Xaml.StateTrigger
            /* 0x0300 */ "WebViewExecutionMode", // Windows.UI.Xaml.Controls.WebViewExecutionMode
            /* 0x0301 */ "WebViewSettings", // Windows.UI.Xaml.Controls.WebViewSettings
            /* 0x0302 */ "WebViewPermissionState", // Windows.UI.Xaml.Controls.WebViewPermissionState
            /* 0x0303 */ "WebViewPermissionType", // Windows.UI.Xaml.Controls.WebViewPermissionType
            /* 0x0304 */ "PickerFlyoutThemeTransition", // Windows.UI.Xaml.Media.Animation.PickerFlyoutThemeTransition
            /* 0x0305 */ "CandidateWindowAlignment", // Windows.UI.Xaml.Controls.CandidateWindowAlignment
            /* 0x0306 */ "CalendarDatePicker", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0307 */ "ContentDialogOpenCloseThemeTransition", // Windows.UI.Xaml.Media.Animation.ContentDialogOpenCloseThemeTransition
            /* 0x0308 */ "ElementCompositionPreview", // Windows.UI.Xaml.Hosting.ElementCompositionPreview
            /* 0x0309 */ "MediaTransportControlsHelper", // Windows.UI.Xaml.Controls.MediaTransportControlsHelper
            /* 0x030A */ "AutoSuggestBoxQuerySubmittedEventArgs", // Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs
            /* 0x030B */ "AppBarTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x030C */ "CommandBarTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x030D */ "CommandBarOverflowPresenter", // Windows.UI.Xaml.Controls.CommandBarOverflowPresenter
            /* 0x030E */ "DrillInThemeAnimation", // Windows.UI.Xaml.Media.Animation.DrillInThemeAnimation
            /* 0x030F */ "DrillOutThemeAnimation", // Windows.UI.Xaml.Media.Animation.DrillOutThemeAnimation
            /* 0x0310 */ "CalendarViewAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarViewAutomationPeer
            /* 0x0311 */ "CalendarViewBaseItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarViewBaseItemAutomationPeer
            /* 0x0312 */ "CalendarViewDayItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarViewDayItemAutomationPeer
            /* 0x0313 */ "CalendarViewItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarViewItemAutomationPeer
            /* 0x0314 */ "XamlBindingHelper", // Windows.UI.Xaml.Markup.XamlBindingHelper
            /* 0x0315 */ "AutomationAnnotation", // Windows.UI.Xaml.Automation.AutomationAnnotation
            /* 0x0316 */ "AutomationPeerAnnotation", // Windows.UI.Xaml.Automation.Peers.AutomationPeerAnnotation
            /* 0x0317 */ null,
            /* 0x0318 */ null,
            /* 0x0319 */ "AutomationAnnotationCollection", // Windows.UI.Xaml.Automation.AutomationAnnotationCollection
            /* 0x031A */ "AutomationPeerAnnotationCollection", // Windows.UI.Xaml.Automation.Peers.AutomationPeerAnnotationCollection
            /* 0x031B */ "MenuFlyoutSubItemAutomationPeer", // Windows.UI.Xaml.Automation.Peers.MenuFlyoutSubItemAutomationPeer
            /* 0x031C */ "SplitViewPaneAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SplitViewPaneAutomationPeer
            /* 0x031D */ "UnderlineStyle", // Windows.UI.Xaml.Documents.UnderlineStyle
            /* 0x031E */ "SplitViewLightDismissAutomationPeer", // Windows.UI.Xaml.Automation.Peers.SplitViewLightDismissAutomationPeer
            /* 0x031F */ "RichEditClipboardFormat", // Windows.UI.Xaml.Controls.RichEditClipboardFormat
            /* 0x0320 */ null,
            /* 0x0321 */ "MenuFlyoutPresenterTemplateSettings", // Windows.UI.Xaml.Controls.Primitives.MenuFlyoutPresenterTemplateSettings
            /* 0x0322 */ null,
            /* 0x0323 */ "LandmarkTargetAutomationPeer", // Windows.UI.Xaml.Automation.Peers.LandmarkTargetAutomationPeer
            /* 0x0324 */ "AutomationLandmarkType", // Windows.UI.Xaml.Automation.Peers.AutomationLandmarkType
            /* 0x0325 */ "CalendarScrollViewerAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarScrollViewerAutomationPeer
            /* 0x0326 */ "CalendarDatePickerAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarDatePickerAutomationPeer
            /* 0x0327 */ null,
            /* 0x0328 */ null,
            /* 0x0329 */ null,
            /* 0x032A */ "CommandBarLabelPosition", // Windows.UI.Xaml.Controls.CommandBarLabelPosition
            /* 0x032B */ null,
            /* 0x032C */ "CommandBarDefaultLabelPosition", // Windows.UI.Xaml.Controls.CommandBarDefaultLabelPosition
            /* 0x032D */ null,
            /* 0x032E */ "CommandBarOverflowButtonVisibility", // Windows.UI.Xaml.Controls.CommandBarOverflowButtonVisibility
            /* 0x032F */ "HWRedirectedCompTreeNodeDComp", // Windows.UI.Xaml.Internal.HWRedirectedCompTreeNodeDComp
            /* 0x0330 */ "HWRedirectedCompTreeNodeWinRT", // Windows.UI.Xaml.Internal.HWRedirectedCompTreeNodeWinRT
            /* 0x0331 */ "HWWindowedPopupCompTreeNodeDComp", // Windows.UI.Xaml.Internal.HWWindowedPopupCompTreeNodeDComp
            /* 0x0332 */ "HWWindowedPopupCompTreeNodeWinRT", // Windows.UI.Xaml.Internal.HWWindowedPopupCompTreeNodeWinRT
            /* 0x0333 */ "CommandBarDynamicOverflowAction", // Windows.UI.Xaml.Controls.CommandBarDynamicOverflowAction
            /* 0x0334 */ "ConnectedAnimation", // Windows.UI.Xaml.Media.Animation.ConnectedAnimation
            /* 0x0335 */ "ConnectedAnimationService", // Windows.UI.Xaml.Media.Animation.ConnectedAnimationService
            /* 0x0336 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.LightDismissOverlayMode
            /* 0x0337 */ "FocusVisualKind", // Windows.UI.Xaml.FocusVisualKind
            /* 0x0338 */ "RequiresPointer", // Windows.UI.Xaml.Controls.RequiresPointer
            /* 0x0339 */ "ConnectedAnimationRoot", // Windows.UI.Xaml.ConnectedAnimationRoot
            /* 0x033A */ "MediaPlayer", // Windows.Media.Playback.MediaPlayer
            /* 0x033B */ "MediaPlayerElementAutomationPeer", // Windows.UI.Xaml.Automation.Peers.MediaPlayerElementAutomationPeer
            /* 0x033C */ "MediaPlayerPresenter", // Windows.UI.Xaml.Controls.MediaPlayerPresenter
            /* 0x033D */ "MediaPlayerElement", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x033E */ "FastPlayFallbackBehaviour", // Windows.UI.Xaml.Media.FastPlayFallbackBehaviour
            /* 0x033F */ "ElementSoundKind", // Windows.UI.Xaml.ElementSoundKind
            /* 0x0340 */ "ElementSoundMode", // Windows.UI.Xaml.ElementSoundMode
            /* 0x0341 */ "ElementSoundPlayerState", // Windows.UI.Xaml.ElementSoundPlayerState
            /* 0x0342 */ "FullWindowMediaRootAutomationPeer", // Windows.UI.Xaml.FullWindowMediaRootAutomationPeer
            /* 0x0343 */ "ApplicationRequiresPointerMode", // Windows.UI.Xaml.ApplicationRequiresPointerMode
            /* 0x0344 */ "MediaPlaybackItemConverter", // Windows.UI.Xaml.Internal.MediaPlaybackItemConverter
            /* 0x0345 */ null,
            /* 0x0346 */ "BrushCollection", // Windows.UI.Xaml.Media.BrushCollection
            /* 0x0347 */ "CalendarViewHeaderAutomationPeer", // Windows.UI.Xaml.Automation.Peers.CalendarViewHeaderAutomationPeer
        };

        private static readonly string[] propertyNames = {
            /* 0x0001 */ "Index", // Windows.UI.Xaml.Controls.Primitives.GeneratorPosition
            /* 0x0002 */ "Offset", // Windows.UI.Xaml.Controls.Primitives.GeneratorPosition
            /* 0x0003 */ "Kind", // Windows.UI.Xaml.Interop.TypeName
            /* 0x0004 */ "Name", // Windows.UI.Xaml.Interop.TypeName
            /* 0x0005 */ "AcceleratorKey", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0006 */ "AccessibilityView", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0007 */ "AccessKey", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0008 */ "AutomationId", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0009 */ "ControlledPeers", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000A */ "HelpText", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000B */ "IsRequiredForForm", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000C */ "ItemStatus", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000D */ "ItemType", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000E */ "LabeledBy", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x000F */ "LiveSetting", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0010 */ "Name", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0011 */ "Name", // Windows.UI.Xaml.DependencyObject
            /* 0x0012 */ "DeferredUnlinkingPayload", // Windows.UI.Xaml.Controls.ItemContainerGenerator
            /* 0x0013 */ "IsRecycledContainer", // Windows.UI.Xaml.Controls.ItemContainerGenerator
            /* 0x0014 */ "ItemForItemContainer", // Windows.UI.Xaml.Controls.ItemContainerGenerator
            /* 0x0015 */ "TextFormattingMode", // Windows.UI.Xaml.TextOptions
            /* 0x0016 */ "TextHintingMode", // Windows.UI.Xaml.TextOptions
            /* 0x0017 */ "TextRenderingMode", // Windows.UI.Xaml.TextOptions
            /* 0x0018 */ "Placement", // Windows.UI.Xaml.Controls.ToolTipService
            /* 0x0019 */ "PlacementTarget", // Windows.UI.Xaml.Controls.ToolTipService
            /* 0x001A */ "ToolTip", // Windows.UI.Xaml.Controls.ToolTipService
            /* 0x001B */ "ToolTipObject", // Windows.UI.Xaml.Controls.ToolTipService
            /* 0x001C */ "AnnotationAlternates", // Windows.UI.Xaml.Documents.Typography
            /* 0x001D */ "Capitals", // Windows.UI.Xaml.Documents.Typography
            /* 0x001E */ "CapitalSpacing", // Windows.UI.Xaml.Documents.Typography
            /* 0x001F */ "CaseSensitiveForms", // Windows.UI.Xaml.Documents.Typography
            /* 0x0020 */ "ContextualAlternates", // Windows.UI.Xaml.Documents.Typography
            /* 0x0021 */ "ContextualLigatures", // Windows.UI.Xaml.Documents.Typography
            /* 0x0022 */ "ContextualSwashes", // Windows.UI.Xaml.Documents.Typography
            /* 0x0023 */ "DiscretionaryLigatures", // Windows.UI.Xaml.Documents.Typography
            /* 0x0024 */ "EastAsianExpertForms", // Windows.UI.Xaml.Documents.Typography
            /* 0x0025 */ "EastAsianLanguage", // Windows.UI.Xaml.Documents.Typography
            /* 0x0026 */ "EastAsianWidths", // Windows.UI.Xaml.Documents.Typography
            /* 0x0027 */ "Fraction", // Windows.UI.Xaml.Documents.Typography
            /* 0x0028 */ "HistoricalForms", // Windows.UI.Xaml.Documents.Typography
            /* 0x0029 */ "HistoricalLigatures", // Windows.UI.Xaml.Documents.Typography
            /* 0x002A */ "Kerning", // Windows.UI.Xaml.Documents.Typography
            /* 0x002B */ "MathematicalGreek", // Windows.UI.Xaml.Documents.Typography
            /* 0x002C */ "NumeralAlignment", // Windows.UI.Xaml.Documents.Typography
            /* 0x002D */ "NumeralStyle", // Windows.UI.Xaml.Documents.Typography
            /* 0x002E */ "SlashedZero", // Windows.UI.Xaml.Documents.Typography
            /* 0x002F */ "StandardLigatures", // Windows.UI.Xaml.Documents.Typography
            /* 0x0030 */ "StandardSwashes", // Windows.UI.Xaml.Documents.Typography
            /* 0x0031 */ "StylisticAlternates", // Windows.UI.Xaml.Documents.Typography
            /* 0x0032 */ "StylisticSet1", // Windows.UI.Xaml.Documents.Typography
            /* 0x0033 */ "StylisticSet10", // Windows.UI.Xaml.Documents.Typography
            /* 0x0034 */ "StylisticSet11", // Windows.UI.Xaml.Documents.Typography
            /* 0x0035 */ "StylisticSet12", // Windows.UI.Xaml.Documents.Typography
            /* 0x0036 */ "StylisticSet13", // Windows.UI.Xaml.Documents.Typography
            /* 0x0037 */ "StylisticSet14", // Windows.UI.Xaml.Documents.Typography
            /* 0x0038 */ "StylisticSet15", // Windows.UI.Xaml.Documents.Typography
            /* 0x0039 */ "StylisticSet16", // Windows.UI.Xaml.Documents.Typography
            /* 0x003A */ "StylisticSet17", // Windows.UI.Xaml.Documents.Typography
            /* 0x003B */ "StylisticSet18", // Windows.UI.Xaml.Documents.Typography
            /* 0x003C */ "StylisticSet19", // Windows.UI.Xaml.Documents.Typography
            /* 0x003D */ "StylisticSet2", // Windows.UI.Xaml.Documents.Typography
            /* 0x003E */ "StylisticSet20", // Windows.UI.Xaml.Documents.Typography
            /* 0x003F */ "StylisticSet3", // Windows.UI.Xaml.Documents.Typography
            /* 0x0040 */ "StylisticSet4", // Windows.UI.Xaml.Documents.Typography
            /* 0x0041 */ "StylisticSet5", // Windows.UI.Xaml.Documents.Typography
            /* 0x0042 */ "StylisticSet6", // Windows.UI.Xaml.Documents.Typography
            /* 0x0043 */ "StylisticSet7", // Windows.UI.Xaml.Documents.Typography
            /* 0x0044 */ "StylisticSet8", // Windows.UI.Xaml.Documents.Typography
            /* 0x0045 */ "StylisticSet9", // Windows.UI.Xaml.Documents.Typography
            /* 0x0046 */ "Variants", // Windows.UI.Xaml.Documents.Typography
            /* 0x0047 */ "ApplicationStarted", // Windows.UI.Xaml.Application
            /* 0x0048 */ "RequestedTheme", // Windows.UI.Xaml.Application
            /* 0x0049 */ "Resources", // Windows.UI.Xaml.Application
            /* 0x004A */ "RootVisual", // Windows.UI.Xaml.Application
            /* 0x004B */ "EventsSource", // Windows.UI.Xaml.Automation.Peers.AutomationPeer
            /* 0x004C */ "SelectedItem", // Windows.UI.Xaml.Controls.AutoSuggestBoxSuggestionChosenEventArgs
            /* 0x004D */ "Reason", // Windows.UI.Xaml.Controls.AutoSuggestBoxTextChangedEventArgs
            /* 0x004E */ "Opacity", // Windows.UI.Xaml.Media.Brush
            /* 0x004F */ "RelativeTransform", // Windows.UI.Xaml.Media.Brush
            /* 0x0050 */ "Transform", // Windows.UI.Xaml.Media.Brush
            /* 0x0051 */ "IsSourceGrouped", // Windows.UI.Xaml.Data.CollectionViewSource
            /* 0x0052 */ "ItemsPath", // Windows.UI.Xaml.Data.CollectionViewSource
            /* 0x0053 */ "Source", // Windows.UI.Xaml.Data.CollectionViewSource
            /* 0x0054 */ "View", // Windows.UI.Xaml.Data.CollectionViewSource
            /* 0x0055 */ "A", // Windows.UI.Color
            /* 0x0056 */ "B", // Windows.UI.Color
            /* 0x0057 */ "ContentProperty", // Windows.UI.Color
            /* 0x0058 */ "G", // Windows.UI.Color
            /* 0x0059 */ "R", // Windows.UI.Color
            /* 0x005A */ "KeyTime", // Windows.UI.Xaml.Media.Animation.ColorKeyFrame
            /* 0x005B */ "Value", // Windows.UI.Xaml.Media.Animation.ColorKeyFrame
            /* 0x005C */ "ActualWidth", // Windows.UI.Xaml.Controls.ColumnDefinition
            /* 0x005D */ "MaxWidth", // Windows.UI.Xaml.Controls.ColumnDefinition
            /* 0x005E */ "MinWidth", // Windows.UI.Xaml.Controls.ColumnDefinition
            /* 0x005F */ "Width", // Windows.UI.Xaml.Controls.ColumnDefinition
            /* 0x0060 */ "DropDownClosedHeight", // Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings
            /* 0x0061 */ "DropDownOffset", // Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings
            /* 0x0062 */ "DropDownOpenedHeight", // Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings
            /* 0x0063 */ "SelectedItemDirection", // Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings
            /* 0x0064 */ "BottomLeft", // Windows.UI.Xaml.CornerRadius
            /* 0x0065 */ "BottomRight", // Windows.UI.Xaml.CornerRadius
            /* 0x0066 */ "TopLeft", // Windows.UI.Xaml.CornerRadius
            /* 0x0067 */ "TopRight", // Windows.UI.Xaml.CornerRadius
            /* 0x0068 */ null,
            /* 0x0069 */ "PropertyId", // Windows.UI.Xaml.DependencyProperty
            /* 0x006A */ "ContentProperty", // Windows.Foundation.Double
            /* 0x006B */ "KeyTime", // Windows.UI.Xaml.Media.Animation.DoubleKeyFrame
            /* 0x006C */ "Value", // Windows.UI.Xaml.Media.Animation.DoubleKeyFrame
            /* 0x006D */ null,
            /* 0x006E */ null,
            /* 0x006F */ "EasingMode", // Windows.UI.Xaml.Media.Animation.EasingFunctionBase
            /* 0x0070 */ "MarkupExtensionType", // Windows.UI.Xaml.Internal.ExternalObjectReference
            /* 0x0071 */ "NativeValue", // Windows.UI.Xaml.Internal.ExternalObjectReference
            /* 0x0072 */ "AttachedFlyout", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x0073 */ "Placement", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x0074 */ "Weight", // Windows.UI.Text.FontWeight
            /* 0x0075 */ "Template", // Windows.UI.Xaml.FrameworkTemplate
            /* 0x0076 */ "Bounds", // Windows.UI.Xaml.Media.Geometry
            /* 0x0077 */ "Transform", // Windows.UI.Xaml.Media.Geometry
            /* 0x0078 */ "Color", // Windows.UI.Xaml.Media.GradientStop
            /* 0x0079 */ "Offset", // Windows.UI.Xaml.Media.GradientStop
            /* 0x007A */ "GridUnitType", // Windows.UI.Xaml.GridLength
            /* 0x007B */ "Value", // Windows.UI.Xaml.GridLength
            /* 0x007C */ "ContainerStyle", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x007D */ "ContainerStyleSelector", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x007E */ "HeaderContainerStyle", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x007F */ "HeaderTemplate", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x0080 */ "HeaderTemplateSelector", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x0081 */ "HidesIfEmpty", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x0082 */ "Panel", // Windows.UI.Xaml.Controls.GroupStyle
            /* 0x0083 */ "CandidateFontSize", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0084 */ "CandidateIndex", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0085 */ "CandidateMargin", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0086 */ "CandidateString", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0087 */ "KeyboardShortcut", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0088 */ "Metadata", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x0089 */ "MetadataVisibility", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x008A */ "SecondaryFontSize", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x008B */ "ShortcutOpacity", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x008C */ "ShortcutVisibility", // Windows.UI.Xaml.Controls.IMECandidateItem
            /* 0x008D */ "Items", // Windows.UI.Xaml.Controls.IMECandidatePage
            /* 0x008E */ "PageIndex", // Windows.UI.Xaml.Controls.IMECandidatePage
            /* 0x008F */ "Width", // Windows.UI.Xaml.Controls.IMECandidatePage
            /* 0x0090 */ "DesiredDeceleration", // Windows.UI.Xaml.Input.InertiaExpansionBehavior
            /* 0x0091 */ "DesiredExpansion", // Windows.UI.Xaml.Input.InertiaExpansionBehavior
            /* 0x0092 */ "DesiredDeceleration", // Windows.UI.Xaml.Input.InertiaRotationBehavior
            /* 0x0093 */ "DesiredRotation", // Windows.UI.Xaml.Input.InertiaRotationBehavior
            /* 0x0094 */ "DesiredDeceleration", // Windows.UI.Xaml.Input.InertiaTranslationBehavior
            /* 0x0095 */ "DesiredDisplacement", // Windows.UI.Xaml.Input.InertiaTranslationBehavior
            /* 0x0096 */ "Names", // Windows.UI.Xaml.Input.InputScope
            /* 0x0097 */ "NameValue", // Windows.UI.Xaml.Input.InputScopeName
            /* 0x0098 */ "ContentProperty", // Windows.Foundation.Int32
            /* 0x0099 */ "ControlPoint1", // Windows.UI.Xaml.Media.Animation.KeySpline
            /* 0x009A */ "ControlPoint2", // Windows.UI.Xaml.Media.Animation.KeySpline
            /* 0x009B */ "Bounds", // Windows.UI.Xaml.LayoutTransitionStaggerItem
            /* 0x009C */ "Element", // Windows.UI.Xaml.LayoutTransitionStaggerItem
            /* 0x009D */ "Index", // Windows.UI.Xaml.LayoutTransitionStaggerItem
            /* 0x009E */ "StaggerTime", // Windows.UI.Xaml.LayoutTransitionStaggerItem
            /* 0x009F */ "Center", // Windows.UI.Xaml.Input.ManipulationPivot
            /* 0x00A0 */ "Radius", // Windows.UI.Xaml.Input.ManipulationPivot
            /* 0x00A1 */ "M11", // Windows.UI.Xaml.Media.Matrix
            /* 0x00A2 */ "M12", // Windows.UI.Xaml.Media.Matrix
            /* 0x00A3 */ "M21", // Windows.UI.Xaml.Media.Matrix
            /* 0x00A4 */ "M22", // Windows.UI.Xaml.Media.Matrix
            /* 0x00A5 */ "OffsetX", // Windows.UI.Xaml.Media.Matrix
            /* 0x00A6 */ "OffsetY", // Windows.UI.Xaml.Media.Matrix
            /* 0x00A7 */ "M11", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00A8 */ "M12", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00A9 */ "M13", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00AA */ "M14", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00AB */ "M21", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00AC */ "M22", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00AD */ "M23", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00AE */ "M24", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00AF */ "M31", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B0 */ "M32", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B1 */ "M33", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B2 */ "M34", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B3 */ "M44", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B4 */ "OffsetX", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B5 */ "OffsetY", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B6 */ "OffsetZ", // Windows.UI.Xaml.Media.Media3D.Matrix3D
            /* 0x00B7 */ "KeyTime", // Windows.UI.Xaml.Media.Animation.ObjectKeyFrame
            /* 0x00B8 */ "Value", // Windows.UI.Xaml.Media.Animation.ObjectKeyFrame
            /* 0x00B9 */ "SourcePageType", // Windows.UI.Xaml.Navigation.PageStackEntry
            /* 0x00BA */ "CurveSegments", // Windows.UI.Xaml.Internal.ParametricCurve
            /* 0x00BB */ "BeginOffset", // Windows.UI.Xaml.Internal.ParametricCurveSegment
            /* 0x00BC */ "ConstantCoefficient", // Windows.UI.Xaml.Internal.ParametricCurveSegment
            /* 0x00BD */ "CubicCoefficient", // Windows.UI.Xaml.Internal.ParametricCurveSegment
            /* 0x00BE */ "LinearCoefficient", // Windows.UI.Xaml.Internal.ParametricCurveSegment
            /* 0x00BF */ "QuadraticCoefficient", // Windows.UI.Xaml.Internal.ParametricCurveSegment
            /* 0x00C0 */ "IsClosed", // Windows.UI.Xaml.Media.PathFigure
            /* 0x00C1 */ "IsFilled", // Windows.UI.Xaml.Media.PathFigure
            /* 0x00C2 */ "Segments", // Windows.UI.Xaml.Media.PathFigure
            /* 0x00C3 */ "StartPoint", // Windows.UI.Xaml.Media.PathFigure
            /* 0x00C4 */ "ContentProperty", // Windows.Foundation.Point
            /* 0x00C5 */ "X", // Windows.Foundation.Point
            /* 0x00C6 */ "Y", // Windows.Foundation.Point
            /* 0x00C7 */ "IsInContact", // Windows.UI.Xaml.Input.Pointer
            /* 0x00C8 */ "IsInRange", // Windows.UI.Xaml.Input.Pointer
            /* 0x00C9 */ "PointerDeviceType", // Windows.UI.Xaml.Input.Pointer
            /* 0x00CA */ "PointerId", // Windows.UI.Xaml.Input.Pointer
            /* 0x00CB */ "PointerValue", // Windows.UI.Xaml.Internal.PointerKeyFrame
            /* 0x00CC */ "TargetValue", // Windows.UI.Xaml.Internal.PointerKeyFrame
            /* 0x00CD */ "KeyTime", // Windows.UI.Xaml.Media.Animation.PointKeyFrame
            /* 0x00CE */ "Value", // Windows.UI.Xaml.Media.Animation.PointKeyFrame
            /* 0x00CF */ "Count", // Windows.UI.Xaml.Collections.PresentationFrameworkCollection
            /* 0x00D0 */ "DesiredFormat", // Windows.UI.Xaml.Printing.PrintDocument
            /* 0x00D1 */ "DocumentSource", // Windows.UI.Xaml.Printing.PrintDocument
            /* 0x00D2 */ "PrintedPageCount", // Windows.UI.Xaml.Printing.PrintDocument
            /* 0x00D3 */ "ContainerAnimationEndPosition", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00D4 */ "ContainerAnimationStartPosition", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00D5 */ "EllipseAnimationEndPosition", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00D6 */ "EllipseAnimationWellPosition", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00D7 */ "EllipseDiameter", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00D8 */ "EllipseOffset", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00D9 */ "IndicatorLengthDelta", // Windows.UI.Xaml.Controls.Primitives.ProgressBarTemplateSettings
            /* 0x00DA */ "EllipseDiameter", // Windows.UI.Xaml.Controls.Primitives.ProgressRingTemplateSettings
            /* 0x00DB */ "EllipseOffset", // Windows.UI.Xaml.Controls.Primitives.ProgressRingTemplateSettings
            /* 0x00DC */ "MaxSideLength", // Windows.UI.Xaml.Controls.Primitives.ProgressRingTemplateSettings
            /* 0x00DD */ "Path", // Windows.UI.Xaml.PropertyPath
            /* 0x00DE */ "Height", // Windows.Foundation.Rect
            /* 0x00DF */ "Width", // Windows.Foundation.Rect
            /* 0x00E0 */ "X", // Windows.Foundation.Rect
            /* 0x00E1 */ "Y", // Windows.Foundation.Rect
            /* 0x00E2 */ "ActualHeight", // Windows.UI.Xaml.Controls.RowDefinition
            /* 0x00E3 */ "Height", // Windows.UI.Xaml.Controls.RowDefinition
            /* 0x00E4 */ "MaxHeight", // Windows.UI.Xaml.Controls.RowDefinition
            /* 0x00E5 */ "MinHeight", // Windows.UI.Xaml.Controls.RowDefinition
            /* 0x00E6 */ "Curves", // Windows.UI.Xaml.Internal.SecondaryContentRelationship
            /* 0x00E7 */ "IsDescendant", // Windows.UI.Xaml.Internal.SecondaryContentRelationship
            /* 0x00E8 */ "ShouldTargetClip", // Windows.UI.Xaml.Internal.SecondaryContentRelationship
            /* 0x00E9 */ "IsSealed", // Windows.UI.Xaml.SetterBase
            /* 0x00EA */ "BorderBrush", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x00EB */ "BorderThickness", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x00EC */ "ContentTransitions", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x00ED */ "HeaderBackground", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x00EE */ "HeaderForeground", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x00EF */ "IconSource", // Windows.UI.Xaml.Controls.Primitives.SettingsFlyoutTemplateSettings
            /* 0x00F0 */ "Height", // Windows.Foundation.Size
            /* 0x00F1 */ "Width", // Windows.Foundation.Size
            /* 0x00F2 */ "Color", // Windows.UI.Xaml.Internal.SolidColorBrushClone
            /* 0x00F3 */ "ContentProperty", // Windows.Foundation.String
            /* 0x00F4 */ "BasedOn", // Windows.UI.Xaml.Style
            /* 0x00F5 */ "IsSealed", // Windows.UI.Xaml.Style
            /* 0x00F6 */ "Setters", // Windows.UI.Xaml.Style
            /* 0x00F7 */ "TargetType", // Windows.UI.Xaml.Style
            /* 0x00F8 */ "Owner", // Windows.UI.Xaml.Automation.Peers.TextAdapter
            /* 0x00F9 */ "CharacterSpacing", // Windows.UI.Xaml.Documents.TextElement
            /* 0x00FA */ "FontFamily", // Windows.UI.Xaml.Documents.TextElement
            /* 0x00FB */ "FontSize", // Windows.UI.Xaml.Documents.TextElement
            /* 0x00FC */ "FontStretch", // Windows.UI.Xaml.Documents.TextElement
            /* 0x00FD */ "FontStyle", // Windows.UI.Xaml.Documents.TextElement
            /* 0x00FE */ "FontWeight", // Windows.UI.Xaml.Documents.TextElement
            /* 0x00FF */ "Foreground", // Windows.UI.Xaml.Documents.TextElement
            /* 0x0100 */ "IsTextScaleFactorEnabled", // Windows.UI.Xaml.Documents.TextElement
            /* 0x0101 */ "Language", // Windows.UI.Xaml.Documents.TextElement
            /* 0x0102 */ "Owner", // Windows.UI.Xaml.Automation.Peers.TextRangeAdapter
            /* 0x0103 */ "Bottom", // Windows.UI.Xaml.Thickness
            /* 0x0104 */ "Left", // Windows.UI.Xaml.Thickness
            /* 0x0105 */ "Right", // Windows.UI.Xaml.Thickness
            /* 0x0106 */ "Top", // Windows.UI.Xaml.Thickness
            /* 0x0107 */ "AutoReverse", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x0108 */ "BeginTime", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x0109 */ "Duration", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x010A */ "FillBehavior", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x010B */ "RepeatBehavior", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x010C */ "SpeedRatio", // Windows.UI.Xaml.Media.Animation.Timeline
            /* 0x010D */ "Text", // Windows.UI.Xaml.Media.TimelineMarker
            /* 0x010E */ "Time", // Windows.UI.Xaml.Media.TimelineMarker
            /* 0x010F */ "Type", // Windows.UI.Xaml.Media.TimelineMarker
            /* 0x0110 */ "Seconds", // Windows.Foundation.TimeSpan
            /* 0x0111 */ "CurtainCurrentToOffOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0112 */ "CurtainCurrentToOnOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0113 */ "CurtainOffToOnOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0114 */ "CurtainOnToOffOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0115 */ "KnobCurrentToOffOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0116 */ "KnobCurrentToOnOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0117 */ "KnobOffToOnOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0118 */ "KnobOnToOffOffset", // Windows.UI.Xaml.Controls.Primitives.ToggleSwitchTemplateSettings
            /* 0x0119 */ "FromHorizontalOffset", // Windows.UI.Xaml.Controls.Primitives.ToolTipTemplateSettings
            /* 0x011A */ "FromVerticalOffset", // Windows.UI.Xaml.Controls.Primitives.ToolTipTemplateSettings
            /* 0x011B */ "GeneratedStaggerFunction", // Windows.UI.Xaml.Media.Animation.Transition
            /* 0x011C */ "ClipTransform", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x011D */ "ClipTransformOrigin", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x011E */ "CompositeTransform", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x011F */ "Opacity", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x0120 */ "Projection", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x0121 */ "TransformOrigin", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x0122 */ null,
            /* 0x0123 */ null,
            /* 0x0124 */ "AllowDrop", // Windows.UI.Xaml.UIElement
            /* 0x0125 */ "CacheMode", // Windows.UI.Xaml.UIElement
            /* 0x0126 */ "ChildrenInternal", // Windows.UI.Xaml.UIElement
            /* 0x0127 */ "Clip", // Windows.UI.Xaml.UIElement
            /* 0x0128 */ "CompositeMode", // Windows.UI.Xaml.UIElement
            /* 0x0129 */ "IsDoubleTapEnabled", // Windows.UI.Xaml.UIElement
            /* 0x012A */ "IsHitTestVisible", // Windows.UI.Xaml.UIElement
            /* 0x012B */ "IsHoldingEnabled", // Windows.UI.Xaml.UIElement
            /* 0x012C */ "IsRightTapEnabled", // Windows.UI.Xaml.UIElement
            /* 0x012D */ "IsTapEnabled", // Windows.UI.Xaml.UIElement
            /* 0x012E */ "ManipulationMode", // Windows.UI.Xaml.UIElement
            /* 0x012F */ "Opacity", // Windows.UI.Xaml.UIElement
            /* 0x0130 */ "PointerCaptures", // Windows.UI.Xaml.UIElement
            /* 0x0131 */ "Projection", // Windows.UI.Xaml.UIElement
            /* 0x0132 */ "RenderSize", // Windows.UI.Xaml.UIElement
            /* 0x0133 */ "RenderTransform", // Windows.UI.Xaml.UIElement
            /* 0x0134 */ "RenderTransformOrigin", // Windows.UI.Xaml.UIElement
            /* 0x0135 */ "Transitions", // Windows.UI.Xaml.UIElement
            /* 0x0136 */ "TransitionTarget", // Windows.UI.Xaml.UIElement
            /* 0x0137 */ "UseLayoutRounding", // Windows.UI.Xaml.UIElement
            /* 0x0138 */ "Visibility", // Windows.UI.Xaml.UIElement
            /* 0x0139 */ "Clip", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x013A */ "LayoutClip", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x013B */ "OffsetX", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x013C */ "OffsetY", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x013D */ "Opacity", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x013E */ "Projection", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x013F */ "Transform", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x0140 */ "TransitionTarget", // Windows.UI.Xaml.Internal.UIElementClone
            /* 0x0141 */ "__DeferredStoryboard", // Windows.UI.Xaml.VisualState
            /* 0x0142 */ "Storyboard", // Windows.UI.Xaml.VisualState
            /* 0x0143 */ "States", // Windows.UI.Xaml.VisualStateGroup
            /* 0x0144 */ "Transitions", // Windows.UI.Xaml.VisualStateGroup
            /* 0x0145 */ "CustomVisualStateManager", // Windows.UI.Xaml.VisualStateManager
            /* 0x0146 */ "VisualStateGroups", // Windows.UI.Xaml.VisualStateManager
            /* 0x0147 */ "From", // Windows.UI.Xaml.VisualTransition
            /* 0x0148 */ "GeneratedDuration", // Windows.UI.Xaml.VisualTransition
            /* 0x0149 */ "GeneratedEasingFunction", // Windows.UI.Xaml.VisualTransition
            /* 0x014A */ "Storyboard", // Windows.UI.Xaml.VisualTransition
            /* 0x014B */ "To", // Windows.UI.Xaml.VisualTransition
            /* 0x014C */ "IsLargeArc", // Windows.UI.Xaml.Media.ArcSegment
            /* 0x014D */ "Point", // Windows.UI.Xaml.Media.ArcSegment
            /* 0x014E */ "RotationAngle", // Windows.UI.Xaml.Media.ArcSegment
            /* 0x014F */ "Size", // Windows.UI.Xaml.Media.ArcSegment
            /* 0x0150 */ "SweepDirection", // Windows.UI.Xaml.Media.ArcSegment
            /* 0x0151 */ "Amplitude", // Windows.UI.Xaml.Media.Animation.BackEase
            /* 0x0152 */ "Storyboard", // Windows.UI.Xaml.Media.Animation.BeginStoryboard
            /* 0x0153 */ "Point1", // Windows.UI.Xaml.Media.BezierSegment
            /* 0x0154 */ "Point2", // Windows.UI.Xaml.Media.BezierSegment
            /* 0x0155 */ "Point3", // Windows.UI.Xaml.Media.BezierSegment
            /* 0x0156 */ "PixelHeight", // Windows.UI.Xaml.Media.Imaging.BitmapSource
            /* 0x0157 */ "PixelWidth", // Windows.UI.Xaml.Media.Imaging.BitmapSource
            /* 0x0158 */ "LineHeight", // Windows.UI.Xaml.Documents.Block
            /* 0x0159 */ "LineStackingStrategy", // Windows.UI.Xaml.Documents.Block
            /* 0x015A */ "Margin", // Windows.UI.Xaml.Documents.Block
            /* 0x015B */ "TextAlignment", // Windows.UI.Xaml.Documents.Block
            /* 0x015C */ "Bounces", // Windows.UI.Xaml.Media.Animation.BounceEase
            /* 0x015D */ "Bounciness", // Windows.UI.Xaml.Media.Animation.BounceEase
            /* 0x015E */ "By", // Windows.UI.Xaml.Media.Animation.ColorAnimation
            /* 0x015F */ "EasingFunction", // Windows.UI.Xaml.Media.Animation.ColorAnimation
            /* 0x0160 */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.ColorAnimation
            /* 0x0161 */ "From", // Windows.UI.Xaml.Media.Animation.ColorAnimation
            /* 0x0162 */ "To", // Windows.UI.Xaml.Media.Animation.ColorAnimation
            /* 0x0163 */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.ColorAnimationUsingKeyFrames
            /* 0x0164 */ "KeyFrames", // Windows.UI.Xaml.Media.Animation.ColorAnimationUsingKeyFrames
            /* 0x0165 */ "HorizontalOffset", // Windows.UI.Xaml.Media.Animation.ContentThemeTransition
            /* 0x0166 */ "VerticalOffset", // Windows.UI.Xaml.Media.Animation.ContentThemeTransition
            /* 0x0167 */ "TargetType", // Windows.UI.Xaml.Controls.ControlTemplate
            /* 0x0168 */ "ResourceKey", // Windows.UI.Xaml.CustomResource
            /* 0x0169 */ "DataType", // Windows.UI.Xaml.DataTemplate
            /* 0x016A */ "Interval", // Windows.UI.Xaml.DispatcherTimer
            /* 0x016B */ "By", // Windows.UI.Xaml.Media.Animation.DoubleAnimation
            /* 0x016C */ "EasingFunction", // Windows.UI.Xaml.Media.Animation.DoubleAnimation
            /* 0x016D */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.DoubleAnimation
            /* 0x016E */ "From", // Windows.UI.Xaml.Media.Animation.DoubleAnimation
            /* 0x016F */ "To", // Windows.UI.Xaml.Media.Animation.DoubleAnimation
            /* 0x0170 */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.DoubleAnimationUsingKeyFrames
            /* 0x0171 */ "KeyFrames", // Windows.UI.Xaml.Media.Animation.DoubleAnimationUsingKeyFrames
            /* 0x0172 */ "TimeSpan", // Windows.UI.Xaml.Duration
            /* 0x0173 */ "Children", // Windows.UI.Xaml.Media.Animation.DynamicTimeline
            /* 0x0174 */ "EasingFunction", // Windows.UI.Xaml.Media.Animation.EasingColorKeyFrame
            /* 0x0175 */ "EasingFunction", // Windows.UI.Xaml.Media.Animation.EasingDoubleKeyFrame
            /* 0x0176 */ "EasingFunction", // Windows.UI.Xaml.Media.Animation.EasingPointKeyFrame
            /* 0x0177 */ "Edge", // Windows.UI.Xaml.Media.Animation.EdgeUIThemeTransition
            /* 0x0178 */ "Oscillations", // Windows.UI.Xaml.Media.Animation.ElasticEase
            /* 0x0179 */ "Springiness", // Windows.UI.Xaml.Media.Animation.ElasticEase
            /* 0x017A */ "Center", // Windows.UI.Xaml.Media.EllipseGeometry
            /* 0x017B */ "RadiusX", // Windows.UI.Xaml.Media.EllipseGeometry
            /* 0x017C */ "RadiusY", // Windows.UI.Xaml.Media.EllipseGeometry
            /* 0x017D */ "FromHorizontalOffset", // Windows.UI.Xaml.Media.Animation.EntranceThemeTransition
            /* 0x017E */ "FromVerticalOffset", // Windows.UI.Xaml.Media.Animation.EntranceThemeTransition
            /* 0x017F */ "IsStaggeringEnabled", // Windows.UI.Xaml.Media.Animation.EntranceThemeTransition
            /* 0x0180 */ "Actions", // Windows.UI.Xaml.EventTrigger
            /* 0x0181 */ "RoutedEvent", // Windows.UI.Xaml.EventTrigger
            /* 0x0182 */ "Exponent", // Windows.UI.Xaml.Media.Animation.ExponentialEase
            /* 0x0183 */ "Content", // Windows.UI.Xaml.Controls.Flyout
            /* 0x0184 */ "FlyoutPresenterStyle", // Windows.UI.Xaml.Controls.Flyout
            /* 0x0185 */ "ActualHeight", // Windows.UI.Xaml.FrameworkElement
            /* 0x0186 */ "ActualWidth", // Windows.UI.Xaml.FrameworkElement
            /* 0x0187 */ "DataContext", // Windows.UI.Xaml.FrameworkElement
            /* 0x0188 */ "FlowDirection", // Windows.UI.Xaml.FrameworkElement
            /* 0x0189 */ "Height", // Windows.UI.Xaml.FrameworkElement
            /* 0x018A */ "HorizontalAlignment", // Windows.UI.Xaml.FrameworkElement
            /* 0x018B */ "IsTextScaleFactorEnabledInternal", // Windows.UI.Xaml.FrameworkElement
            /* 0x018C */ "Language", // Windows.UI.Xaml.FrameworkElement
            /* 0x018D */ "Margin", // Windows.UI.Xaml.FrameworkElement
            /* 0x018E */ "MaxHeight", // Windows.UI.Xaml.FrameworkElement
            /* 0x018F */ "MaxWidth", // Windows.UI.Xaml.FrameworkElement
            /* 0x0190 */ "MinHeight", // Windows.UI.Xaml.FrameworkElement
            /* 0x0191 */ "MinWidth", // Windows.UI.Xaml.FrameworkElement
            /* 0x0192 */ "Parent", // Windows.UI.Xaml.FrameworkElement
            /* 0x0193 */ "RequestedTheme", // Windows.UI.Xaml.FrameworkElement
            /* 0x0194 */ "Resources", // Windows.UI.Xaml.FrameworkElement
            /* 0x0195 */ "Style", // Windows.UI.Xaml.FrameworkElement
            /* 0x0196 */ "Tag", // Windows.UI.Xaml.FrameworkElement
            /* 0x0197 */ "Triggers", // Windows.UI.Xaml.FrameworkElement
            /* 0x0198 */ "VerticalAlignment", // Windows.UI.Xaml.FrameworkElement
            /* 0x0199 */ "Width", // Windows.UI.Xaml.FrameworkElement
            /* 0x019A */ "Owner", // Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer
            /* 0x019B */ "Children", // Windows.UI.Xaml.Media.GeometryGroup
            /* 0x019C */ "FillRule", // Windows.UI.Xaml.Media.GeometryGroup
            /* 0x019D */ "ColorInterpolationMode", // Windows.UI.Xaml.Media.GradientBrush
            /* 0x019E */ "GradientStops", // Windows.UI.Xaml.Media.GradientBrush
            /* 0x019F */ "MappingMode", // Windows.UI.Xaml.Media.GradientBrush
            /* 0x01A0 */ "SpreadMethod", // Windows.UI.Xaml.Media.GradientBrush
            /* 0x01A1 */ "DragItemsCount", // Windows.UI.Xaml.Controls.Primitives.GridViewItemTemplateSettings
            /* 0x01A2 */ "TextDecorations", // Windows.UI.Xaml.Documents.Inline
            /* 0x01A3 */ "Item", // Windows.UI.Xaml.Automation.Peers.ItemAutomationPeer
            /* 0x01A4 */ "ItemsControlAutomationPeer", // Windows.UI.Xaml.Automation.Peers.ItemAutomationPeer
            /* 0x01A5 */ "TimeSpan", // Windows.UI.Xaml.Media.Animation.KeyTime
            /* 0x01A6 */ "EndPoint", // Windows.UI.Xaml.Media.LineGeometry
            /* 0x01A7 */ "StartPoint", // Windows.UI.Xaml.Media.LineGeometry
            /* 0x01A8 */ "Point", // Windows.UI.Xaml.Media.LineSegment
            /* 0x01A9 */ "DragItemsCount", // Windows.UI.Xaml.Controls.Primitives.ListViewItemTemplateSettings
            /* 0x01AA */ "ProjectionMatrix", // Windows.UI.Xaml.Media.Matrix3DProjection
            /* 0x01AB */ "Items", // Windows.UI.Xaml.Controls.MenuFlyout
            /* 0x01AC */ "MenuFlyoutPresenterStyle", // Windows.UI.Xaml.Controls.MenuFlyout
            /* 0x01AD */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames
            /* 0x01AE */ "KeyFrames", // Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames
            /* 0x01AF */ "Edge", // Windows.UI.Xaml.Media.Animation.PaneThemeTransition
            /* 0x01B0 */ "Figures", // Windows.UI.Xaml.Media.PathGeometry
            /* 0x01B1 */ "FillRule", // Windows.UI.Xaml.Media.PathGeometry
            /* 0x01B2 */ "CenterOfRotationX", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B3 */ "CenterOfRotationY", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B4 */ "CenterOfRotationZ", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B5 */ "GlobalOffsetX", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B6 */ "GlobalOffsetY", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B7 */ "GlobalOffsetZ", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B8 */ "LocalOffsetX", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01B9 */ "LocalOffsetY", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01BA */ "LocalOffsetZ", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01BB */ "ProjectionMatrix", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01BC */ "RotationX", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01BD */ "RotationY", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01BE */ "RotationZ", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x01BF */ "By", // Windows.UI.Xaml.Media.Animation.PointAnimation
            /* 0x01C0 */ "EasingFunction", // Windows.UI.Xaml.Media.Animation.PointAnimation
            /* 0x01C1 */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.PointAnimation
            /* 0x01C2 */ "From", // Windows.UI.Xaml.Media.Animation.PointAnimation
            /* 0x01C3 */ "To", // Windows.UI.Xaml.Media.Animation.PointAnimation
            /* 0x01C4 */ "EnableDependentAnimation", // Windows.UI.Xaml.Media.Animation.PointAnimationUsingKeyFrames
            /* 0x01C5 */ "KeyFrames", // Windows.UI.Xaml.Media.Animation.PointAnimationUsingKeyFrames
            /* 0x01C6 */ "KeyFrames", // Windows.UI.Xaml.Internal.PointerAnimationUsingKeyFrames
            /* 0x01C7 */ "PointerSource", // Windows.UI.Xaml.Internal.PointerAnimationUsingKeyFrames
            /* 0x01C8 */ "Points", // Windows.UI.Xaml.Media.PolyBezierSegment
            /* 0x01C9 */ "Points", // Windows.UI.Xaml.Media.PolyLineSegment
            /* 0x01CA */ "Points", // Windows.UI.Xaml.Media.PolyQuadraticBezierSegment
            /* 0x01CB */ "FromHorizontalOffset", // Windows.UI.Xaml.Media.Animation.PopupThemeTransition
            /* 0x01CC */ "FromVerticalOffset", // Windows.UI.Xaml.Media.Animation.PopupThemeTransition
            /* 0x01CD */ "Power", // Windows.UI.Xaml.Media.Animation.PowerEase
            /* 0x01CE */ "Delay", // Windows.UI.Xaml.PVLStaggerFunction
            /* 0x01CF */ "DelayReduce", // Windows.UI.Xaml.PVLStaggerFunction
            /* 0x01D0 */ "Maximum", // Windows.UI.Xaml.PVLStaggerFunction
            /* 0x01D1 */ "Reverse", // Windows.UI.Xaml.PVLStaggerFunction
            /* 0x01D2 */ "Point1", // Windows.UI.Xaml.Media.QuadraticBezierSegment
            /* 0x01D3 */ "Point2", // Windows.UI.Xaml.Media.QuadraticBezierSegment
            /* 0x01D4 */ "RadiusX", // Windows.UI.Xaml.Media.RectangleGeometry
            /* 0x01D5 */ "RadiusY", // Windows.UI.Xaml.Media.RectangleGeometry
            /* 0x01D6 */ "Rect", // Windows.UI.Xaml.Media.RectangleGeometry
            /* 0x01D7 */ "Mode", // Windows.UI.Xaml.Data.RelativeSource
            /* 0x01D8 */ "PixelHeight", // Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap
            /* 0x01D9 */ "PixelWidth", // Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap
            /* 0x01DA */ "Property", // Windows.UI.Xaml.Setter
            /* 0x01DB */ "Value", // Windows.UI.Xaml.Setter
            /* 0x01DC */ "Color", // Windows.UI.Xaml.Media.SolidColorBrush
            /* 0x01DD */ "KeySpline", // Windows.UI.Xaml.Media.Animation.SplineColorKeyFrame
            /* 0x01DE */ "KeySpline", // Windows.UI.Xaml.Media.Animation.SplineDoubleKeyFrame
            /* 0x01DF */ "KeySpline", // Windows.UI.Xaml.Media.Animation.SplinePointKeyFrame
            /* 0x01E0 */ "ResourceKey", // Windows.UI.Xaml.StaticResource
            /* 0x01E1 */ "Property", // Windows.UI.Xaml.Data.TemplateBinding
            /* 0x01E2 */ "ResourceKey", // Windows.UI.Xaml.ThemeResource
            /* 0x01E3 */ "AlignmentX", // Windows.UI.Xaml.Media.TileBrush
            /* 0x01E4 */ "AlignmentY", // Windows.UI.Xaml.Media.TileBrush
            /* 0x01E5 */ "Stretch", // Windows.UI.Xaml.Media.TileBrush
            /* 0x01E6 */ "ContentProperty", // Windows.UI.Xaml.Automation.Peers.AutomationPeerCollection
            /* 0x01E7 */ "Converter", // Windows.UI.Xaml.Data.Binding
            /* 0x01E8 */ "ConverterLanguage", // Windows.UI.Xaml.Data.Binding
            /* 0x01E9 */ "ConverterParameter", // Windows.UI.Xaml.Data.Binding
            /* 0x01EA */ "ElementName", // Windows.UI.Xaml.Data.Binding
            /* 0x01EB */ "FallbackValue", // Windows.UI.Xaml.Data.Binding
            /* 0x01EC */ "Mode", // Windows.UI.Xaml.Data.Binding
            /* 0x01ED */ "Path", // Windows.UI.Xaml.Data.Binding
            /* 0x01EE */ "RelativeSource", // Windows.UI.Xaml.Data.Binding
            /* 0x01EF */ "Source", // Windows.UI.Xaml.Data.Binding
            /* 0x01F0 */ "TargetNullValue", // Windows.UI.Xaml.Data.Binding
            /* 0x01F1 */ "UpdateSourceTrigger", // Windows.UI.Xaml.Data.Binding
            /* 0x01F2 */ "CreateOptions", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x01F3 */ "DecodePixelHeight", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x01F4 */ "DecodePixelType", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x01F5 */ "DecodePixelWidth", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x01F6 */ "UriSource", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x01F7 */ "Background", // Windows.UI.Xaml.Controls.Border
            /* 0x01F8 */ "BorderBrush", // Windows.UI.Xaml.Controls.Border
            /* 0x01F9 */ "BorderThickness", // Windows.UI.Xaml.Controls.Border
            /* 0x01FA */ "Child", // Windows.UI.Xaml.Controls.Border
            /* 0x01FB */ "ChildTransitions", // Windows.UI.Xaml.Controls.Border
            /* 0x01FC */ "CornerRadius", // Windows.UI.Xaml.Controls.Border
            /* 0x01FD */ "Padding", // Windows.UI.Xaml.Controls.Border
            /* 0x01FE */ "Source", // Windows.UI.Xaml.Controls.CaptureElement
            /* 0x01FF */ "Stretch", // Windows.UI.Xaml.Controls.CaptureElement
            /* 0x0200 */ "ContentProperty", // Windows.UI.Xaml.Media.Animation.ColorKeyFrameCollection
            /* 0x0201 */ "ContentProperty", // Windows.UI.Xaml.Controls.ColumnDefinitionCollection
            /* 0x0202 */ "CenterX", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0203 */ "CenterY", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0204 */ "Rotation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0205 */ "ScaleX", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0206 */ "ScaleY", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0207 */ "SkewX", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0208 */ "SkewY", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0209 */ "TranslateX", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x020A */ "TranslateY", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x020B */ "CharacterSpacing", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x020C */ "Content", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x020D */ "ContentTemplate", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x020E */ "ContentTemplateSelector", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x020F */ "ContentTransitions", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0210 */ "FontFamily", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0211 */ "FontSize", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0212 */ "FontStretch", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0213 */ "FontStyle", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0214 */ "FontWeight", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0215 */ "Foreground", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0216 */ "IsTextScaleFactorEnabled", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0217 */ "LineStackingStrategy", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0218 */ "MaxLines", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0219 */ "OpticalMarginAlignment", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x021A */ "SelectedContentTemplate", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x021B */ "TextLineBounds", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x021C */ "TextWrapping", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x021D */ "Background", // Windows.UI.Xaml.Controls.Control
            /* 0x021E */ "BorderBrush", // Windows.UI.Xaml.Controls.Control
            /* 0x021F */ "BorderThickness", // Windows.UI.Xaml.Controls.Control
            /* 0x0220 */ "CharacterSpacing", // Windows.UI.Xaml.Controls.Control
            /* 0x0221 */ "DefaultStyleKey", // Windows.UI.Xaml.Controls.Control
            /* 0x0222 */ "FocusState", // Windows.UI.Xaml.Controls.Control
            /* 0x0223 */ "FontFamily", // Windows.UI.Xaml.Controls.Control
            /* 0x0224 */ "FontSize", // Windows.UI.Xaml.Controls.Control
            /* 0x0225 */ "FontStretch", // Windows.UI.Xaml.Controls.Control
            /* 0x0226 */ "FontStyle", // Windows.UI.Xaml.Controls.Control
            /* 0x0227 */ "FontWeight", // Windows.UI.Xaml.Controls.Control
            /* 0x0228 */ "Foreground", // Windows.UI.Xaml.Controls.Control
            /* 0x0229 */ "HorizontalContentAlignment", // Windows.UI.Xaml.Controls.Control
            /* 0x022A */ "IsEnabled", // Windows.UI.Xaml.Controls.Control
            /* 0x022B */ "IsTabStop", // Windows.UI.Xaml.Controls.Control
            /* 0x022C */ "IsTextScaleFactorEnabled", // Windows.UI.Xaml.Controls.Control
            /* 0x022D */ "Padding", // Windows.UI.Xaml.Controls.Control
            /* 0x022E */ "TabIndex", // Windows.UI.Xaml.Controls.Control
            /* 0x022F */ "TabNavigation", // Windows.UI.Xaml.Controls.Control
            /* 0x0230 */ "Template", // Windows.UI.Xaml.Controls.Control
            /* 0x0231 */ "VerticalContentAlignment", // Windows.UI.Xaml.Controls.Control
            /* 0x0232 */ "DisplayMemberPath", // Windows.UI.Xaml.Internal.DisplayMemberTemplate
            /* 0x0233 */ "ContentProperty", // Windows.UI.Xaml.Media.DoubleCollection
            /* 0x0234 */ "ContentProperty", // Windows.UI.Xaml.Media.Animation.DoubleKeyFrameCollection
            /* 0x0235 */ "TargetName", // Windows.UI.Xaml.Media.Animation.DragItemThemeAnimation
            /* 0x0236 */ "Direction", // Windows.UI.Xaml.Media.Animation.DragOverThemeAnimation
            /* 0x0237 */ "TargetName", // Windows.UI.Xaml.Media.Animation.DragOverThemeAnimation
            /* 0x0238 */ "ToOffset", // Windows.UI.Xaml.Media.Animation.DragOverThemeAnimation
            /* 0x0239 */ "TargetName", // Windows.UI.Xaml.Media.Animation.DropTargetItemThemeAnimation
            /* 0x023A */ "TargetName", // Windows.UI.Xaml.Media.Animation.FadeInThemeAnimation
            /* 0x023B */ "TargetName", // Windows.UI.Xaml.Media.Animation.FadeOutThemeAnimation
            /* 0x023C */ "ContentProperty", // Windows.UI.Xaml.Media.FloatCollection
            /* 0x023D */ "ContentProperty", // Windows.UI.Xaml.Media.GeometryCollection
            /* 0x023E */ "Fill", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x023F */ "FontRenderingEmSize", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0240 */ "FontUri", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0241 */ "Indices", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0242 */ "OriginX", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0243 */ "OriginY", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0244 */ "StyleSimulations", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0245 */ "UnicodeString", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x0246 */ "ContentProperty", // Windows.UI.Xaml.Media.GradientStopCollection
            /* 0x0247 */ "ContentProperty", // Windows.UI.Xaml.Controls.HubSectionCollection
            /* 0x0248 */ "Foreground", // Windows.UI.Xaml.Controls.IconElement
            /* 0x0249 */ "DownloadProgress", // Windows.UI.Xaml.Controls.Image
            /* 0x024A */ "NineGrid", // Windows.UI.Xaml.Controls.Image
            /* 0x024B */ "PlayToSource", // Windows.UI.Xaml.Controls.Image
            /* 0x024C */ "Source", // Windows.UI.Xaml.Controls.Image
            /* 0x024D */ "Stretch", // Windows.UI.Xaml.Controls.Image
            /* 0x024E */ "DownloadProgress", // Windows.UI.Xaml.Media.ImageBrush
            /* 0x024F */ "ImageSource", // Windows.UI.Xaml.Media.ImageBrush
            /* 0x0250 */ "Child", // Windows.UI.Xaml.Documents.InlineUIContainer
            /* 0x0251 */ "ContentProperty", // Windows.UI.Xaml.Input.InputScopeNameCollection
            /* 0x0252 */ "Footer", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0253 */ "FooterTemplate", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0254 */ "FooterTransitions", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0255 */ "Header", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0256 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0257 */ "HeaderTransitions", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0258 */ "ItemsPanel", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x0259 */ "Padding", // Windows.UI.Xaml.Controls.ItemsPresenter
            /* 0x025A */ "EndPoint", // Windows.UI.Xaml.Media.LinearGradientBrush
            /* 0x025B */ "StartPoint", // Windows.UI.Xaml.Media.LinearGradientBrush
            /* 0x025C */ "Matrix", // Windows.UI.Xaml.Media.MatrixTransform
            /* 0x025D */ "ActualStereo3DVideoPackingMode", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x025E */ "AreTransportControlsEnabled", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x025F */ "AspectRatioHeight", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0260 */ "AspectRatioWidth", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0261 */ "AudioCategory", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0262 */ "AudioDeviceType", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0263 */ "AudioStreamCount", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0264 */ "AudioStreamIndex", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0265 */ "AutoPlay", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0266 */ "Balance", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0267 */ "BufferingProgress", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0268 */ "CanPause", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0269 */ "CanSeek", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x026A */ "CurrentState", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x026B */ "DefaultPlaybackRate", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x026C */ "DownloadProgress", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x026D */ "DownloadProgressOffset", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x026E */ "FullScreen", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x026F */ "IsAudioOnly", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0270 */ "IsFullWindow", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0271 */ "IsLooping", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0272 */ "IsMuted", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0273 */ "IsStereo3DVideo", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0274 */ "Markers", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0275 */ "NaturalDuration", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0276 */ "NaturalVideoHeight", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0277 */ "NaturalVideoWidth", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0278 */ "PlaybackRate", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0279 */ "PlayToPreferredSourceUri", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x027A */ "PlayToSource", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x027B */ "Position", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x027C */ "PosterSource", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x027D */ "ProtectionManager", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x027E */ "RealTimePlayback", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x027F */ "Source", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0280 */ "Stereo3DVideoPackingMode", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0281 */ "Stereo3DVideoRenderMode", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0282 */ "Stretch", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0283 */ "TransportControls", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0284 */ "Volume", // Windows.UI.Xaml.Controls.MediaElement
            /* 0x0285 */ "ContentProperty", // Windows.UI.Xaml.Controls.MenuFlyoutItemBaseCollection
            /* 0x0286 */ "ContentProperty", // Windows.UI.Xaml.Media.Animation.ObjectKeyFrameCollection
            /* 0x0287 */ "Background", // Windows.UI.Xaml.Controls.Panel
            /* 0x0288 */ "Children", // Windows.UI.Xaml.Controls.Panel
            /* 0x0289 */ "ChildrenTransitions", // Windows.UI.Xaml.Controls.Panel
            /* 0x028A */ "IsIgnoringTransitions", // Windows.UI.Xaml.Controls.Panel
            /* 0x028B */ "IsItemsHost", // Windows.UI.Xaml.Controls.Panel
            /* 0x028C */ "Inlines", // Windows.UI.Xaml.Documents.Paragraph
            /* 0x028D */ "TextIndent", // Windows.UI.Xaml.Documents.Paragraph
            /* 0x028E */ "ContentProperty", // Windows.UI.Xaml.Internal.ParametricCurveCollection
            /* 0x028F */ "ContentProperty", // Windows.UI.Xaml.Internal.ParametricCurveSegmentCollection
            /* 0x0290 */ "ContentProperty", // Windows.UI.Xaml.Media.PathFigureCollection
            /* 0x0291 */ "ContentProperty", // Windows.UI.Xaml.Media.PathSegmentCollection
            /* 0x0292 */ "ContentProperty", // Windows.UI.Xaml.Media.PointCollection
            /* 0x0293 */ "ContentProperty", // Windows.UI.Xaml.Input.PointerCollection
            /* 0x0294 */ "TargetName", // Windows.UI.Xaml.Media.Animation.PointerDownThemeAnimation
            /* 0x0295 */ "ContentProperty", // Windows.UI.Xaml.Internal.PointerKeyFrameCollection
            /* 0x0296 */ "TargetName", // Windows.UI.Xaml.Media.Animation.PointerUpThemeAnimation
            /* 0x0297 */ "ContentProperty", // Windows.UI.Xaml.Media.Animation.PointKeyFrameCollection
            /* 0x0298 */ "FromHorizontalOffset", // Windows.UI.Xaml.Media.Animation.PopInThemeAnimation
            /* 0x0299 */ "FromVerticalOffset", // Windows.UI.Xaml.Media.Animation.PopInThemeAnimation
            /* 0x029A */ "TargetName", // Windows.UI.Xaml.Media.Animation.PopInThemeAnimation
            /* 0x029B */ "TargetName", // Windows.UI.Xaml.Media.Animation.PopOutThemeAnimation
            /* 0x029C */ "Child", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x029D */ "ChildTransitions", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x029E */ "HorizontalOffset", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x029F */ "IsApplicationBarService", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x02A0 */ "IsFlyout", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x02A1 */ "IsLightDismissEnabled", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x02A2 */ "IsOpen", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x02A3 */ "IsSettingsFlyout", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x02A4 */ "VerticalOffset", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x02A5 */ "Center", // Windows.UI.Xaml.Media.RadialGradientBrush
            /* 0x02A6 */ "GradientOrigin", // Windows.UI.Xaml.Media.RadialGradientBrush
            /* 0x02A7 */ "RadiusX", // Windows.UI.Xaml.Media.RadialGradientBrush
            /* 0x02A8 */ "RadiusY", // Windows.UI.Xaml.Media.RadialGradientBrush
            /* 0x02A9 */ "Count", // Windows.UI.Xaml.Media.Animation.RepeatBehavior
            /* 0x02AA */ "Duration", // Windows.UI.Xaml.Media.Animation.RepeatBehavior
            /* 0x02AB */ "FromHorizontalOffset", // Windows.UI.Xaml.Media.Animation.RepositionThemeAnimation
            /* 0x02AC */ "FromVerticalOffset", // Windows.UI.Xaml.Media.Animation.RepositionThemeAnimation
            /* 0x02AD */ "TargetName", // Windows.UI.Xaml.Media.Animation.RepositionThemeAnimation
            /* 0x02AE */ "ContentProperty", // Windows.UI.Xaml.ResourceDictionary
            /* 0x02AF */ "MergedDictionaries", // Windows.UI.Xaml.ResourceDictionary
            /* 0x02B0 */ "Source", // Windows.UI.Xaml.ResourceDictionary
            /* 0x02B1 */ "ThemeDictionaries", // Windows.UI.Xaml.ResourceDictionary
            /* 0x02B2 */ "ContentProperty", // Windows.UI.Xaml.Internal.ResourceDictionaryCollection
            /* 0x02B3 */ "Blocks", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02B4 */ "CharacterSpacing", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02B5 */ "FontFamily", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02B6 */ "FontSize", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02B7 */ "FontStretch", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02B8 */ "FontStyle", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02B9 */ "FontWeight", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02BA */ "Foreground", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02BB */ "HasOverflowContent", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02BC */ "IsColorFontEnabled", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02BD */ "IsTextScaleFactorEnabled", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02BE */ "IsTextSelectionEnabled", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02BF */ "LineHeight", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C0 */ "LineStackingStrategy", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C1 */ "MaxLines", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C2 */ "OpticalMarginAlignment", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C3 */ "OverflowContentTarget", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C4 */ "Padding", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C5 */ "SelectedText", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C6 */ "SelectionHighlightColor", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C7 */ "TextAlignment", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C8 */ "TextIndent", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02C9 */ "TextLineBounds", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02CA */ "TextReadingOrder", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02CB */ "TextTrimming", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02CC */ "TextWrapping", // Windows.UI.Xaml.Controls.RichTextBlock
            /* 0x02CD */ "HasOverflowContent", // Windows.UI.Xaml.Controls.RichTextBlockOverflow
            /* 0x02CE */ "MaxLines", // Windows.UI.Xaml.Controls.RichTextBlockOverflow
            /* 0x02CF */ "OverflowContentTarget", // Windows.UI.Xaml.Controls.RichTextBlockOverflow
            /* 0x02D0 */ "Padding", // Windows.UI.Xaml.Controls.RichTextBlockOverflow
            /* 0x02D1 */ "Angle", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x02D2 */ "CenterX", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x02D3 */ "CenterY", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x02D4 */ "ContentProperty", // Windows.UI.Xaml.Controls.RowDefinitionCollection
            /* 0x02D5 */ "FlowDirection", // Windows.UI.Xaml.Documents.Run
            /* 0x02D6 */ "Text", // Windows.UI.Xaml.Documents.Run
            /* 0x02D7 */ "CenterX", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x02D8 */ "CenterY", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x02D9 */ "ScaleX", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x02DA */ "ScaleY", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x02DB */ "ContentProperty", // Windows.UI.Xaml.SetterBaseCollection
            /* 0x02DC */ "IsSealed", // Windows.UI.Xaml.SetterBaseCollection
            /* 0x02DD */ "Fill", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02DE */ "GeometryTransform", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02DF */ "Stretch", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E0 */ "Stroke", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E1 */ "StrokeDashArray", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E2 */ "StrokeDashCap", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E3 */ "StrokeDashOffset", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E4 */ "StrokeEndLineCap", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E5 */ "StrokeLineJoin", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E6 */ "StrokeMiterLimit", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E7 */ "StrokeStartLineCap", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E8 */ "StrokeThickness", // Windows.UI.Xaml.Shapes.Shape
            /* 0x02E9 */ "AngleX", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x02EA */ "AngleY", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x02EB */ "CenterX", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x02EC */ "CenterY", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x02ED */ "Inlines", // Windows.UI.Xaml.Documents.Span
            /* 0x02EE */ "ClosedLength", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02EF */ "ClosedTarget", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F0 */ "ClosedTargetName", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F1 */ "ContentTarget", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F2 */ "ContentTargetName", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F3 */ "ContentTranslationDirection", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F4 */ "ContentTranslationOffset", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F5 */ "OffsetFromCenter", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F6 */ "OpenedLength", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F7 */ "OpenedTarget", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F8 */ "OpenedTargetName", // Windows.UI.Xaml.Media.Animation.SplitCloseThemeAnimation
            /* 0x02F9 */ "ClosedLength", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x02FA */ "ClosedTarget", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x02FB */ "ClosedTargetName", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x02FC */ "ContentTarget", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x02FD */ "ContentTargetName", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x02FE */ "ContentTranslationDirection", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x02FF */ "ContentTranslationOffset", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x0300 */ "OffsetFromCenter", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x0301 */ "OpenedLength", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x0302 */ "OpenedTarget", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x0303 */ "OpenedTargetName", // Windows.UI.Xaml.Media.Animation.SplitOpenThemeAnimation
            /* 0x0304 */ "Children", // Windows.UI.Xaml.Media.Animation.Storyboard
            /* 0x0305 */ "IsEssential", // Windows.UI.Xaml.Media.Animation.Storyboard
            /* 0x0306 */ "TargetName", // Windows.UI.Xaml.Media.Animation.Storyboard
            /* 0x0307 */ "TargetProperty", // Windows.UI.Xaml.Media.Animation.Storyboard
            /* 0x0308 */ "FromHorizontalOffset", // Windows.UI.Xaml.Media.Animation.SwipeBackThemeAnimation
            /* 0x0309 */ "FromVerticalOffset", // Windows.UI.Xaml.Media.Animation.SwipeBackThemeAnimation
            /* 0x030A */ "TargetName", // Windows.UI.Xaml.Media.Animation.SwipeBackThemeAnimation
            /* 0x030B */ "TargetName", // Windows.UI.Xaml.Media.Animation.SwipeHintThemeAnimation
            /* 0x030C */ "ToHorizontalOffset", // Windows.UI.Xaml.Media.Animation.SwipeHintThemeAnimation
            /* 0x030D */ "ToVerticalOffset", // Windows.UI.Xaml.Media.Animation.SwipeHintThemeAnimation
            /* 0x030E */ "CharacterSpacing", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x030F */ "FontFamily", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0310 */ "FontSize", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0311 */ "FontStretch", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0312 */ "FontStyle", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0313 */ "FontWeight", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0314 */ "Foreground", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0315 */ "Inlines", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0316 */ "IsColorFontEnabled", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0317 */ "IsTextScaleFactorEnabled", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0318 */ "IsTextSelectionEnabled", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0319 */ "LineHeight", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x031A */ "LineStackingStrategy", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x031B */ "MaxLines", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x031C */ "OpticalMarginAlignment", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x031D */ "Padding", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x031E */ "SelectedText", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x031F */ "SelectionHighlightColor", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0320 */ "Text", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0321 */ "TextAlignment", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0322 */ "TextDecorations", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0323 */ "TextLineBounds", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0324 */ "TextReadingOrder", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0325 */ "TextTrimming", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0326 */ "TextWrapping", // Windows.UI.Xaml.Controls.TextBlock
            /* 0x0327 */ "ContentProperty", // Windows.UI.Xaml.Documents.TextElementCollection
            /* 0x0328 */ "ContentProperty", // Windows.UI.Xaml.Media.Animation.TimelineCollection
            /* 0x0329 */ "ContentProperty", // Windows.UI.Xaml.Media.TimelineMarkerCollection
            /* 0x032A */ "ContentProperty", // Windows.UI.Xaml.Media.TransformCollection
            /* 0x032B */ "Children", // Windows.UI.Xaml.Media.TransformGroup
            /* 0x032C */ "Value", // Windows.UI.Xaml.Media.TransformGroup
            /* 0x032D */ "ContentProperty", // Windows.UI.Xaml.Media.Animation.TransitionCollection
            /* 0x032E */ "X", // Windows.UI.Xaml.Media.TranslateTransform
            /* 0x032F */ "Y", // Windows.UI.Xaml.Media.TranslateTransform
            /* 0x0330 */ "ContentProperty", // Windows.UI.Xaml.TriggerActionCollection
            /* 0x0331 */ "ContentProperty", // Windows.UI.Xaml.TriggerCollection
            /* 0x0332 */ "ContentProperty", // Windows.UI.Xaml.Controls.UIElementCollection
            /* 0x0333 */ "Child", // Windows.UI.Xaml.Controls.Viewbox
            /* 0x0334 */ "Stretch", // Windows.UI.Xaml.Controls.Viewbox
            /* 0x0335 */ "StretchDirection", // Windows.UI.Xaml.Controls.Viewbox
            /* 0x0336 */ "ContentProperty", // Windows.UI.Xaml.Internal.VisualStateCollection
            /* 0x0337 */ "ContentProperty", // Windows.UI.Xaml.Internal.VisualStateGroupCollection
            /* 0x0338 */ "ContentProperty", // Windows.UI.Xaml.Internal.VisualTransitionCollection
            /* 0x0339 */ "SourceName", // Windows.UI.Xaml.Controls.WebViewBrush
            /* 0x033A */ "IsCompact", // Windows.UI.Xaml.Controls.AppBarSeparator
            /* 0x033B */ "UriSource", // Windows.UI.Xaml.Controls.BitmapIcon
            /* 0x033C */ "Left", // Windows.UI.Xaml.Controls.Canvas
            /* 0x033D */ "Top", // Windows.UI.Xaml.Controls.Canvas
            /* 0x033E */ "ZIndex", // Windows.UI.Xaml.Controls.Canvas
            /* 0x033F */ "ContentProperty", // Windows.UI.Xaml.Controls.CommandBarElementCollection
            /* 0x0340 */ "Content", // Windows.UI.Xaml.Controls.ContentControl
            /* 0x0341 */ "ContentTemplate", // Windows.UI.Xaml.Controls.ContentControl
            /* 0x0342 */ "ContentTemplateSelector", // Windows.UI.Xaml.Controls.ContentControl
            /* 0x0343 */ "ContentTransitions", // Windows.UI.Xaml.Controls.ContentControl
            /* 0x0344 */ "SelectedContentTemplate", // Windows.UI.Xaml.Controls.ContentControl
            /* 0x0345 */ "CalendarIdentifier", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0346 */ "Date", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0347 */ "DayFormat", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0348 */ "DayVisible", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0349 */ "Header", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x034A */ "HeaderTemplate", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x034B */ "MaxYear", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x034C */ "MinYear", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x034D */ "MonthFormat", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x034E */ "MonthVisible", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x034F */ "Orientation", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0350 */ "YearFormat", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0351 */ "YearVisible", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0352 */ "ContentProperty", // Windows.UI.Xaml.DependencyObjectCollection
            /* 0x0353 */ "FontFamily", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0354 */ "FontSize", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0355 */ "FontStyle", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0356 */ "FontWeight", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0357 */ "Glyph", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0358 */ "IsTextScaleFactorEnabled", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0359 */ "Column", // Windows.UI.Xaml.Controls.Grid
            /* 0x035A */ "ColumnDefinitions", // Windows.UI.Xaml.Controls.Grid
            /* 0x035B */ "ColumnSpan", // Windows.UI.Xaml.Controls.Grid
            /* 0x035C */ "Row", // Windows.UI.Xaml.Controls.Grid
            /* 0x035D */ "RowDefinitions", // Windows.UI.Xaml.Controls.Grid
            /* 0x035E */ "RowSpan", // Windows.UI.Xaml.Controls.Grid
            /* 0x035F */ "DefaultSectionIndex", // Windows.UI.Xaml.Controls.Hub
            /* 0x0360 */ "Header", // Windows.UI.Xaml.Controls.Hub
            /* 0x0361 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.Hub
            /* 0x0362 */ "IsActiveView", // Windows.UI.Xaml.Controls.Hub
            /* 0x0363 */ "IsZoomedInView", // Windows.UI.Xaml.Controls.Hub
            /* 0x0364 */ "Orientation", // Windows.UI.Xaml.Controls.Hub
            /* 0x0365 */ "SectionHeaders", // Windows.UI.Xaml.Controls.Hub
            /* 0x0366 */ "Sections", // Windows.UI.Xaml.Controls.Hub
            /* 0x0367 */ "SectionsInView", // Windows.UI.Xaml.Controls.Hub
            /* 0x0368 */ "SemanticZoomOwner", // Windows.UI.Xaml.Controls.Hub
            /* 0x0369 */ "ContentTemplate", // Windows.UI.Xaml.Controls.HubSection
            /* 0x036A */ "Header", // Windows.UI.Xaml.Controls.HubSection
            /* 0x036B */ "HeaderTemplate", // Windows.UI.Xaml.Controls.HubSection
            /* 0x036C */ "IsHeaderInteractive", // Windows.UI.Xaml.Controls.HubSection
            /* 0x036D */ "NavigateUri", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x036E */ "ContentProperty", // Windows.UI.Xaml.Controls.ItemCollection
            /* 0x036F */ "DisplayMemberPath", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0370 */ "GroupStyle", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0371 */ "GroupStyleSelector", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0372 */ "IsGrouping", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0373 */ "IsItemsHostInvalid", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0374 */ "ItemContainerStyle", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0375 */ "ItemContainerStyleSelector", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0376 */ "ItemContainerTransitions", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0377 */ "Items", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0378 */ "ItemsHost", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x0379 */ "ItemsPanel", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x037A */ "ItemsSource", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x037B */ "ItemTemplate", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x037C */ "ItemTemplateSelector", // Windows.UI.Xaml.Controls.ItemsControl
            /* 0x037D */ "X1", // Windows.UI.Xaml.Shapes.Line
            /* 0x037E */ "X2", // Windows.UI.Xaml.Shapes.Line
            /* 0x037F */ "Y1", // Windows.UI.Xaml.Shapes.Line
            /* 0x0380 */ "Y2", // Windows.UI.Xaml.Shapes.Line
            /* 0x0381 */ null,
            /* 0x0382 */ "IsFastForwardButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0383 */ null,
            /* 0x0384 */ "IsFastRewindButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0385 */ null,
            /* 0x0386 */ "IsFullWindowButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0387 */ null,
            /* 0x0388 */ "IsPlaybackRateButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0389 */ "IsSeekBarVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x038A */ null,
            /* 0x038B */ null,
            /* 0x038C */ "IsStopButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x038D */ null,
            /* 0x038E */ "IsVolumeButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x038F */ null,
            /* 0x0390 */ "IsZoomButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0391 */ "Header", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0392 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0393 */ "IsPasswordRevealButtonEnabled", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0394 */ "MaxLength", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0395 */ "Password", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0396 */ "PasswordChar", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0397 */ "PlaceholderText", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0398 */ "PreventKeyboardDisplayOnProgrammaticFocus", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0399 */ "SelectionHighlightColor", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x039A */ "Data", // Windows.UI.Xaml.Shapes.Path
            /* 0x039B */ "Data", // Windows.UI.Xaml.Controls.PathIcon
            /* 0x039C */ "FillRule", // Windows.UI.Xaml.Shapes.Polygon
            /* 0x039D */ "Points", // Windows.UI.Xaml.Shapes.Polygon
            /* 0x039E */ "FillRule", // Windows.UI.Xaml.Shapes.Polyline
            /* 0x039F */ "Points", // Windows.UI.Xaml.Shapes.Polyline
            /* 0x03A0 */ "IsActive", // Windows.UI.Xaml.Controls.ProgressRing
            /* 0x03A1 */ "TemplateSettings", // Windows.UI.Xaml.Controls.ProgressRing
            /* 0x03A2 */ "LargeChange", // Windows.UI.Xaml.Controls.Primitives.RangeBase
            /* 0x03A3 */ "Maximum", // Windows.UI.Xaml.Controls.Primitives.RangeBase
            /* 0x03A4 */ "Minimum", // Windows.UI.Xaml.Controls.Primitives.RangeBase
            /* 0x03A5 */ "SmallChange", // Windows.UI.Xaml.Controls.Primitives.RangeBase
            /* 0x03A6 */ "Value", // Windows.UI.Xaml.Controls.Primitives.RangeBase
            /* 0x03A7 */ "RadiusX", // Windows.UI.Xaml.Shapes.Rectangle
            /* 0x03A8 */ "RadiusY", // Windows.UI.Xaml.Shapes.Rectangle
            /* 0x03A9 */ "AcceptsReturn", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03AA */ "Header", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03AB */ "HeaderTemplate", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03AC */ "InputScope", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03AD */ "IsColorFontEnabled", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03AE */ "IsReadOnly", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03AF */ "IsSpellCheckEnabled", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B0 */ "IsTextPredictionEnabled", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B1 */ "PlaceholderText", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B2 */ "PreventKeyboardDisplayOnProgrammaticFocus", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B3 */ "SelectionHighlightColor", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B4 */ "TextAlignment", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B5 */ "TextWrapping", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x03B6 */ "ChooseSuggestionOnEnter", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x03B7 */ "FocusOnKeyboardInput", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x03B8 */ "PlaceholderText", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x03B9 */ "QueryText", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x03BA */ "SearchHistoryContext", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x03BB */ "SearchHistoryEnabled", // Windows.UI.Xaml.Controls.SearchBox
            /* 0x03BC */ "CanChangeViews", // Windows.UI.Xaml.Controls.SemanticZoom
            /* 0x03BD */ "IsZoomedInViewActive", // Windows.UI.Xaml.Controls.SemanticZoom
            /* 0x03BE */ "IsZoomOutButtonEnabled", // Windows.UI.Xaml.Controls.SemanticZoom
            /* 0x03BF */ "ZoomedInView", // Windows.UI.Xaml.Controls.SemanticZoom
            /* 0x03C0 */ "ZoomedOutView", // Windows.UI.Xaml.Controls.SemanticZoom
            /* 0x03C1 */ "AreScrollSnapPointsRegular", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x03C2 */ "Orientation", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x03C3 */ "Symbol", // Windows.UI.Xaml.Controls.SymbolIcon
            /* 0x03C4 */ "AcceptsReturn", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03C5 */ "Header", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03C6 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03C7 */ "InputScope", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03C8 */ "IsColorFontEnabled", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03C9 */ "IsCoreDesktopPopupMenuEnabled", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03CA */ null,
            /* 0x03CB */ "IsReadOnly", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03CC */ "IsSpellCheckEnabled", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03CD */ "IsTextPredictionEnabled", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03CE */ "MaxLength", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03CF */ "PlaceholderText", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D0 */ "PreventKeyboardDisplayOnProgrammaticFocus", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D1 */ "SelectedText", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D2 */ "SelectionHighlightColor", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D3 */ "SelectionLength", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D4 */ "SelectionStart", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D5 */ "Text", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D6 */ "TextAlignment", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D7 */ "TextWrapping", // Windows.UI.Xaml.Controls.TextBox
            /* 0x03D8 */ "IsDragging", // Windows.UI.Xaml.Controls.Primitives.Thumb
            /* 0x03D9 */ "Fill", // Windows.UI.Xaml.Controls.Primitives.TickBar
            /* 0x03DA */ "ClockIdentifier", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x03DB */ "Header", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x03DC */ "HeaderTemplate", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x03DD */ "MinuteIncrement", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x03DE */ "Time", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x03DF */ "Header", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E0 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E1 */ "IsOn", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E2 */ "OffContent", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E3 */ "OffContentTemplate", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E4 */ "OnContent", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E5 */ "OnContentTemplate", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E6 */ "TemplateSettings", // Windows.UI.Xaml.Controls.ToggleSwitch
            /* 0x03E7 */ "Content", // Windows.UI.Xaml.Controls.UserControl
            /* 0x03E8 */ "ColumnSpan", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03E9 */ "HorizontalChildrenAlignment", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03EA */ "ItemHeight", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03EB */ "ItemWidth", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03EC */ "MaximumRowsOrColumns", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03ED */ "Orientation", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03EE */ "RowSpan", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03EF */ "VerticalChildrenAlignment", // Windows.UI.Xaml.Controls.VariableSizedWrapGrid
            /* 0x03F0 */ "AllowedScriptNotifyUris", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F1 */ "CanGoBack", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F2 */ "CanGoForward", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F3 */ "ContainsFullScreenElement", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F4 */ "DataTransferPackage", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F5 */ "DefaultBackgroundColor", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F6 */ "DocumentTitle", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F7 */ "Source", // Windows.UI.Xaml.Controls.WebView
            /* 0x03F8 */ "ClosedDisplayMode", // Windows.UI.Xaml.Controls.AppBar
            /* 0x03F9 */ "IsOpen", // Windows.UI.Xaml.Controls.AppBar
            /* 0x03FA */ "IsSticky", // Windows.UI.Xaml.Controls.AppBar
            /* 0x03FB */ "AutoMaximizeSuggestionArea", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x03FC */ "Header", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x03FD */ "IsSuggestionListOpen", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x03FE */ "MaxSuggestionListHeight", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x03FF */ "PlaceholderText", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x0400 */ "Text", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x0401 */ "TextBoxStyle", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x0402 */ "TextMemberPath", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x0403 */ "UpdateTextOnSelect", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x0404 */ "ContentProperty", // Windows.UI.Xaml.Documents.BlockCollection
            /* 0x0405 */ "ClickMode", // Windows.UI.Xaml.Controls.Primitives.ButtonBase
            /* 0x0406 */ "Command", // Windows.UI.Xaml.Controls.Primitives.ButtonBase
            /* 0x0407 */ "CommandParameter", // Windows.UI.Xaml.Controls.Primitives.ButtonBase
            /* 0x0408 */ "IsPointerOver", // Windows.UI.Xaml.Controls.Primitives.ButtonBase
            /* 0x0409 */ "IsPressed", // Windows.UI.Xaml.Controls.Primitives.ButtonBase
            /* 0x040A */ "FullSizeDesired", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x040B */ "IsPrimaryButtonEnabled", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x040C */ "IsSecondaryButtonEnabled", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x040D */ "PrimaryButtonCommand", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x040E */ "PrimaryButtonCommandParameter", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x040F */ "PrimaryButtonText", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x0410 */ "SecondaryButtonCommand", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x0411 */ "SecondaryButtonCommandParameter", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x0412 */ "SecondaryButtonText", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x0413 */ "Title", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x0414 */ "TitleTemplate", // Windows.UI.Xaml.Controls.ContentDialog
            /* 0x0415 */ "BackStack", // Windows.UI.Xaml.Controls.Frame
            /* 0x0416 */ "BackStackDepth", // Windows.UI.Xaml.Controls.Frame
            /* 0x0417 */ "CacheSize", // Windows.UI.Xaml.Controls.Frame
            /* 0x0418 */ "CanGoBack", // Windows.UI.Xaml.Controls.Frame
            /* 0x0419 */ "CanGoForward", // Windows.UI.Xaml.Controls.Frame
            /* 0x041A */ "CurrentSourcePageType", // Windows.UI.Xaml.Controls.Frame
            /* 0x041B */ "ForwardStack", // Windows.UI.Xaml.Controls.Frame
            /* 0x041C */ "SourcePageType", // Windows.UI.Xaml.Controls.Frame
            /* 0x041D */ "CheckBrush", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x041E */ "CheckHintBrush", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x041F */ "CheckSelectingBrush", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0420 */ "ContentMargin", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0421 */ "DisabledOpacity", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0422 */ "DragBackground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0423 */ "DragForeground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0424 */ "DragOpacity", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0425 */ "FocusBorderBrush", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0426 */ "GridViewItemPresenterHorizontalContentAlignment", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0427 */ "GridViewItemPresenterPadding", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0428 */ "PlaceholderBackground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0429 */ "PointerOverBackground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x042A */ "PointerOverBackgroundMargin", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x042B */ "ReorderHintOffset", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x042C */ "SelectedBackground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x042D */ "SelectedBorderThickness", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x042E */ "SelectedForeground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x042F */ "SelectedPointerOverBackground", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0430 */ "SelectedPointerOverBorderBrush", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0431 */ "SelectionCheckMarkVisualEnabled", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0432 */ "GridViewItemPresenterVerticalContentAlignment", // Windows.UI.Xaml.Controls.Primitives.GridViewItemPresenter
            /* 0x0433 */ "ContentProperty", // Windows.UI.Xaml.Documents.InlineCollection
            /* 0x0434 */ "CacheLength", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x0435 */ "GroupHeaderPlacement", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x0436 */ "GroupPadding", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x0437 */ "ItemsUpdatingScrollMode", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x0438 */ "Orientation", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x0439 */ "CacheLength", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x043A */ "GroupHeaderPlacement", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x043B */ "GroupPadding", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x043C */ "ItemHeight", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x043D */ "ItemWidth", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x043E */ "MaximumRowsOrColumns", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x043F */ "Orientation", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x0440 */ "CheckBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0441 */ "CheckHintBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0442 */ "CheckSelectingBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0443 */ "ContentMargin", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0444 */ "DisabledOpacity", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0445 */ "DragBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0446 */ "DragForeground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0447 */ "DragOpacity", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0448 */ "FocusBorderBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0449 */ "ListViewItemPresenterHorizontalContentAlignment", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x044A */ "ListViewItemPresenterPadding", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x044B */ "PlaceholderBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x044C */ "PointerOverBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x044D */ "PointerOverBackgroundMargin", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x044E */ "ReorderHintOffset", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x044F */ "SelectedBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0450 */ "SelectedBorderThickness", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0451 */ "SelectedForeground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0452 */ "SelectedPointerOverBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0453 */ "SelectedPointerOverBorderBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0454 */ "SelectionCheckMarkVisualEnabled", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0455 */ "ListViewItemPresenterVerticalContentAlignment", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0456 */ "Command", // Windows.UI.Xaml.Controls.MenuFlyoutItem
            /* 0x0457 */ "CommandParameter", // Windows.UI.Xaml.Controls.MenuFlyoutItem
            /* 0x0458 */ "Text", // Windows.UI.Xaml.Controls.MenuFlyoutItem
            /* 0x0459 */ "IsContainerGeneratedForInsert", // Windows.UI.Xaml.Controls.Primitives.OrientedVirtualizingPanel
            /* 0x045A */ "BottomAppBar", // Windows.UI.Xaml.Controls.Page
            /* 0x045B */ "Frame", // Windows.UI.Xaml.Controls.Page
            /* 0x045C */ "NavigationCacheMode", // Windows.UI.Xaml.Controls.Page
            /* 0x045D */ "TopAppBar", // Windows.UI.Xaml.Controls.Page
            /* 0x045E */ "IsIndeterminate", // Windows.UI.Xaml.Controls.ProgressBar
            /* 0x045F */ "ShowError", // Windows.UI.Xaml.Controls.ProgressBar
            /* 0x0460 */ "ShowPaused", // Windows.UI.Xaml.Controls.ProgressBar
            /* 0x0461 */ "TemplateSettings", // Windows.UI.Xaml.Controls.ProgressBar
            /* 0x0462 */ "IndicatorMode", // Windows.UI.Xaml.Controls.Primitives.ScrollBar
            /* 0x0463 */ "Orientation", // Windows.UI.Xaml.Controls.Primitives.ScrollBar
            /* 0x0464 */ "ViewportSize", // Windows.UI.Xaml.Controls.Primitives.ScrollBar
            /* 0x0465 */ "IsSelectionActive", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x0466 */ "IsSynchronizedWithCurrentItem", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x0467 */ "SelectedIndex", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x0468 */ "SelectedItem", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x0469 */ "SelectedValue", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x046A */ "SelectedValuePath", // Windows.UI.Xaml.Controls.Primitives.Selector
            /* 0x046B */ "IsSelected", // Windows.UI.Xaml.Controls.Primitives.SelectorItem
            /* 0x046C */ "HeaderBackground", // Windows.UI.Xaml.Controls.SettingsFlyout
            /* 0x046D */ "HeaderForeground", // Windows.UI.Xaml.Controls.SettingsFlyout
            /* 0x046E */ "IconSource", // Windows.UI.Xaml.Controls.SettingsFlyout
            /* 0x046F */ "TemplateSettings", // Windows.UI.Xaml.Controls.SettingsFlyout
            /* 0x0470 */ "Title", // Windows.UI.Xaml.Controls.SettingsFlyout
            /* 0x0471 */ "Header", // Windows.UI.Xaml.Controls.Slider
            /* 0x0472 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.Slider
            /* 0x0473 */ "IntermediateValue", // Windows.UI.Xaml.Controls.Slider
            /* 0x0474 */ "IsDirectionReversed", // Windows.UI.Xaml.Controls.Slider
            /* 0x0475 */ "IsThumbToolTipEnabled", // Windows.UI.Xaml.Controls.Slider
            /* 0x0476 */ "Orientation", // Windows.UI.Xaml.Controls.Slider
            /* 0x0477 */ "SnapsTo", // Windows.UI.Xaml.Controls.Slider
            /* 0x0478 */ "StepFrequency", // Windows.UI.Xaml.Controls.Slider
            /* 0x0479 */ "ThumbToolTipValueConverter", // Windows.UI.Xaml.Controls.Slider
            /* 0x047A */ "TickFrequency", // Windows.UI.Xaml.Controls.Slider
            /* 0x047B */ "TickPlacement", // Windows.UI.Xaml.Controls.Slider
            /* 0x047C */ "CompositionScaleX", // Windows.UI.Xaml.Controls.SwapChainPanel
            /* 0x047D */ "CompositionScaleY", // Windows.UI.Xaml.Controls.SwapChainPanel
            /* 0x047E */ "HorizontalOffset", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x047F */ "IsOpen", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x0480 */ "Placement", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x0481 */ "PlacementTarget", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x0482 */ "TemplateSettings", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x0483 */ "VerticalOffset", // Windows.UI.Xaml.Controls.ToolTip
            /* 0x0484 */ "Flyout", // Windows.UI.Xaml.Controls.Button
            /* 0x0485 */ "Header", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x0486 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x0487 */ "IsDropDownOpen", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x0488 */ "IsEditable", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x0489 */ "IsSelectionBoxHighlighted", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x048A */ "MaxDropDownHeight", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x048B */ "PlaceholderText", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x048C */ "SelectionBoxItem", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x048D */ "SelectionBoxItemTemplate", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x048E */ "TemplateSettings", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x048F */ "PrimaryCommands", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x0490 */ "SecondaryCommands", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x0491 */ "UseTouchAnimationsForAllNavigation", // Windows.UI.Xaml.Controls.FlipView
            /* 0x0492 */ "NavigateUri", // Windows.UI.Xaml.Controls.HyperlinkButton
            /* 0x0493 */ "SelectedItems", // Windows.UI.Xaml.Controls.ListBox
            /* 0x0494 */ "SelectionMode", // Windows.UI.Xaml.Controls.ListBox
            /* 0x0495 */ "CanDragItems", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x0496 */ "CanReorderItems", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x0497 */ "DataFetchSize", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x0498 */ "Footer", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x0499 */ "FooterTemplate", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x049A */ "FooterTransitions", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x049B */ "Header", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x049C */ "HeaderTemplate", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x049D */ "HeaderTransitions", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x049E */ "IncrementalLoadingThreshold", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x049F */ "IncrementalLoadingTrigger", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A0 */ "IsActiveView", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A1 */ "IsItemClickEnabled", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A2 */ "IsSwipeEnabled", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A3 */ "IsZoomedInView", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A4 */ "ReorderMode", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A5 */ "SelectedItems", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A6 */ "SelectionMode", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A7 */ "SemanticZoomOwner", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A8 */ "ShowsScrollingPlaceholders", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x04A9 */ "Delay", // Windows.UI.Xaml.Controls.Primitives.RepeatButton
            /* 0x04AA */ "Interval", // Windows.UI.Xaml.Controls.Primitives.RepeatButton
            /* 0x04AB */ "BringIntoViewOnFocusChange", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04AC */ "ComputedHorizontalScrollBarVisibility", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04AD */ "ComputedVerticalScrollBarVisibility", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04AE */ "ExtentHeight", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04AF */ "ExtentWidth", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B0 */ "HorizontalOffset", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B1 */ "HorizontalScrollBarVisibility", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B2 */ "HorizontalScrollMode", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B3 */ "HorizontalSnapPointsAlignment", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B4 */ "HorizontalSnapPointsType", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B5 */ "IsDeferredScrollingEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B6 */ "IsHorizontalRailEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B7 */ "IsHorizontalScrollChainingEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B8 */ "IsScrollInertiaEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04B9 */ "IsVerticalRailEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04BA */ "IsVerticalScrollChainingEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04BB */ "IsZoomChainingEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04BC */ "IsZoomInertiaEnabled", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04BD */ "LeftHeader", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04BE */ "MaxZoomFactor", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04BF */ "MinZoomFactor", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C0 */ "ScrollableHeight", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C1 */ "ScrollableWidth", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C2 */ "TopHeader", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C3 */ "TopLeftHeader", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C4 */ "VerticalOffset", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C5 */ "VerticalScrollBarVisibility", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C6 */ "VerticalScrollMode", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C7 */ "VerticalSnapPointsAlignment", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C8 */ "VerticalSnapPointsType", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04C9 */ "ViewportHeight", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04CA */ "ViewportWidth", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04CB */ "ZoomFactor", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04CC */ "ZoomMode", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04CD */ "ZoomSnapPoints", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04CE */ "ZoomSnapPointsType", // Windows.UI.Xaml.Controls.ScrollViewer
            /* 0x04CF */ "IsChecked", // Windows.UI.Xaml.Controls.Primitives.ToggleButton
            /* 0x04D0 */ "IsThreeState", // Windows.UI.Xaml.Controls.Primitives.ToggleButton
            /* 0x04D1 */ "IsChecked", // Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem
            /* 0x04D2 */ "AreScrollSnapPointsRegular", // Windows.UI.Xaml.Controls.VirtualizingStackPanel
            /* 0x04D3 */ "IsContainerGeneratedForInsert", // Windows.UI.Xaml.Controls.VirtualizingStackPanel
            /* 0x04D4 */ "IsVirtualizing", // Windows.UI.Xaml.Controls.VirtualizingStackPanel
            /* 0x04D5 */ "Orientation", // Windows.UI.Xaml.Controls.VirtualizingStackPanel
            /* 0x04D6 */ "VirtualizationMode", // Windows.UI.Xaml.Controls.VirtualizingStackPanel
            /* 0x04D7 */ "HorizontalChildrenAlignment", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x04D8 */ "ItemHeight", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x04D9 */ "ItemWidth", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x04DA */ "MaximumRowsOrColumns", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x04DB */ "Orientation", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x04DC */ "VerticalChildrenAlignment", // Windows.UI.Xaml.Controls.WrapGrid
            /* 0x04DD */ "Icon", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x04DE */ "IsCompact", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x04DF */ "Label", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x04E0 */ "Icon", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x04E1 */ "IsCompact", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x04E2 */ "Label", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x04E3 */ "TemplateSettings", // Windows.UI.Xaml.Controls.GridViewItem
            /* 0x04E4 */ "TemplateSettings", // Windows.UI.Xaml.Controls.ListViewItem
            /* 0x04E5 */ "GroupName", // Windows.UI.Xaml.Controls.RadioButton
            /* 0x04E6 */ "ActiveStoryboards", // Windows.UI.Xaml.Internal.VisualStateGroupCollection
            /* 0x04E7 */ "ActiveTransitions", // Windows.UI.Xaml.Internal.VisualStateGroupCollection
            /* 0x04E8 */ null,
            /* 0x04E9 */ null,
            /* 0x04EA */ null,
            /* 0x04EB */ null,
            /* 0x04EC */ null,
            /* 0x04ED */ null,
            /* 0x04EE */ null,
            /* 0x04EF */ null,
            /* 0x04F0 */ null,
            /* 0x04F1 */ "ContentProperty", // Windows.UI.Xaml.Internal.StoryboardCollection
            /* 0x04F2 */ "CacheLength", // Windows.UI.Xaml.Controls.PluggableLayoutPanel
            /* 0x04F3 */ "ColorFontPaletteIndex", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x04F4 */ "IsColorFontEnabled", // Windows.UI.Xaml.Documents.Glyphs
            /* 0x04F5 */ null,
            /* 0x04F6 */ null,
            /* 0x04F7 */ null,
            /* 0x04F8 */ null,
            /* 0x04F9 */ null,
            /* 0x04FA */ "HasMoreContentAfter", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x04FB */ "HasMoreContentBefore", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x04FC */ "HasMoreViews", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x04FD */ "HeaderText", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x04FE */ null,
            /* 0x04FF */ null,
            /* 0x0500 */ "WeekDay1", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0501 */ "WeekDay2", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0502 */ "WeekDay3", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0503 */ "WeekDay4", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0504 */ "WeekDay5", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0505 */ "WeekDay6", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0506 */ "WeekDay7", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0507 */ null,
            /* 0x0508 */ null,
            /* 0x0509 */ null,
            /* 0x050A */ null,
            /* 0x050B */ "CalendarIdentifier", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x050C */ null,
            /* 0x050D */ null,
            /* 0x050E */ null,
            /* 0x050F */ null,
            /* 0x0510 */ null,
            /* 0x0511 */ null,
            /* 0x0512 */ null,
            /* 0x0513 */ "DayOfWeekFormat", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0514 */ null,
            /* 0x0515 */ null,
            /* 0x0516 */ "DisplayMode", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0517 */ "FirstDayOfWeek", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0518 */ null,
            /* 0x0519 */ null,
            /* 0x051A */ null,
            /* 0x051B */ null,
            /* 0x051C */ null,
            /* 0x051D */ null,
            /* 0x051E */ null,
            /* 0x051F */ null,
            /* 0x0520 */ null,
            /* 0x0521 */ null,
            /* 0x0522 */ null,
            /* 0x0523 */ null,
            /* 0x0524 */ null,
            /* 0x0525 */ "IsOutOfScopeEnabled", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0526 */ "IsTodayHighlighted", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0527 */ null,
            /* 0x0528 */ "MaxDate", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0529 */ "MinDate", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x052A */ null,
            /* 0x052B */ null,
            /* 0x052C */ null,
            /* 0x052D */ null,
            /* 0x052E */ null,
            /* 0x052F */ "NumberOfWeeksInView", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0530 */ null,
            /* 0x0531 */ null,
            /* 0x0532 */ null,
            /* 0x0533 */ null,
            /* 0x0534 */ null,
            /* 0x0535 */ "SelectedDates", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0536 */ null,
            /* 0x0537 */ "SelectionMode", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0538 */ "TemplateSettings", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0539 */ null,
            /* 0x053A */ null,
            /* 0x053B */ "Date", // Windows.UI.Xaml.Controls.CalendarViewDayItem
            /* 0x053C */ "IsBlackout", // Windows.UI.Xaml.Controls.CalendarViewDayItem
            /* 0x053D */ "Date", // Windows.UI.Xaml.Controls.Primitives.CalendarViewItem
            /* 0x053E */ null,
            /* 0x053F */ null,
            /* 0x0540 */ null,
            /* 0x0541 */ null,
            /* 0x0542 */ null,
            /* 0x0543 */ null,
            /* 0x0544 */ null,
            /* 0x0545 */ null,
            /* 0x0546 */ null,
            /* 0x0547 */ null,
            /* 0x0548 */ null,
            /* 0x0549 */ null,
            /* 0x054A */ null,
            /* 0x054B */ null,
            /* 0x054C */ null,
            /* 0x054D */ null,
            /* 0x054E */ null,
            /* 0x054F */ null,
            /* 0x0550 */ null,
            /* 0x0551 */ null,
            /* 0x0552 */ null,
            /* 0x0553 */ null,
            /* 0x0554 */ null,
            /* 0x0555 */ null,
            /* 0x0556 */ null,
            /* 0x0557 */ null,
            /* 0x0558 */ null,
            /* 0x0559 */ null,
            /* 0x055A */ null,
            /* 0x055B */ null,
            /* 0x055C */ null,
            /* 0x055D */ null,
            /* 0x055E */ null,
            /* 0x055F */ null,
            /* 0x0560 */ null,
            /* 0x0561 */ null,
            /* 0x0562 */ null,
            /* 0x0563 */ null,
            /* 0x0564 */ null,
            /* 0x0565 */ null,
            /* 0x0566 */ "IsFastForwardEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0567 */ "IsFastRewindEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0568 */ "IsFullWindowEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0569 */ "IsPlaybackRateEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x056A */ "IsSeekEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x056B */ "IsStopEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x056C */ "IsVolumeEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x056D */ "IsZoomEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x056E */ null,
            /* 0x056F */ "Column", // Windows.UI.Xaml.DependencyObject
            /* 0x0570 */ "Line", // Windows.UI.Xaml.DependencyObject
            /* 0x0571 */ null,
            /* 0x0572 */ null,
            /* 0x0573 */ "OffsetXAnimation", // Windows.UI.Xaml.UIElement
            /* 0x0574 */ "OffsetYAnimation", // Windows.UI.Xaml.UIElement
            /* 0x0575 */ "CenterXAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0576 */ "CenterYAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0577 */ "RotateAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0578 */ "ScaleXAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x0579 */ "ScaleYAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x057A */ "SkewXAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x057B */ "SkewYAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x057C */ "TranslateXAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x057D */ "TranslateYAnimation", // Windows.UI.Xaml.Media.CompositeTransform
            /* 0x057E */ "AngleAnimation", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x057F */ "CenterXAnimation", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x0580 */ "CenterYAnimation", // Windows.UI.Xaml.Media.RotateTransform
            /* 0x0581 */ "CenterXAnimation", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x0582 */ "CenterYAnimation", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x0583 */ "ScaleXAnimation", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x0584 */ "ScaleYAnimation", // Windows.UI.Xaml.Media.ScaleTransform
            /* 0x0585 */ "AngleXAnimation", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x0586 */ "AngleYAnimation", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x0587 */ "CenterXAnimation", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x0588 */ "CenterYAnimation", // Windows.UI.Xaml.Media.SkewTransform
            /* 0x0589 */ "XAnimation", // Windows.UI.Xaml.Media.TranslateTransform
            /* 0x058A */ "YAnimation", // Windows.UI.Xaml.Media.TranslateTransform
            /* 0x058B */ null,
            /* 0x058C */ null,
            /* 0x058D */ null,
            /* 0x058E */ null,
            /* 0x058F */ null,
            /* 0x0590 */ null,
            /* 0x0591 */ "LineHeight", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0592 */ "UseOverflowStyle", // Windows.UI.Xaml.Controls.AppBarSeparator
            /* 0x0593 */ "UseOverflowStyle", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x0594 */ "UseOverflowStyle", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x0595 */ "CacheLength", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x0596 */ "Cols", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x0597 */ "Orientation", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x0598 */ "Rows", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x0599 */ "ItemMinHeight", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x059A */ "ItemMinWidth", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x059B */ "MinViewWidth", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x059C */ null,
            /* 0x059D */ null,
            /* 0x059E */ null,
            /* 0x059F */ null,
            /* 0x05A0 */ null,
            /* 0x05A1 */ null,
            /* 0x05A2 */ "OpacityAnimation", // Windows.UI.Xaml.Media.Animation.TransitionTarget
            /* 0x05A3 */ "OpacityAnimation", // Windows.UI.Xaml.UIElement
            /* 0x05A4 */ "CenterOfRotationXAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05A5 */ "CenterOfRotationYAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05A6 */ "CenterOfRotationZAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05A7 */ "GlobalOffsetXAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05A8 */ "GlobalOffsetYAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05A9 */ "GlobalOffsetZAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05AA */ "LocalOffsetXAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05AB */ "LocalOffsetYAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05AC */ "LocalOffsetZAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05AD */ "RotationXAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05AE */ "RotationYAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05AF */ "RotationZAnimation", // Windows.UI.Xaml.Media.PlaneProjection
            /* 0x05B0 */ null,
            /* 0x05B1 */ "CacheLength", // Windows.UI.Xaml.Controls.TileGrid
            /* 0x05B2 */ "Orientation", // Windows.UI.Xaml.Controls.TileGrid
            /* 0x05B3 */ "SelectedRanges", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x05B4 */ null,
            /* 0x05B5 */ null,
            /* 0x05B6 */ "CompactPaneGridLength", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x05B7 */ "NegativeOpenPaneLength", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x05B8 */ "NegativeOpenPaneLengthMinusCompactLength", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x05B9 */ "OpenPaneGridLength", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x05BA */ "OpenPaneLengthMinusCompactLength", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x05BB */ "CompactPaneLength", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05BC */ "Content", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05BD */ "DisplayMode", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05BE */ "IsPaneOpen", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05BF */ "OpenPaneLength", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05C0 */ "Pane", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05C1 */ "PanePlacement", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05C2 */ "TemplateSettings", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05C3 */ "Transform3D", // Windows.UI.Xaml.UIElement
            /* 0x05C4 */ "CenterX", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05C5 */ "CenterXAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05C6 */ "CenterY", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05C7 */ "CenterYAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05C8 */ "CenterZ", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05C9 */ "CenterZAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05CA */ "RotationX", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05CB */ "RotationXAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05CC */ "RotationY", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05CD */ "RotationYAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05CE */ "RotationZ", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05CF */ "RotationZAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D0 */ "ScaleX", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D1 */ "ScaleXAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D2 */ "ScaleY", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D3 */ "ScaleYAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D4 */ "ScaleZ", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D5 */ "ScaleZAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D6 */ "TranslateX", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D7 */ "TranslateXAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D8 */ "TranslateY", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05D9 */ "TranslateYAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05DA */ "TranslateZ", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05DB */ "TranslateZAnimation", // Windows.UI.Xaml.Media.Media3D.CompositeTransform3D
            /* 0x05DC */ "Depth", // Windows.UI.Xaml.Media.Media3D.PerspectiveTransform3D
            /* 0x05DD */ "OffsetX", // Windows.UI.Xaml.Media.Media3D.PerspectiveTransform3D
            /* 0x05DE */ "OffsetY", // Windows.UI.Xaml.Media.Media3D.PerspectiveTransform3D
            /* 0x05DF */ "ColorAAnimation", // Windows.UI.Xaml.Media.SolidColorBrush
            /* 0x05E0 */ "ColorBAnimation", // Windows.UI.Xaml.Media.SolidColorBrush
            /* 0x05E1 */ "ColorGAnimation", // Windows.UI.Xaml.Media.SolidColorBrush
            /* 0x05E2 */ "ColorRAnimation", // Windows.UI.Xaml.Media.SolidColorBrush
            /* 0x05E3 */ "ParseUri", // Windows.UI.Xaml.DependencyObject
            /* 0x05E4 */ "Above", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05E5 */ "AlignBottomWith", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05E6 */ "AlignLeftWith", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05E7 */ null,
            /* 0x05E8 */ null,
            /* 0x05E9 */ null,
            /* 0x05EA */ null,
            /* 0x05EB */ "AlignRightWith", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05EC */ "AlignTopWith", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05ED */ "Below", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05EE */ null,
            /* 0x05EF */ null,
            /* 0x05F0 */ "LeftOf", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05F1 */ "RightOf", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x05F2 */ null,
            /* 0x05F3 */ null,
            /* 0x05F4 */ "OpenPaneLength", // Windows.UI.Xaml.Controls.Primitives.SplitViewTemplateSettings
            /* 0x05F5 */ "TransparentBackground", // Windows.UI.Xaml.Window
            /* 0x05F6 */ "AreStickyGroupHeadersEnabledBase", // Windows.UI.Xaml.Controls.ModernCollectionBasePanel
            /* 0x05F7 */ "PasswordRevealMode", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x05F8 */ "PaneBackground", // Windows.UI.Xaml.Controls.SplitView
            /* 0x05F9 */ "AreStickyGroupHeadersEnabled", // Windows.UI.Xaml.Controls.ItemsStackPanel
            /* 0x05FA */ "AreStickyGroupHeadersEnabled", // Windows.UI.Xaml.Controls.ItemsWrapGrid
            /* 0x05FB */ "Items", // Windows.UI.Xaml.Controls.MenuFlyoutSubItem
            /* 0x05FC */ "Text", // Windows.UI.Xaml.Controls.MenuFlyoutSubItem
            /* 0x05FD */ "XbfHash", // Windows.UI.Xaml.DependencyObject
            /* 0x05FE */ "CanDrag", // Windows.UI.Xaml.UIElement
            /* 0x05FF */ "ExtensionInstance", // Windows.UI.Xaml.DataTemplate
            /* 0x0600 */ null,
            /* 0x0601 */ null,
            /* 0x0602 */ null,
            /* 0x0603 */ null,
            /* 0x0604 */ null,
            /* 0x0605 */ null,
            /* 0x0606 */ null,
            /* 0x0607 */ null,
            /* 0x0608 */ null,
            /* 0x0609 */ null,
            /* 0x060A */ null,
            /* 0x060B */ null,
            /* 0x060C */ null,
            /* 0x060D */ null,
            /* 0x060E */ null,
            /* 0x060F */ null,
            /* 0x0610 */ "AlignHorizontalCenterWith", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x0611 */ "AlignVerticalCenterWith", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x0612 */ null,
            /* 0x0613 */ "Path", // Windows.UI.Xaml.TargetPropertyPath
            /* 0x0614 */ "Target", // Windows.UI.Xaml.TargetPropertyPath
            /* 0x0615 */ "__DeferredSetters", // Windows.UI.Xaml.VisualState
            /* 0x0616 */ "Setters", // Windows.UI.Xaml.VisualState
            /* 0x0617 */ "StateTriggers", // Windows.UI.Xaml.VisualState
            /* 0x0618 */ "MinWindowHeight", // Windows.UI.Xaml.AdaptiveTrigger
            /* 0x0619 */ "MinWindowWidth", // Windows.UI.Xaml.AdaptiveTrigger
            /* 0x061A */ "Target", // Windows.UI.Xaml.Setter
            /* 0x061B */ "ContentProperty", // Windows.UI.Xaml.Internal.StateTriggerCollection
            /* 0x061C */ null,
            /* 0x061D */ "BlackoutForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x061E */ "CalendarItemBackground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x061F */ "CalendarItemBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0620 */ "CalendarItemBorderThickness", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0621 */ "CalendarItemForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0622 */ "CalendarViewDayItemStyle", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0623 */ "DayItemFontFamily", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0624 */ "DayItemFontSize", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0625 */ "DayItemFontStyle", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0626 */ "DayItemFontWeight", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0627 */ "FirstOfMonthLabelFontFamily", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0628 */ "FirstOfMonthLabelFontSize", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0629 */ "FirstOfMonthLabelFontStyle", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x062A */ "FirstOfMonthLabelFontWeight", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x062B */ "FirstOfYearDecadeLabelFontFamily", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x062C */ "FirstOfYearDecadeLabelFontSize", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x062D */ "FirstOfYearDecadeLabelFontStyle", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x062E */ "FirstOfYearDecadeLabelFontWeight", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x062F */ "FocusBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0630 */ "HorizontalDayItemAlignment", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0631 */ "HorizontalFirstOfMonthLabelAlignment", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0632 */ "HoverBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0633 */ null,
            /* 0x0634 */ "MonthYearItemFontFamily", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0635 */ "MonthYearItemFontSize", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0636 */ "MonthYearItemFontStyle", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0637 */ "MonthYearItemFontWeight", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0638 */ "OutOfScopeBackground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0639 */ "OutOfScopeForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x063A */ "PressedBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x063B */ "PressedForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x063C */ "SelectedBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x063D */ "SelectedForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x063E */ "SelectedHoverBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x063F */ "SelectedPressedBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0640 */ "TodayFontWeight", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0641 */ "TodayForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0642 */ "VerticalDayItemAlignment", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0643 */ "VerticalFirstOfMonthLabelAlignment", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0644 */ null,
            /* 0x0645 */ "IsCompact", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0646 */ "AlignBottomWithPanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x0647 */ "AlignHorizontalCenterWithPanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x0648 */ "AlignLeftWithPanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x0649 */ "AlignRightWithPanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x064A */ "AlignTopWithPanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x064B */ "AlignVerticalCenterWithPanel", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x064C */ "IsMultiSelectCheckBoxEnabled", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x064D */ "IsDraggable", // Windows.UI.Xaml.Controls.ListViewBaseItem
            /* 0x064E */ "Level", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x064F */ "PositionInSet", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0650 */ "SizeOfSet", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0651 */ "CheckBoxBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0652 */ "CheckMode", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0653 */ null,
            /* 0x0654 */ "PressedBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0655 */ "SelectedPressedBackground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x0656 */ "FocusTargetDescendant", // Windows.UI.Xaml.Controls.Control
            /* 0x0657 */ "IsTemplateFocusTarget", // Windows.UI.Xaml.Controls.Control
            /* 0x0658 */ "UseSystemFocusVisuals", // Windows.UI.Xaml.Controls.Control
            /* 0x0659 */ null,
            /* 0x065A */ null,
            /* 0x065B */ "DirectManipulationContainer", // Windows.UI.Xaml.UIElement
            /* 0x065C */ "FocusSecondaryBorderBrush", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x065D */ null,
            /* 0x065E */ "PointerOverForeground", // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter
            /* 0x065F */ "MirroredWhenRightToLeft", // Windows.UI.Xaml.Controls.FontIcon
            /* 0x0660 */ "CenterX", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0661 */ "CenterY", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0662 */ "ClipRect", // Windows.UI.Xaml.Controls.Primitives.CalendarViewTemplateSettings
            /* 0x0663 */ "HandOffCompositionVisual", // Windows.UI.Xaml.UIElement
            /* 0x0664 */ "DeferredStorage", // Windows.UI.Xaml.DependencyObject
            /* 0x0665 */ "RealizingProxy", // Windows.UI.Xaml.DependencyObject
            /* 0x0666 */ "CanvasOffset", // Windows.UI.Xaml.UIElement
            /* 0x0667 */ null,
            /* 0x0668 */ null,
            /* 0x0669 */ null,
            /* 0x066A */ null,
            /* 0x066B */ null,
            /* 0x066C */ "ClosedRatio", // Windows.UI.Xaml.Media.Animation.MenuPopupThemeTransition
            /* 0x066D */ "Direction", // Windows.UI.Xaml.Media.Animation.MenuPopupThemeTransition
            /* 0x066E */ "OpenedLength", // Windows.UI.Xaml.Media.Animation.MenuPopupThemeTransition
            /* 0x066F */ "DeferredStateTriggers", // Windows.UI.Xaml.Internal.VisualStateGroupCollection
            /* 0x0670 */ "TriggerState", // Windows.UI.Xaml.StateTriggerBase
            /* 0x0671 */ null,
            /* 0x0672 */ "TextReadingOrder", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x0673 */ "TextReadingOrder", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x0674 */ "TextReadingOrder", // Windows.UI.Xaml.Controls.TextBox
            /* 0x0675 */ "ExecutionMode", // Windows.UI.Xaml.Controls.WebView
            /* 0x0676 */ "CachedStyleSetterProperty", // Windows.UI.Xaml.TargetPropertyPath
            /* 0x0677 */ "DeferredPermissionRequests", // Windows.UI.Xaml.Controls.WebView
            /* 0x0678 */ "Settings", // Windows.UI.Xaml.Controls.WebView
            /* 0x0679 */ "OffsetFromCenter", // Windows.UI.Xaml.Media.Animation.PickerFlyoutThemeTransition
            /* 0x067A */ "OpenedLength", // Windows.UI.Xaml.Media.Animation.PickerFlyoutThemeTransition
            /* 0x067B */ null,
            /* 0x067C */ "DesiredCandidateWindowAlignment", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x067D */ null,
            /* 0x067E */ "DesiredCandidateWindowAlignment", // Windows.UI.Xaml.Controls.TextBox
            /* 0x067F */ "CalendarIdentifier", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0680 */ "CalendarViewStyle", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0681 */ "Date", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0682 */ "DateFormat", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0683 */ "DayOfWeekFormat", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0684 */ "DisplayMode", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0685 */ "FirstDayOfWeek", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0686 */ "Header", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0687 */ "HeaderTemplate", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0688 */ "IsCalendarOpen", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0689 */ "IsGroupLabelVisible", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x068A */ "IsOutOfScopeEnabled", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x068B */ "IsTodayHighlighted", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x068C */ "MaxDate", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x068D */ "MinDate", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x068E */ "PlaceholderText", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x068F */ "IsGroupLabelVisible", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x0690 */ "Background", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0691 */ "BorderBrush", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0692 */ "BorderThickness", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0693 */ "CornerRadius", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0694 */ "Padding", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x0695 */ "BorderBrush", // Windows.UI.Xaml.Controls.Grid
            /* 0x0696 */ "BorderThickness", // Windows.UI.Xaml.Controls.Grid
            /* 0x0697 */ "CornerRadius", // Windows.UI.Xaml.Controls.Grid
            /* 0x0698 */ "Padding", // Windows.UI.Xaml.Controls.Grid
            /* 0x0699 */ "BorderBrush", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x069A */ "BorderThickness", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x069B */ "CornerRadius", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x069C */ "Padding", // Windows.UI.Xaml.Controls.RelativePanel
            /* 0x069D */ "BorderBrush", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x069E */ "BorderThickness", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x069F */ "CornerRadius", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x06A0 */ "Padding", // Windows.UI.Xaml.Controls.StackPanel
            /* 0x06A1 */ "InputScope", // Windows.UI.Xaml.Controls.PasswordBox
            /* 0x06A2 */ "DropoutOrder", // Windows.UI.Xaml.Controls.MediaTransportControlsHelper
            /* 0x06A3 */ "ChosenSuggestion", // Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs
            /* 0x06A4 */ "QueryText", // Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs
            /* 0x06A5 */ "QueryIcon", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x06A6 */ "IsActive", // Windows.UI.Xaml.StateTrigger
            /* 0x06A7 */ "HorizontalContentAlignment", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x06A8 */ "VerticalContentAlignment", // Windows.UI.Xaml.Controls.ContentPresenter
            /* 0x06A9 */ "ClipRect", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06AA */ "CompactRootMargin", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06AB */ "CompactVerticalDelta", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06AC */ "HiddenRootMargin", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06AD */ "HiddenVerticalDelta", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06AE */ "MinimalRootMargin", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06AF */ "MinimalVerticalDelta", // Windows.UI.Xaml.Controls.Primitives.AppBarTemplateSettings
            /* 0x06B0 */ "ContentHeight", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B1 */ "NegativeOverflowContentHeight", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B2 */ "OverflowContentClipRect", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B3 */ "OverflowContentHeight", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B4 */ "OverflowContentHorizontalOffset", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B5 */ "OverflowContentMaxHeight", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B6 */ "OverflowContentMinWidth", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06B7 */ "TemplateSettings", // Windows.UI.Xaml.Controls.AppBar
            /* 0x06B8 */ "CommandBarOverflowPresenterStyle", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x06B9 */ "CommandBarTemplateSettings", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x06BA */ "EntranceTarget", // Windows.UI.Xaml.Media.Animation.DrillInThemeAnimation
            /* 0x06BB */ "EntranceTargetName", // Windows.UI.Xaml.Media.Animation.DrillInThemeAnimation
            /* 0x06BC */ "ExitTarget", // Windows.UI.Xaml.Media.Animation.DrillInThemeAnimation
            /* 0x06BD */ "ExitTargetName", // Windows.UI.Xaml.Media.Animation.DrillInThemeAnimation
            /* 0x06BE */ "EntranceTarget", // Windows.UI.Xaml.Media.Animation.DrillOutThemeAnimation
            /* 0x06BF */ "EntranceTargetName", // Windows.UI.Xaml.Media.Animation.DrillOutThemeAnimation
            /* 0x06C0 */ "ExitTarget", // Windows.UI.Xaml.Media.Animation.DrillOutThemeAnimation
            /* 0x06C1 */ "ExitTargetName", // Windows.UI.Xaml.Media.Animation.DrillOutThemeAnimation
            /* 0x06C2 */ "DataTemplateComponent", // Windows.UI.Xaml.Markup.XamlBindingHelper
            /* 0x06C3 */ "DeferredSetters", // Windows.UI.Xaml.Internal.VisualStateGroupCollection
            /* 0x06C4 */ "Annotations", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06C5 */ "Element", // Windows.UI.Xaml.Automation.AutomationAnnotation
            /* 0x06C6 */ "Type", // Windows.UI.Xaml.Automation.AutomationAnnotation
            /* 0x06C7 */ "Peer", // Windows.UI.Xaml.Automation.Peers.AutomationPeerAnnotation
            /* 0x06C8 */ "Type", // Windows.UI.Xaml.Automation.Peers.AutomationPeerAnnotation
            /* 0x06C9 */ "ContentProperty", // Windows.UI.Xaml.Automation.AutomationAnnotationCollection
            /* 0x06CA */ "ContentProperty", // Windows.UI.Xaml.Automation.Peers.AutomationPeerAnnotationCollection
            /* 0x06CB */ "StartIndex", // Windows.UI.Xaml.Controls.Primitives.CalendarPanel
            /* 0x06CC */ "AutomationPeerFactoryIndex", // Windows.UI.Xaml.FrameworkElement
            /* 0x06CD */ "UnderlineStyle", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x06CE */ "DisabledForeground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x06CF */ "TodayBackground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x06D0 */ "TodayBlackoutBackground", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x06D1 */ "TodayHoverBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x06D2 */ "TodayPressedBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x06D3 */ "TodaySelectedInnerBorderBrush", // Windows.UI.Xaml.Controls.CalendarView
            /* 0x06D4 */ "IsContentDialog", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x06D5 */ "IsFocusEngaged", // Windows.UI.Xaml.Controls.Control
            /* 0x06D6 */ null,
            /* 0x06D7 */ "ElevatorHelper", // Windows.UI.Xaml.Controls.Control
            /* 0x06D8 */ "IsFocusEngagementEnabled", // Windows.UI.Xaml.Controls.Control
            /* 0x06D9 */ "LayoutToWindowBounds", // Windows.UI.Xaml.Controls.Page
            /* 0x06DA */ "ClipboardCopyFormat", // Windows.UI.Xaml.Controls.RichEditBox
            /* 0x06DB */ "PreventEditFocusLoss", // Windows.UI.Xaml.Controls.TextBox
            /* 0x06DC */ "HandInCompositionVisual", // Windows.UI.Xaml.UIElement
            /* 0x06DD */ "OverflowContentMaxWidth", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x06DE */ "DropDownContentMinWidth", // Windows.UI.Xaml.Controls.Primitives.ComboBoxTemplateSettings
            /* 0x06DF */ null,
            /* 0x06E0 */ null,
            /* 0x06E1 */ "HandOffVisualTransform", // Windows.UI.Xaml.UIElement
            /* 0x06E2 */ "FlyoutContentMinWidth", // Windows.UI.Xaml.Controls.Primitives.MenuFlyoutPresenterTemplateSettings
            /* 0x06E3 */ "TemplateSettings", // Windows.UI.Xaml.Controls.MenuFlyoutPresenter
            /* 0x06E4 */ null,
            /* 0x06E5 */ null,
            /* 0x06E6 */ "LandmarkType", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06E7 */ "LocalizedLandmarkType", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06E8 */ "GlobalScaleFactor", // Windows.UI.Xaml.UIElement
            /* 0x06E9 */ "IsStaggeringEnabled", // Windows.UI.Xaml.Media.Animation.RepositionThemeTransition
            /* 0x06EA */ "SingleSelectionFollowsFocus", // Windows.UI.Xaml.Controls.ListBox
            /* 0x06EB */ "SingleSelectionFollowsFocus", // Windows.UI.Xaml.Controls.ListViewBase
            /* 0x06EC */ "AssociatedFlyout", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x06ED */ "AutoPlay", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x06EE */ "IsAnimatedBitmap", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x06EF */ "IsPlaying", // Windows.UI.Xaml.Media.Imaging.BitmapImage
            /* 0x06F0 */ "FullDescription", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06F1 */ "IsDataValidForForm", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06F2 */ "IsPeripheral", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06F3 */ "LocalizedControlType", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06F4 */ "AllowFocusOnInteraction", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x06F5 */ "AllowFocusOnInteraction", // Windows.UI.Xaml.Documents.TextElement
            /* 0x06F6 */ "AllowFocusOnInteraction", // Windows.UI.Xaml.FrameworkElement
            /* 0x06F7 */ "RequiresPointer", // Windows.UI.Xaml.Controls.Control
            /* 0x06F8 */ null,
            /* 0x06F9 */ "ContextFlyout", // Windows.UI.Xaml.UIElement
            /* 0x06FA */ "AccessKey", // Windows.UI.Xaml.Documents.TextElement
            /* 0x06FB */ "AccessKeyScopeOwner", // Windows.UI.Xaml.UIElement
            /* 0x06FC */ "IsAccessKeyScope", // Windows.UI.Xaml.UIElement
            /* 0x06FD */ null,
            /* 0x06FE */ "DescribedBy", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x06FF */ null,
            /* 0x0700 */ null,
            /* 0x0701 */ null,
            /* 0x0702 */ null,
            /* 0x0703 */ null,
            /* 0x0704 */ null,
            /* 0x0705 */ null,
            /* 0x0706 */ null,
            /* 0x0707 */ null,
            /* 0x0708 */ null,
            /* 0x0709 */ null,
            /* 0x070A */ null,
            /* 0x070B */ "AccessKey", // Windows.UI.Xaml.UIElement
            /* 0x070C */ "XYFocusDown", // Windows.UI.Xaml.Controls.Control
            /* 0x070D */ "XYFocusLeft", // Windows.UI.Xaml.Controls.Control
            /* 0x070E */ "XYFocusRight", // Windows.UI.Xaml.Controls.Control
            /* 0x070F */ "XYFocusUp", // Windows.UI.Xaml.Controls.Control
            /* 0x0710 */ "XYFocusDown", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x0711 */ "XYFocusLeft", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x0712 */ "XYFocusRight", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x0713 */ "XYFocusUp", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x0714 */ "XYFocusDown", // Windows.UI.Xaml.Controls.WebView
            /* 0x0715 */ "XYFocusLeft", // Windows.UI.Xaml.Controls.WebView
            /* 0x0716 */ "XYFocusRight", // Windows.UI.Xaml.Controls.WebView
            /* 0x0717 */ "XYFocusUp", // Windows.UI.Xaml.Controls.WebView
            /* 0x0718 */ "EffectiveOverflowButtonVisibility", // Windows.UI.Xaml.Controls.Primitives.CommandBarTemplateSettings
            /* 0x0719 */ "IsInOverflow", // Windows.UI.Xaml.Controls.AppBarSeparator
            /* 0x071A */ "DefaultLabelPosition", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x071B */ "IsDynamicOverflowEnabled", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x071C */ "OverflowButtonVisibility", // Windows.UI.Xaml.Controls.CommandBar
            /* 0x071D */ "IsInOverflow", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x071E */ "LabelPosition", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x071F */ "IsInOverflow", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x0720 */ "LabelPosition", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x0721 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x0722 */ "DisableOverlayIsLightDismissCheck", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x0723 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x0724 */ "OverlayElement", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x0725 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.CalendarDatePicker
            /* 0x0726 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.DatePicker
            /* 0x0727 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.SplitView
            /* 0x0728 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.TimePicker
            /* 0x0729 */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.AppBar
            /* 0x072A */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.AutoSuggestBox
            /* 0x072B */ "LightDismissOverlayMode", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x072C */ "DynamicOverflowOrder", // Windows.UI.Xaml.Controls.AppBarSeparator
            /* 0x072D */ "DynamicOverflowOrder", // Windows.UI.Xaml.Controls.AppBarButton
            /* 0x072E */ "DynamicOverflowOrder", // Windows.UI.Xaml.Controls.AppBarToggleButton
            /* 0x072F */ "FocusVisualMargin", // Windows.UI.Xaml.FrameworkElement
            /* 0x0730 */ "FocusVisualPrimaryBrush", // Windows.UI.Xaml.FrameworkElement
            /* 0x0731 */ "FocusVisualPrimaryThickness", // Windows.UI.Xaml.FrameworkElement
            /* 0x0732 */ "FocusVisualSecondaryBrush", // Windows.UI.Xaml.FrameworkElement
            /* 0x0733 */ "FocusVisualSecondaryThickness", // Windows.UI.Xaml.FrameworkElement
            /* 0x0734 */ null,
            /* 0x0735 */ null,
            /* 0x0736 */ "AllowFocusWhenDisabled", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x0737 */ "AllowFocusWhenDisabled", // Windows.UI.Xaml.FrameworkElement
            /* 0x0738 */ "IsTextSearchEnabled", // Windows.UI.Xaml.Controls.ComboBox
            /* 0x0739 */ "ExitDisplayModeOnAccessKeyInvoked", // Windows.UI.Xaml.Documents.TextElement
            /* 0x073A */ "ExitDisplayModeOnAccessKeyInvoked", // Windows.UI.Xaml.UIElement
            /* 0x073B */ "IsFullWindow", // Windows.UI.Xaml.Controls.MediaPlayerPresenter
            /* 0x073C */ "MediaPlayer", // Windows.UI.Xaml.Controls.MediaPlayerPresenter
            /* 0x073D */ "Stretch", // Windows.UI.Xaml.Controls.MediaPlayerPresenter
            /* 0x073E */ "AreTransportControlsEnabled", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x073F */ "AutoPlay", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0740 */ "IsFullWindow", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0741 */ "MediaPlayer", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0742 */ "PosterSource", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0743 */ "Source", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0744 */ "Stretch", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0745 */ "TransportControls", // Windows.UI.Xaml.Controls.MediaPlayerElement
            /* 0x0746 */ "FastPlayFallbackBehaviour", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0747 */ "IsNextTrackButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0748 */ "IsPreviousTrackButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x0749 */ "IsSkipBackwardButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x074A */ "IsSkipBackwardEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x074B */ "IsSkipForwardButtonVisible", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x074C */ "IsSkipForwardEnabled", // Windows.UI.Xaml.Controls.MediaTransportControls
            /* 0x074D */ "ElementSoundMode", // Windows.UI.Xaml.Controls.Primitives.FlyoutBase
            /* 0x074E */ "ElementSoundMode", // Windows.UI.Xaml.Controls.Control
            /* 0x074F */ "ElementSoundMode", // Windows.UI.Xaml.Documents.Hyperlink
            /* 0x0750 */ "OpacityExpression", // Windows.UI.Xaml.UIElement
            /* 0x0751 */ "IsGamepadFocusCandidate", // Windows.UI.Xaml.UIElement
            /* 0x0752 */ "IsSubMenu", // Windows.UI.Xaml.Controls.Primitives.Popup
            /* 0x0753 */ "ContentProperty", // Windows.UI.Xaml.Media.BrushCollection
            /* 0x0754 */ "FlowsFrom", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0755 */ "FlowsTo", // Windows.UI.Xaml.Automation.AutomationProperties
            /* 0x0756 */ "RequiresPointerMode", // Windows.UI.Xaml.Application
        };

        private static readonly Dictionary<int, string>[] enumValues = {
            /* 0x023C */ new Dictionary<int, string> { { 0, "Raw" }, { 1, "Control" }, { 2, "Content" } }, // Windows.UI.Xaml.Automation.Peers.AccessibilityView
            /* 0x023D */ new Dictionary<int, string> { { 0, "Left" }, { 1, "Center" }, { 2, "Right" } }, // Windows.UI.Xaml.Media.AlignmentX
            /* 0x023E */ new Dictionary<int, string> { { 0, "Top" }, { 1, "Center" }, { 2, "Bottom" } }, // Windows.UI.Xaml.Media.AlignmentY
            /* 0x023F */ new Dictionary<int, string> { { 0, "Left" }, { 1, "Top" }, { 2, "Right" }, { 3, "Bottom" } }, // Windows.UI.Xaml.Controls.Primitives.AnimationDirection
            /* 0x0240 */ new Dictionary<int, string> { { 60000, "Unknown" }, { 60001, "SpellingError" }, { 60002, "GrammarError" }, { 60003, "Comment" }, { 60004, "FormulaError" }, { 60005, "TrackChanges" }, { 60006, "Header" }, { 60007, "Footer" }, { 60008, "Highlighted" }, { 60009, "Endnote" }, { 60010, "Footnote" }, { 60011, "InsertionChange" }, { 60012, "DeletionChange" }, { 60013, "MoveChange" }, { 60014, "FormatChange" }, { 60015, "UnsyncedChange" }, { 60016, "EditingLockedChange" }, { 60017, "ExternalChange" }, { 60018, "ConflictingChange" }, { 60019, "Author" }, { 60020, "AdvancedProofingIssue" }, { 60021, "DataValidationError" }, { 60022, "CircularReferenceError" } }, // Windows.UI.Xaml.Automation.AnnotationType
            /* 0x0241 */ new Dictionary<int, string> { { 0, "Compact" }, { 1, "Minimal" }, { 2, "Hidden" } }, // Windows.UI.Xaml.Controls.AppBarClosedDisplayMode
            /* 0x0242 */ new Dictionary<int, string> { { 0, "Light" }, { 1, "Dark" } }, // Windows.UI.Xaml.ApplicationTheme
            /* 0x0243 */ new Dictionary<int, string> { { 0, "Other" }, { 1, "ForegroundOnlyMedia" }, { 2, "BackgroundCapableMedia" }, { 3, "Communications" }, { 4, "Alerts" }, { 5, "SoundEffects" }, { 6, "GameEffects" }, { 7, "GameMedia" }, { 8, "GameChat" }, { 9, "Speech" }, { 10, "Movie" }, { 11, "Media" } }, // Windows.UI.Xaml.Media.AudioCategory
            /* 0x0244 */ new Dictionary<int, string> { { 0, "Console" }, { 1, "Multimedia" }, { 2, "Communications" } }, // Windows.UI.Xaml.Media.AudioDeviceType
            /* 0x0245 */ new Dictionary<int, string> { { 0, "Button" }, { 1, "Calendar" }, { 2, "CheckBox" }, { 3, "ComboBox" }, { 4, "Edit" }, { 5, "Hyperlink" }, { 6, "Image" }, { 7, "ListItem" }, { 8, "List" }, { 9, "Menu" }, { 10, "MenuBar" }, { 11, "MenuItem" }, { 12, "ProgressBar" }, { 13, "RadioButton" }, { 14, "ScrollBar" }, { 15, "Slider" }, { 16, "Spinner" }, { 17, "StatusBar" }, { 18, "Tab" }, { 19, "TabItem" }, { 20, "Text" }, { 21, "ToolBar" }, { 22, "ToolTip" }, { 23, "Tree" }, { 24, "TreeItem" }, { 25, "Custom" }, { 26, "Group" }, { 27, "Thumb" }, { 28, "DataGrid" }, { 29, "DataItem" }, { 30, "Document" }, { 31, "SplitButton" }, { 32, "Window" }, { 33, "Pane" }, { 34, "Header" }, { 35, "HeaderItem" }, { 36, "Table" }, { 37, "TitleBar" }, { 38, "Separator" }, { 39, "SemanticZoom" }, { 40, "AppBar" } }, // Windows.UI.Xaml.Automation.Peers.AutomationControlType
            /* 0x0246 */ new Dictionary<int, string> { { 0, "ToolTipOpened" }, { 1, "ToolTipClosed" }, { 2, "MenuOpened" }, { 3, "MenuClosed" }, { 4, "AutomationFocusChanged" }, { 5, "InvokePatternOnInvoked" }, { 6, "SelectionItemPatternOnElementAddedToSelection" }, { 7, "SelectionItemPatternOnElementRemovedFromSelection" }, { 8, "SelectionItemPatternOnElementSelected" }, { 9, "SelectionPatternOnInvalidated" }, { 10, "TextPatternOnTextSelectionChanged" }, { 11, "TextPatternOnTextChanged" }, { 12, "AsyncContentLoaded" }, { 13, "PropertyChanged" }, { 14, "StructureChanged" }, { 15, "DragStart" }, { 16, "DragCancel" }, { 17, "DragComplete" }, { 18, "DragEnter" }, { 19, "DragLeave" }, { 20, "Dropped" }, { 21, "LiveRegionChanged" }, { 22, "InputReachedTarget" }, { 23, "InputReachedOtherElement" }, { 24, "InputDiscarded" }, { 25, "WindowClosed" }, { 26, "WindowOpened" }, { 27, "ConversionTargetChanged" }, { 28, "TextEditTextChanged" }, { 29, "LayoutInvalidated" } }, // Windows.UI.Xaml.Automation.Peers.AutomationEvents
            /* 0x0247 */ new Dictionary<int, string> { { 0, "Off" }, { 1, "Polite" }, { 2, "Assertive" } }, // Windows.UI.Xaml.Automation.Peers.AutomationLiveSetting
            /* 0x0248 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Horizontal" }, { 2, "Vertical" } }, // Windows.UI.Xaml.Automation.Peers.AutomationOrientation
            /* 0x0249 */ new Dictionary<int, string> { { 0, "UserInput" }, { 1, "ProgrammaticChange" }, { 2, "SuggestionChosen" } }, // Windows.UI.Xaml.Controls.AutoSuggestionBoxTextChangeReason
            /* 0x024A */ new Dictionary<int, string> { { 1, "OneWay" }, { 2, "OneTime" }, { 3, "TwoWay" } }, // Windows.UI.Xaml.Data.BindingMode
            /* 0x024B */ new Dictionary<int, string> { { 0, "None" }, { 8, "IgnoreImageCache" } }, // Windows.UI.Xaml.Media.Imaging.BitmapCreateOptions
            /* 0x024C */ new Dictionary<int, string> { { 0, "Absolute" }, { 1, "RelativeToBoundingBox" } }, // Windows.UI.Xaml.Media.BrushMappingMode
            /* 0x024D */ new Dictionary<int, string> { { 0, "Release" }, { 1, "Press" }, { 2, "Hover" } }, // Windows.UI.Xaml.Controls.ClickMode
            /* 0x024E */ new Dictionary<int, string> { { 0, "Active" }, { 1, "Filling" }, { 2, "Stopped" }, { 3, "NotStarted" } }, // Windows.UI.Xaml.Media.Animation.ClockState
            /* 0x024F */ null,
            /* 0x0250 */ new Dictionary<int, string> { { 0, "Reset" }, { 1, "ItemInserted" }, { 2, "ItemRemoved" }, { 3, "ItemChanged" } }, // Windows.Foundation.Collections.CollectionChange
            /* 0x0251 */ new Dictionary<int, string> { { 0, "ScRgbLinearInterpolation" }, { 1, "SRgbLinearInterpolation" } }, // Windows.UI.Xaml.Media.ColorInterpolationMode
            /* 0x0252 */ new Dictionary<int, string> { { 0, "Application" }, { 1, "Nested" } }, // Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation
            /* 0x0253 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Primary" }, { 2, "Secondary" } }, // Windows.UI.Xaml.Controls.ContentDialogResult
            /* 0x0254 */ new Dictionary<int, string> { { 0, "Physical" }, { 1, "Logical" } }, // Windows.UI.Xaml.Media.Imaging.DecodePixelType
            /* 0x0255 */ new Dictionary<int, string> { { 0, "Top" }, { 1, "Left" }, { 2, "Bottom" }, { 3, "Right" }, { 4, "Fill" }, { 5, "None" } }, // Windows.UI.Xaml.Automation.DockPosition
            /* 0x0256 */ new Dictionary<int, string> { { 0, "EaseOut" }, { 1, "EaseIn" }, { 2, "EaseInOut" } }, // Windows.UI.Xaml.Media.Animation.EasingMode
            /* 0x0257 */ new Dictionary<int, string> { { 0, "Left" }, { 1, "Top" }, { 2, "Right" }, { 3, "Bottom" } }, // Windows.UI.Xaml.Controls.Primitives.EdgeTransitionLocation
            /* 0x0258 */ new Dictionary<int, string> { { 0, "Inherit" }, { 1, "SourceOver" }, { 2, "MinBlend" }, { 3, "DestInvert" } }, // Windows.UI.Xaml.Media.ElementCompositeMode
            /* 0x0259 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "Light" }, { 2, "Dark" } }, // Windows.UI.Xaml.ElementTheme
            /* 0x025A */ new Dictionary<int, string> { { 0, "Collapsed" }, { 1, "Expanded" }, { 2, "PartiallyExpanded" }, { 3, "LeafNode" } }, // Windows.UI.Xaml.Automation.ExpandCollapseState
            /* 0x025B */ new Dictionary<int, string> { { 0, "HoldEnd" }, { 1, "Stop" } }, // Windows.UI.Xaml.Media.Animation.FillBehavior
            /* 0x025C */ new Dictionary<int, string> { { 0, "EvenOdd" }, { 1, "Nonzero" } }, // Windows.UI.Xaml.Media.FillRule
            /* 0x025D */ new Dictionary<int, string> { { 0, "LeftToRight" }, { 1, "RightToLeft" } }, // Windows.UI.Xaml.FlowDirection
            /* 0x025E */ new Dictionary<int, string> { { 0, "Top" }, { 1, "Bottom" }, { 2, "Left" }, { 3, "Right" }, { 4, "Full" } }, // Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode
            /* 0x025F */ new Dictionary<int, string> { { 0, "Next" }, { 1, "Previous" }, { 2, "Up" }, { 3, "Down" }, { 4, "Left" }, { 5, "Right" }, { 6, "None" } }, // Windows.UI.Xaml.Input.FocusNavigationDirection
            /* 0x0260 */ new Dictionary<int, string> { { 0, "Unfocused" }, { 1, "Pointer" }, { 2, "Keyboard" }, { 3, "Programmatic" } }, // Windows.UI.Xaml.FocusState
            /* 0x0261 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "AllSmallCaps" }, { 2, "SmallCaps" }, { 3, "AllPetiteCaps" }, { 4, "PetiteCaps" }, { 5, "Unicase" }, { 6, "Titling" } }, // Windows.UI.Xaml.FontCapitals
            /* 0x0262 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "HojoKanji" }, { 2, "Jis04" }, { 3, "Jis78" }, { 4, "Jis83" }, { 5, "Jis90" }, { 6, "NlcKanji" }, { 7, "Simplified" }, { 8, "Traditional" }, { 9, "TraditionalNames" } }, // Windows.UI.Xaml.FontEastAsianLanguage
            /* 0x0263 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Full" }, { 2, "Half" }, { 3, "Proportional" }, { 4, "Quarter" }, { 5, "Third" } }, // Windows.UI.Xaml.FontEastAsianWidths
            /* 0x0264 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Stacked" }, { 2, "Slashed" } }, // Windows.UI.Xaml.FontFraction
            /* 0x0265 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Proportional" }, { 2, "Tabular" } }, // Windows.UI.Xaml.FontNumeralAlignment
            /* 0x0266 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Lining" }, { 2, "OldStyle" } }, // Windows.UI.Xaml.FontNumeralStyle
            /* 0x0267 */ new Dictionary<int, string> { { 0, "Undefined" }, { 1, "UltraCondensed" }, { 2, "ExtraCondensed" }, { 3, "Condensed" }, { 4, "SemiCondensed" }, { 5, "Normal" }, { 6, "SemiExpanded" }, { 7, "Expanded" }, { 8, "ExtraExpanded" }, { 9, "UltraExpanded" } }, // Windows.UI.Text.FontStretch
            /* 0x0268 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Oblique" }, { 2, "Italic" } }, // Windows.UI.Text.FontStyle
            /* 0x0269 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Superscript" }, { 2, "Subscript" }, { 3, "Ordinal" }, { 4, "Inferior" }, { 5, "Ruby" } }, // Windows.UI.Xaml.FontVariants
            /* 0x026A */ new Dictionary<int, string> { { 0, "Forward" }, { 1, "Backward" } }, // Windows.UI.Xaml.Controls.Primitives.GeneratorDirection
            /* 0x026B */ new Dictionary<int, string> { { 0, "None" }, { 1, "Tapped" }, { 2, "DoubleTapped" }, { 3, "RightTapped" }, { 4, "Holding" } }, // Windows.UI.Xaml.Input.GestureModes
            /* 0x026C */ new Dictionary<int, string> { { 0, "Pad" }, { 1, "Reflect" }, { 2, "Repeat" } }, // Windows.UI.Xaml.Media.GradientSpreadMethod
            /* 0x026D */ new Dictionary<int, string> { { 0, "Auto" }, { 1, "Pixel" }, { 2, "Star" } }, // Windows.UI.Xaml.GridUnitType
            /* 0x026E */ new Dictionary<int, string> { { 0, "Top" }, { 1, "Left" } }, // Windows.UI.Xaml.Controls.Primitives.GroupHeaderPlacement
            /* 0x026F */ new Dictionary<int, string> { { 0, "Started" }, { 1, "Completed" }, { 2, "Canceled" } }, // Windows.UI.Input.HoldingState
            /* 0x0270 */ new Dictionary<int, string> { { 0, "Left" }, { 1, "Center" }, { 2, "Right" }, { 3, "Stretch" } }, // Windows.UI.Xaml.HorizontalAlignment
            /* 0x0271 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Edge" } }, // Windows.UI.Xaml.Controls.IncrementalLoadingTrigger
            /* 0x0272 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "Url" }, { 5, "EmailSmtpAddress" }, { 7, "PersonalFullName" }, { 20, "CurrencyAmountAndSymbol" }, { 21, "CurrencyAmount" }, { 23, "DateMonthNumber" }, { 24, "DateDayNumber" }, { 25, "DateYear" }, { 28, "Digits" }, { 29, "Number" }, { 31, "Password" }, { 32, "TelephoneNumber" }, { 33, "TelephoneCountryCode" }, { 34, "TelephoneAreaCode" }, { 35, "TelephoneLocalNumber" }, { 37, "TimeHour" }, { 38, "TimeMinutesOrSeconds" }, { 39, "NumberFullWidth" }, { 40, "AlphanumericHalfWidth" }, { 41, "AlphanumericFullWidth" }, { 44, "Hiragana" }, { 45, "KatakanaHalfWidth" }, { 46, "KatakanaFullWidth" }, { 47, "Hanja" }, { 48, "HangulHalfWidth" }, { 49, "HangulFullWidth" }, { 50, "Search" }, { 51, "Formula" }, { 52, "SearchIncremental" }, { 53, "ChineseHalfWidth" }, { 54, "ChineseFullWidth" }, { 55, "NativeScript" }, { 57, "Text" }, { 58, "Chat" }, { 59, "NameOrPhoneNumber" }, { 60, "EmailNameOrAddress" }, { 62, "Maps" }, { 63, "NumericPassword" }, { 64, "NumericPin" }, { 65, "AlphanumericPin" }, { 67, "FormulaNumber" }, { 68, "ChatWithoutEmoji" } }, // Windows.UI.Xaml.Input.InputScopeNameValue
            /* 0x0273 */ new Dictionary<int, string> { { 0, "KeepItemsInView" }, { 1, "KeepScrollOffset" }, { 2, "KeepLastItemInView" } }, // Windows.UI.Xaml.Controls.ItemsUpdatingScrollMode
            /* 0x0274 */ new Dictionary<int, string> { { 0, "Local" }, { 1, "Cycle" }, { 2, "Once" } }, // Windows.UI.Xaml.Input.KeyboardNavigationMode
            /* 0x0275 */ new Dictionary<int, string> { { 0, "MaxHeight" }, { 1, "BlockLineHeight" }, { 2, "BaselineToBaseline" } }, // Windows.UI.Xaml.LineStackingStrategy
            /* 0x0276 */ new Dictionary<int, string> { { 0, "Disabled" }, { 1, "Enabled" } }, // Windows.UI.Xaml.Controls.ListViewReorderMode
            /* 0x0277 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Single" }, { 2, "Multiple" }, { 3, "Extended" } }, // Windows.UI.Xaml.Controls.ListViewSelectionMode
            /* 0x0278 */ new Dictionary<int, string> { { 0, "Backward" }, { 1, "Forward" } }, // Windows.UI.Xaml.Documents.LogicalDirection
            /* 0x0279 */ new Dictionary<int, string> { { 0, "None" }, { 1, "TranslateX" }, { 2, "TranslateY" }, { 4, "TranslateRailsX" }, { 8, "TranslateRailsY" }, { 16, "Rotate" }, { 32, "Scale" }, { 64, "TranslateInertia" }, { 128, "RotateInertia" }, { 256, "ScaleInertia" }, { 65535, "All" }, { 65536, "System" } }, // Windows.UI.Xaml.Input.ManipulationModes
            /* 0x027A */ new Dictionary<int, string> { { 0, "None" }, { 1, "Extension" }, { 2, "Binding" } }, // Windows.UI.Xaml.MarkupExtensionType
            /* 0x027B */ new Dictionary<int, string> { { 0, "NotSupported" }, { 1, "Maybe" }, { 2, "Probably" } }, // Windows.UI.Xaml.Media.MediaCanPlayResponse
            /* 0x027C */ new Dictionary<int, string> { { 0, "Closed" }, { 1, "Opening" }, { 2, "Buffering" }, { 3, "Playing" }, { 4, "Paused" }, { 5, "Stopped" } }, // Windows.UI.Xaml.Media.MediaElementState
            /* 0x027D */ new Dictionary<int, string> { { 0, "Disabled" }, { 1, "Required" }, { 2, "Enabled" } }, // Windows.UI.Xaml.Navigation.NavigationCacheMode
            /* 0x027E */ new Dictionary<int, string> { { 0, "New" }, { 1, "Back" }, { 2, "Forward" }, { 3, "Refresh" } }, // Windows.UI.Xaml.Navigation.NavigationMode
            /* 0x027F */ new Dictionary<int, string> { { 0, "Add" }, { 1, "Remove" }, { 2, "Replace" }, { 3, "Move" }, { 4, "Reset" } }, // Windows.UI.Xaml.Interop.NotifyCollectionChangedAction
            /* 0x0280 */ new Dictionary<int, string> { { 0, "None" }, { 1, "TrimSideBearings" } }, // Windows.UI.Xaml.OpticalMarginAlignment
            /* 0x0281 */ new Dictionary<int, string> { { 0, "Vertical" }, { 1, "Horizontal" } }, // Windows.UI.Xaml.Controls.Orientation
            /* 0x0282 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Forward" }, { 2, "Backward" } }, // Windows.UI.Xaml.Controls.PanelScrollingDirection
            /* 0x0283 */ new Dictionary<int, string> { { 0, "Invoke" }, { 1, "Selection" }, { 2, "Value" }, { 3, "RangeValue" }, { 4, "Scroll" }, { 5, "ScrollItem" }, { 6, "ExpandCollapse" }, { 7, "Grid" }, { 8, "GridItem" }, { 9, "MultipleView" }, { 10, "Window" }, { 11, "SelectionItem" }, { 12, "Dock" }, { 13, "Table" }, { 14, "TableItem" }, { 15, "Toggle" }, { 16, "Transform" }, { 17, "Text" }, { 18, "ItemContainer" }, { 19, "VirtualizedItem" }, { 20, "Text2" }, { 21, "TextChild" }, { 22, "TextRange" }, { 23, "Annotation" }, { 24, "Drag" }, { 25, "DropTarget" }, { 26, "ObjectModel" }, { 27, "Spreadsheet" }, { 28, "SpreadsheetItem" }, { 29, "Styles" }, { 30, "Transform2" }, { 31, "SynchronizedInput" }, { 32, "TextEdit" }, { 33, "CustomNavigation" }, { 34, "SeeitSayit" } }, // Windows.UI.Xaml.Automation.Peers.PatternInterface
            /* 0x0284 */ new Dictionary<int, string> { { 0, "Flat" }, { 1, "Square" }, { 2, "Round" }, { 3, "Triangle" } }, // Windows.UI.Xaml.Media.PenLineCap
            /* 0x0285 */ new Dictionary<int, string> { { 0, "Miter" }, { 1, "Bevel" }, { 2, "Round" } }, // Windows.UI.Xaml.Media.PenLineJoin
            /* 0x0286 */ new Dictionary<int, string> { { 2, "Bottom" }, { 4, "Right" }, { 7, "Mouse" }, { 9, "Left" }, { 10, "Top" } }, // Windows.UI.Xaml.Controls.Primitives.PlacementMode
            /* 0x0287 */ new Dictionary<int, string> { { 0, "Touch" }, { 1, "Pen" }, { 2, "Mouse" } }, // Windows.Devices.Input.PointerDeviceType
            /* 0x0288 */ new Dictionary<int, string> { { 0, "PointerDirection_XAxis" }, { 1, "PointerDirection_YAxis" }, { 2, "PointerDirection_BothAxes" } }, // Windows.UI.Xaml.Internal.PointerDirection
            /* 0x0289 */ new Dictionary<int, string> { { 0, "Final" }, { 1, "Intermediate" } }, // Windows.UI.Xaml.Printing.PreviewPageCountType
            /* 0x028A */ new Dictionary<int, string> { { 0, "Bitmap" }, { 1, "Vector" } }, // Windows.UI.Xaml.Printing.PrintDocumentFormat
            /* 0x028B */ new Dictionary<int, string> { { 0, "None" }, { 1, "TemplatedParent" }, { 2, "Self" } }, // Windows.UI.Xaml.Data.RelativeSourceMode
            /* 0x028C */ new Dictionary<int, string> { { 0, "RowMajor" }, { 1, "ColumnMajor" }, { 2, "Indeterminate" } }, // Windows.UI.Xaml.Automation.RowOrColumnMajor
            /* 0x028D */ new Dictionary<int, string> { { 0, "LargeDecrement" }, { 1, "SmallDecrement" }, { 2, "NoAmount" }, { 3, "LargeIncrement" }, { 4, "SmallIncrement" } }, // Windows.UI.Xaml.Automation.ScrollAmount
            /* 0x028E */ new Dictionary<int, string> { { 0, "Disabled" }, { 1, "Auto" }, { 2, "Hidden" }, { 3, "Visible" } }, // Windows.UI.Xaml.Controls.ScrollBarVisibility
            /* 0x028F */ new Dictionary<int, string> { { 0, "SmallDecrement" }, { 1, "SmallIncrement" }, { 2, "LargeDecrement" }, { 3, "LargeIncrement" }, { 4, "ThumbPosition" }, { 5, "ThumbTrack" }, { 6, "First" }, { 7, "Last" }, { 8, "EndScroll" } }, // Windows.UI.Xaml.Controls.Primitives.ScrollEventType
            /* 0x0290 */ new Dictionary<int, string> { { 0, "None" }, { 1, "TouchIndicator" }, { 2, "MouseIndicator" } }, // Windows.UI.Xaml.Controls.Primitives.ScrollingIndicatorMode
            /* 0x0291 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "Leading" } }, // Windows.UI.Xaml.Controls.ScrollIntoViewAlignment
            /* 0x0292 */ new Dictionary<int, string> { { 0, "Disabled" }, { 1, "Enabled" }, { 2, "Auto" } }, // Windows.UI.Xaml.Controls.ScrollMode
            /* 0x0293 */ new Dictionary<int, string> { { 0, "Single" }, { 1, "Multiple" }, { 2, "Extended" } }, // Windows.UI.Xaml.Controls.SelectionMode
            /* 0x0294 */ new Dictionary<int, string> { { 0, "StepValues" }, { 1, "Ticks" } }, // Windows.UI.Xaml.Controls.Primitives.SliderSnapsTo
            /* 0x0295 */ new Dictionary<int, string> { { 0, "Near" }, { 1, "Center" }, { 2, "Far" } }, // Windows.UI.Xaml.Controls.Primitives.SnapPointsAlignment
            /* 0x0296 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Optional" }, { 2, "Mandatory" }, { 3, "OptionalSingle" }, { 4, "MandatorySingle" } }, // Windows.UI.Xaml.Controls.SnapPointsType
            /* 0x0297 */ new Dictionary<int, string> { { 0, "None" }, { 1, "SideBySide" }, { 2, "TopBottom" } }, // Windows.UI.Xaml.Media.Stereo3DVideoPackingMode
            /* 0x0298 */ new Dictionary<int, string> { { 0, "Mono" }, { 1, "Stereo" } }, // Windows.UI.Xaml.Media.Stereo3DVideoRenderMode
            /* 0x0299 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Fill" }, { 2, "Uniform" }, { 3, "UniformToFill" } }, // Windows.UI.Xaml.Media.Stretch
            /* 0x029A */ new Dictionary<int, string> { { 0, "UpOnly" }, { 1, "DownOnly" }, { 2, "Both" } }, // Windows.UI.Xaml.Controls.StretchDirection
            /* 0x029B */ new Dictionary<int, string> { { 0, "None" }, { 1, "BoldSimulation" }, { 2, "ItalicSimulation" }, { 3, "BoldItalicSimulation" } }, // Windows.UI.Xaml.Media.StyleSimulations
            /* 0x029C */ new Dictionary<int, string> { { 0, "None" }, { 1, "Single" }, { 2, "Multiple" } }, // Windows.UI.Xaml.Automation.SupportedTextSelection
            /* 0x029D */ new Dictionary<int, string> { { 0, "Counterclockwise" }, { 1, "Clockwise" } }, // Windows.UI.Xaml.Media.SweepDirection
            /* 0x029E */ new Dictionary<int, string> { { 57600, "Previous" }, { 57601, "Next" }, { 57602, "Play" }, { 57603, "Pause" }, { 57604, "Edit" }, { 57605, "Save" }, { 57606, "Clear" }, { 57607, "Delete" }, { 57608, "Remove" }, { 57609, "Add" }, { 57610, "Cancel" }, { 57611, "Accept" }, { 57612, "More" }, { 57613, "Redo" }, { 57614, "Undo" }, { 57615, "Home" }, { 57616, "Up" }, { 57617, "Forward" }, { 57618, "Back" }, { 57619, "Favorite" }, { 57620, "Camera" }, { 57621, "Setting" }, { 57622, "Video" }, { 57623, "Sync" }, { 57624, "Download" }, { 57625, "Mail" }, { 57626, "Find" }, { 57627, "Help" }, { 57628, "Upload" }, { 57629, "Emoji" }, { 57630, "TwoPage" }, { 57631, "LeaveChat" }, { 57632, "MailForward" }, { 57633, "Clock" }, { 57634, "Send" }, { 57635, "Crop" }, { 57636, "RotateCamera" }, { 57637, "People" }, { 57638, "OpenPane" }, { 57639, "ClosePane" }, { 57640, "World" }, { 57641, "Flag" }, { 57642, "PreviewLink" }, { 57643, "Globe" }, { 57644, "Trim" }, { 57645, "AttachCamera" }, { 57646, "ZoomIn" }, { 57647, "Bookmarks" }, { 57648, "Document" }, { 57649, "ProtectedDocument" }, { 57650, "Page" }, { 57651, "Bullets" }, { 57652, "Comment" }, { 57653, "MailFilled" }, { 57654, "ContactInfo" }, { 57655, "HangUp" }, { 57656, "ViewAll" }, { 57657, "MapPin" }, { 57658, "Phone" }, { 57659, "VideoChat" }, { 57660, "Switch" }, { 57661, "Contact" }, { 57662, "Rename" }, { 57665, "Pin" }, { 57666, "MusicInfo" }, { 57667, "Go" }, { 57668, "Keyboard" }, { 57669, "DockLeft" }, { 57670, "DockRight" }, { 57671, "DockBottom" }, { 57672, "Remote" }, { 57673, "Refresh" }, { 57674, "Rotate" }, { 57675, "Shuffle" }, { 57676, "List" }, { 57677, "Shop" }, { 57678, "SelectAll" }, { 57679, "Orientation" }, { 57680, "Import" }, { 57681, "ImportAll" }, { 57685, "BrowsePhotos" }, { 57686, "WebCam" }, { 57688, "Pictures" }, { 57689, "SaveLocal" }, { 57690, "Caption" }, { 57691, "Stop" }, { 57692, "ShowResults" }, { 57693, "Volume" }, { 57694, "Repair" }, { 57695, "Message" }, { 57696, "Page2" }, { 57697, "CalendarDay" }, { 57698, "CalendarWeek" }, { 57699, "Calendar" }, { 57700, "Character" }, { 57701, "MailReplyAll" }, { 57702, "Read" }, { 57703, "Link" }, { 57704, "Account" }, { 57705, "ShowBcc" }, { 57706, "HideBcc" }, { 57707, "Cut" }, { 57708, "Attach" }, { 57709, "Paste" }, { 57710, "Filter" }, { 57711, "Copy" }, { 57712, "Emoji2" }, { 57713, "Important" }, { 57714, "MailReply" }, { 57715, "SlideShow" }, { 57716, "Sort" }, { 57720, "Manage" }, { 57721, "AllApps" }, { 57722, "DisconnectDrive" }, { 57723, "MapDrive" }, { 57724, "NewWindow" }, { 57725, "OpenWith" }, { 57729, "ContactPresence" }, { 57730, "Priority" }, { 57732, "GoToToday" }, { 57733, "Font" }, { 57734, "FontColor" }, { 57735, "Contact2" }, { 57736, "Folder" }, { 57737, "Audio" }, { 57738, "Placeholder" }, { 57739, "View" }, { 57740, "SetLockScreen" }, { 57741, "SetTile" }, { 57744, "ClosedCaption" }, { 57745, "StopSlideShow" }, { 57746, "Permissions" }, { 57747, "Highlight" }, { 57748, "DisableUpdates" }, { 57749, "UnFavorite" }, { 57750, "UnPin" }, { 57751, "OpenLocal" }, { 57752, "Mute" }, { 57753, "Italic" }, { 57754, "Underline" }, { 57755, "Bold" }, { 57756, "MoveToFolder" }, { 57757, "LikeDislike" }, { 57758, "Dislike" }, { 57759, "Like" }, { 57760, "AlignRight" }, { 57761, "AlignCenter" }, { 57762, "AlignLeft" }, { 57763, "Zoom" }, { 57764, "ZoomOut" }, { 57765, "OpenFile" }, { 57766, "OtherUser" }, { 57767, "Admin" }, { 57795, "Street" }, { 57796, "Map" }, { 57797, "ClearSelection" }, { 57798, "FontDecrease" }, { 57799, "FontIncrease" }, { 57800, "FontSize" }, { 57801, "CellPhone" }, { 57802, "ReShare" }, { 57803, "Tag" }, { 57804, "RepeatOne" }, { 57805, "RepeatAll" }, { 57806, "OutlineStar" }, { 57807, "SolidStar" }, { 57808, "Calculator" }, { 57809, "Directions" }, { 57810, "Target" }, { 57811, "Library" }, { 57812, "PhoneBook" }, { 57813, "Memo" }, { 57814, "Microphone" }, { 57815, "PostUpdate" }, { 57816, "BackToWindow" }, { 57817, "FullScreen" }, { 57818, "NewFolder" }, { 57819, "CalendarReply" }, { 57821, "UnSyncFolder" }, { 57822, "ReportHacked" }, { 57823, "SyncFolder" }, { 57824, "BlockContact" }, { 57825, "SwitchApps" }, { 57826, "AddFriend" }, { 57827, "TouchPointer" }, { 57828, "GoToStart" }, { 57829, "ZeroBars" }, { 57830, "OneBar" }, { 57831, "TwoBars" }, { 57832, "ThreeBars" }, { 57833, "FourBars" }, { 58004, "Scan" }, { 58005, "Preview" } }, // Windows.UI.Xaml.Controls.Symbol
            /* 0x029F */ new Dictionary<int, string> { { 1, "KeyUp" }, { 2, "KeyDown" }, { 4, "LeftMouseUp" }, { 8, "LeftMouseDown" }, { 16, "RightMouseUp" }, { 32, "RightMouseDown" } }, // Windows.UI.Xaml.Automation.SynchronizedInputType
            /* 0x02A0 */ new Dictionary<int, string> { { 0, "Center" }, { 1, "Left" }, { 2, "Right" }, { 3, "Justify" }, { 4, "DetectFromContent" } }, // Windows.UI.Xaml.TextAlignment
            /* 0x02A1 */ new Dictionary<int, string> { { 0, "Ideal" }, { 1, "Display" } }, // Windows.UI.Xaml.Media.TextFormattingMode
            /* 0x02A2 */ new Dictionary<int, string> { { 0, "Fixed" }, { 1, "Animated" } }, // Windows.UI.Xaml.Media.TextHintingMode
            /* 0x02A3 */ new Dictionary<int, string> { { 0, "Full" }, { 1, "TrimToCapHeight" }, { 2, "TrimToBaseline" }, { 3, "Tight" } }, // Windows.UI.Xaml.TextLineBounds
            /* 0x02A4 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "DetectFromContent" } }, // Windows.UI.Xaml.TextReadingOrder
            /* 0x02A5 */ new Dictionary<int, string> { { 0, "Auto" }, { 1, "Aliased" }, { 2, "Grayscale" }, { 3, "ClearType" } }, // Windows.UI.Xaml.Media.TextRenderingMode
            /* 0x02A6 */ new Dictionary<int, string> { { 0, "None" }, { 1, "CharacterEllipsis" }, { 2, "WordEllipsis" }, { 3, "Clip" } }, // Windows.UI.Xaml.TextTrimming
            /* 0x02A7 */ new Dictionary<int, string> { { 1, "NoWrap" }, { 2, "Wrap" }, { 3, "WrapWholeWords" } }, // Windows.UI.Xaml.TextWrapping
            /* 0x02A8 */ new Dictionary<int, string> { { 0, "None" }, { 1, "TopLeft" }, { 2, "BottomRight" }, { 3, "Outside" }, { 4, "Inline" } }, // Windows.UI.Xaml.Controls.Primitives.TickPlacement
            /* 0x02A9 */ new Dictionary<int, string> { { 0, "Off" }, { 1, "On" }, { 2, "Indeterminate" } }, // Windows.UI.Xaml.Automation.ToggleState
            /* 0x02AA */ new Dictionary<int, string> { { 0, "Primitive" }, { 1, "Metadata" }, { 2, "Custom" } }, // Windows.UI.Xaml.Interop.TypeKind
            /* 0x02AB */ new Dictionary<int, string> { { 0, "Default" }, { 1, "PropertyChanged" }, { 2, "Explicit" } }, // Windows.UI.Xaml.Data.UpdateSourceTrigger
            /* 0x02AC */ new Dictionary<int, string> { { 0, "Top" }, { 1, "Center" }, { 2, "Bottom" }, { 3, "Stretch" } }, // Windows.UI.Xaml.VerticalAlignment
            /* 0x02AD */ new Dictionary<int, string> { { 0, "Standard" }, { 1, "Recycling" } }, // Windows.UI.Xaml.Controls.VirtualizationMode
            /* 0x02AE */ new Dictionary<int, string> { { 0, "None" }, { 1, "LeftButton" }, { 2, "RightButton" }, { 3, "Cancel" }, { 4, "MiddleButton" }, { 5, "XButton1" }, { 6, "XButton2" }, { 8, "Back" }, { 9, "Tab" }, { 12, "Clear" }, { 13, "Enter" }, { 16, "Shift" }, { 17, "Control" }, { 18, "Menu" }, { 19, "Pause" }, { 20, "CapitalLock" }, { 21, "Kana" }, { 23, "Junja" }, { 24, "Final" }, { 25, "Kanji" }, { 27, "Escape" }, { 28, "Convert" }, { 29, "NonConvert" }, { 30, "Accept" }, { 31, "ModeChange" }, { 32, "Space" }, { 33, "PageUp" }, { 34, "PageDown" }, { 35, "End" }, { 36, "Home" }, { 37, "Left" }, { 38, "Up" }, { 39, "Right" }, { 40, "Down" }, { 41, "Select" }, { 42, "Print" }, { 43, "Execute" }, { 44, "Snapshot" }, { 45, "Insert" }, { 46, "Delete" }, { 47, "Help" }, { 48, "Number0" }, { 49, "Number1" }, { 50, "Number2" }, { 51, "Number3" }, { 52, "Number4" }, { 53, "Number5" }, { 54, "Number6" }, { 55, "Number7" }, { 56, "Number8" }, { 57, "Number9" }, { 65, "A" }, { 66, "B" }, { 67, "C" }, { 68, "D" }, { 69, "E" }, { 70, "F" }, { 71, "G" }, { 72, "H" }, { 73, "I" }, { 74, "J" }, { 75, "K" }, { 76, "L" }, { 77, "M" }, { 78, "N" }, { 79, "O" }, { 80, "P" }, { 81, "Q" }, { 82, "R" }, { 83, "S" }, { 84, "T" }, { 85, "U" }, { 86, "V" }, { 87, "W" }, { 88, "X" }, { 89, "Y" }, { 90, "Z" }, { 91, "LeftWindows" }, { 92, "RightWindows" }, { 93, "Application" }, { 95, "Sleep" }, { 96, "NumberPad0" }, { 97, "NumberPad1" }, { 98, "NumberPad2" }, { 99, "NumberPad3" }, { 100, "NumberPad4" }, { 101, "NumberPad5" }, { 102, "NumberPad6" }, { 103, "NumberPad7" }, { 104, "NumberPad8" }, { 105, "NumberPad9" }, { 106, "Multiply" }, { 107, "Add" }, { 108, "Separator" }, { 109, "Subtract" }, { 110, "Decimal" }, { 111, "Divide" }, { 112, "F1" }, { 113, "F2" }, { 114, "F3" }, { 115, "F4" }, { 116, "F5" }, { 117, "F6" }, { 118, "F7" }, { 119, "F8" }, { 120, "F9" }, { 121, "F10" }, { 122, "F11" }, { 123, "F12" }, { 124, "F13" }, { 125, "F14" }, { 126, "F15" }, { 127, "F16" }, { 128, "F17" }, { 129, "F18" }, { 130, "F19" }, { 131, "F20" }, { 132, "F21" }, { 133, "F22" }, { 134, "F23" }, { 135, "F24" }, { 144, "NumberKeyLock" }, { 145, "Scroll" }, { 160, "LeftShift" }, { 161, "RightShift" }, { 162, "LeftControl" }, { 163, "RightControl" }, { 164, "LeftMenu" }, { 165, "RightMenu" } }, // Windows.System.VirtualKey
            /* 0x02AF */ new Dictionary<int, string> { { 0, "None" }, { 1, "Control" }, { 2, "Menu" }, { 4, "Shift" }, { 8, "Windows" } }, // Windows.System.VirtualKeyModifiers
            /* 0x02B0 */ new Dictionary<int, string> { { 0, "Visible" }, { 1, "Collapsed" } }, // Windows.UI.Xaml.Visibility
            /* 0x02B1 */ new Dictionary<int, string> { { 0, "Running" }, { 1, "Closing" }, { 2, "ReadyForUserInteraction" }, { 3, "BlockedByModalWindow" }, { 4, "NotResponding" } }, // Windows.UI.Xaml.Automation.WindowInteractionState
            /* 0x02B2 */ new Dictionary<int, string> { { 0, "Normal" }, { 1, "Maximized" }, { 2, "Minimized" } }, // Windows.UI.Xaml.Automation.WindowVisualState
            /* 0x02B3 */ new Dictionary<int, string> { { 0, "Disabled" }, { 1, "Enabled" } }, // Windows.UI.Xaml.Controls.ZoomMode
            /* 0x02B4 */ new Dictionary<int, string> { { 0, "NoAmount" }, { 1, "LargeDecrement" }, { 2, "SmallDecrement" }, { 3, "LargeIncrement" }, { 4, "SmallIncrement" } }, // Windows.UI.Xaml.Automation.ZoomUnit
            /* 0x02B5 */ null,
            /* 0x02B6 */ null,
            /* 0x02B7 */ null,
            /* 0x02B8 */ null,
            /* 0x02B9 */ null,
            /* 0x02BA */ null,
            /* 0x02BB */ null,
            /* 0x02BC */ null,
            /* 0x02BD */ null,
            /* 0x02BE */ null,
            /* 0x02BF */ new Dictionary<int, string> { { 0, "Parent" }, { 1, "NextSibling" }, { 2, "PreviousSibling" }, { 3, "FirstChild" }, { 4, "LastChild" } }, // Windows.UI.Xaml.Automation.Peers.AutomationNavigationDirection
            /* 0x02C0 */ null,
            /* 0x02C1 */ null,
            /* 0x02C2 */ null,
            /* 0x02C3 */ null,
            /* 0x02C4 */ null,
            /* 0x02C5 */ null,
            /* 0x02C6 */ null,
            /* 0x02C7 */ new Dictionary<int, string> { { 0, "Month" }, { 1, "Year" }, { 2, "Decade" } }, // Windows.UI.Xaml.Controls.CalendarViewDisplayMode
            /* 0x02C8 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Single" }, { 2, "Multiple" } }, // Windows.UI.Xaml.Controls.CalendarViewSelectionMode
            /* 0x02C9 */ new Dictionary<int, string> { { 0, "Sunday" }, { 1, "Monday" }, { 2, "Tuesday" }, { 3, "Wednesday" }, { 4, "Thursday" }, { 5, "Friday" }, { 6, "Saturday" } }, // Windows.Globalization.DayOfWeek
            /* 0x02CA */ null,
            /* 0x02CB */ null,
            /* 0x02CC */ new Dictionary<int, string> { { 0, "None" }, { 1, "Copy" }, { 2, "Move" }, { 4, "Link" } }, // Windows.ApplicationModel.DataTransfer.DataPackageOperation
            /* 0x02CD */ null,
            /* 0x02CE */ null,
            /* 0x02CF */ null,
            /* 0x02D0 */ null,
            /* 0x02D1 */ null,
            /* 0x02D2 */ null,
            /* 0x02D3 */ null,
            /* 0x02D4 */ null,
            /* 0x02D5 */ null,
            /* 0x02D6 */ null,
            /* 0x02D7 */ null,
            /* 0x02D8 */ null,
            /* 0x02D9 */ new Dictionary<int, string> { { 0, "Overlay" }, { 1, "Inline" }, { 2, "CompactOverlay" }, { 3, "CompactInline" } }, // Windows.UI.Xaml.Controls.SplitViewDisplayMode
            /* 0x02DA */ new Dictionary<int, string> { { 0, "Left" }, { 1, "Right" } }, // Windows.UI.Xaml.Controls.SplitViewPanePlacement
            /* 0x02DB */ null,
            /* 0x02DC */ null,
            /* 0x02DD */ null,
            /* 0x02DE */ new Dictionary<int, string> { { 0, "None" }, { 1, "Start" }, { 2, "End" } }, // Windows.UI.Xaml.Automation.AutomationActiveEnd
            /* 0x02DF */ new Dictionary<int, string> { { 0, "None" }, { 1, "LasVegasLights" }, { 2, "BlinkingBackground" }, { 3, "SparkleText" }, { 4, "MarchingBlackAnts" }, { 5, "MarchingRedAnts" }, { 6, "Shimmer" }, { 7, "Other" } }, // Windows.UI.Xaml.Automation.AutomationAnimationStyle
            /* 0x02E0 */ new Dictionary<int, string> { { 0, "None" }, { 1, "HollowRoundBullet" }, { 2, "FilledRoundBullet" }, { 3, "HollowSquareBullet" }, { 4, "FilledSquareBullet" }, { 5, "DashBullet" }, { 6, "Other" } }, // Windows.UI.Xaml.Automation.AutomationBulletStyle
            /* 0x02E1 */ new Dictionary<int, string> { { 0, "LTR" }, { 1, "RTL" } }, // Windows.UI.Xaml.Automation.AutomationCaretBidiMode
            /* 0x02E2 */ new Dictionary<int, string> { { 0, "Unknown" }, { 1, "EndOfLine" }, { 2, "BeginningOfLine" } }, // Windows.UI.Xaml.Automation.AutomationCaretPosition
            /* 0x02E3 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "RightToLeft" }, { 2, "BottomToTop" }, { 3, "Vertical" } }, // Windows.UI.Xaml.Automation.AutomationFlowDirections
            /* 0x02E4 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Outline" }, { 2, "Shadow" }, { 3, "Engraved" }, { 4, "Embossed" } }, // Windows.UI.Xaml.Automation.AutomationOutlineStyles
            /* 0x02E5 */ new Dictionary<int, string> { { 70001, "Heading1" }, { 70002, "Heading2" }, { 70003, "Heading3" }, { 70004, "Heading4" }, { 70005, "Heading5" }, { 70006, "Heading6" }, { 70007, "Heading7" }, { 70008, "Heading8" }, { 70009, "Heading9" }, { 70010, "Title" }, { 70011, "Subtitle" }, { 70012, "Normal" }, { 70013, "Emphasis" }, { 70014, "Quote" }, { 70015, "BulletedList" } }, // Windows.UI.Xaml.Automation.AutomationStyleId
            /* 0x02E6 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Single" }, { 2, "WordsOnly" }, { 3, "Double" }, { 4, "Dot" }, { 5, "Dash" }, { 6, "DashDot" }, { 7, "DashDotDot" }, { 8, "Wavy" }, { 9, "ThickSingle" }, { 10, "DoubleWavy" }, { 11, "ThickWavy" }, { 12, "LongDash" }, { 13, "ThickDash" }, { 14, "ThickDashDot" }, { 15, "ThickDashDotDot" }, { 16, "ThickDot" }, { 17, "ThickLongDash" }, { 18, "Other" } }, // Windows.UI.Xaml.Automation.AutomationTextDecorationLineStyle
            /* 0x02E7 */ new Dictionary<int, string> { { 0, "None" }, { 1, "AutoCorrect" }, { 2, "Composition" }, { 3, "CompositionFinalized" } }, // Windows.UI.Xaml.Automation.AutomationTextEditChangeType
            /* 0x02E8 */ null,
            /* 0x02E9 */ null,
            /* 0x02EA */ null,
            /* 0x02EB */ null,
            /* 0x02EC */ null,
            /* 0x02ED */ null,
            /* 0x02EE */ new Dictionary<int, string> { { 0, "ChildAdded" }, { 1, "ChildRemoved" }, { 2, "ChildrenInvalidated" }, { 3, "ChildrenBulkAdded" }, { 4, "ChildrenBulkRemoved" }, { 5, "ChildrenReordered" } }, // Windows.UI.Xaml.Automation.Peers.AutomationStructureChangeType
            /* 0x02EF */ new Dictionary<int, string> { { 0, "Peek" }, { 1, "Hidden" }, { 2, "Visible" } }, // Windows.UI.Xaml.Controls.PasswordRevealMode
            /* 0x02F0 */ null,
            /* 0x02F1 */ new Dictionary<int, string> { { 0, "Unknown" }, { 1, "Audio" }, { 2, "Video" } }, // Windows.Media.Playback.FailedMediaStreamKind
            /* 0x02F2 */ null,
            /* 0x02F3 */ null,
            /* 0x02F4 */ null,
            /* 0x02F5 */ null,
            /* 0x02F6 */ null,
            /* 0x02F7 */ null,
            /* 0x02F8 */ new Dictionary<int, string> { { 0, "Inline" }, { 1, "Overlay" } }, // Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenterCheckMode
            /* 0x02F9 */ null,
            /* 0x02FA */ null,
            /* 0x02FB */ null,
            /* 0x02FC */ null,
            /* 0x02FD */ null,
            /* 0x02FE */ null,
            /* 0x02FF */ null,
            /* 0x0300 */ new Dictionary<int, string> { { 0, "SameThread" }, { 1, "SeparateThread" } }, // Windows.UI.Xaml.Controls.WebViewExecutionMode
            /* 0x0301 */ null,
            /* 0x0302 */ new Dictionary<int, string> { { 0, "Unknown" }, { 1, "Defer" }, { 2, "Allow" }, { 3, "Deny" } }, // Windows.UI.Xaml.Controls.WebViewPermissionState
            /* 0x0303 */ new Dictionary<int, string> { { 0, "Geolocation" }, { 1, "UnlimitedIndexedDBQuota" }, { 2, "Media" }, { 3, "PointerLock" }, { 4, "WebNotifications" } }, // Windows.UI.Xaml.Controls.WebViewPermissionType
            /* 0x0304 */ null,
            /* 0x0305 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "BottomEdge" } }, // Windows.UI.Xaml.Controls.CandidateWindowAlignment
            /* 0x0306 */ null,
            /* 0x0307 */ null,
            /* 0x0308 */ null,
            /* 0x0309 */ null,
            /* 0x030A */ null,
            /* 0x030B */ null,
            /* 0x030C */ null,
            /* 0x030D */ null,
            /* 0x030E */ null,
            /* 0x030F */ null,
            /* 0x0310 */ null,
            /* 0x0311 */ null,
            /* 0x0312 */ null,
            /* 0x0313 */ null,
            /* 0x0314 */ null,
            /* 0x0315 */ null,
            /* 0x0316 */ null,
            /* 0x0317 */ null,
            /* 0x0318 */ null,
            /* 0x0319 */ null,
            /* 0x031A */ null,
            /* 0x031B */ null,
            /* 0x031C */ null,
            /* 0x031D */ new Dictionary<int, string> { { 0, "None" }, { 1, "Single" } }, // Windows.UI.Xaml.Documents.UnderlineStyle
            /* 0x031E */ null,
            /* 0x031F */ new Dictionary<int, string> { { 0, "AllFormats" }, { 1, "PlainText" } }, // Windows.UI.Xaml.Controls.RichEditClipboardFormat
            /* 0x0320 */ null,
            /* 0x0321 */ null,
            /* 0x0322 */ null,
            /* 0x0323 */ null,
            /* 0x0324 */ new Dictionary<int, string> { { 0, "None" }, { 1, "Custom" }, { 2, "Form" }, { 3, "Main" }, { 4, "Navigation" }, { 5, "Search" } }, // Windows.UI.Xaml.Automation.Peers.AutomationLandmarkType
            /* 0x0325 */ null,
            /* 0x0326 */ null,
            /* 0x0327 */ null,
            /* 0x0328 */ null,
            /* 0x0329 */ null,
            /* 0x032A */ new Dictionary<int, string> { { 0, "Default" }, { 1, "Collapsed" } }, // Windows.UI.Xaml.Controls.CommandBarLabelPosition
            /* 0x032B */ null,
            /* 0x032C */ new Dictionary<int, string> { { 0, "Bottom" }, { 1, "Right" }, { 2, "Collapsed" } }, // Windows.UI.Xaml.Controls.CommandBarDefaultLabelPosition
            /* 0x032D */ null,
            /* 0x032E */ new Dictionary<int, string> { { 0, "Auto" }, { 1, "Visible" }, { 2, "Collapsed" } }, // Windows.UI.Xaml.Controls.CommandBarOverflowButtonVisibility
            /* 0x032F */ null,
            /* 0x0330 */ null,
            /* 0x0331 */ null,
            /* 0x0332 */ null,
            /* 0x0333 */ new Dictionary<int, string> { { 0, "AddingToOverflow" }, { 1, "RemovingFromOverflow" } }, // Windows.UI.Xaml.Controls.CommandBarDynamicOverflowAction
            /* 0x0334 */ null,
            /* 0x0335 */ null,
            /* 0x0336 */ new Dictionary<int, string> { { 0, "Auto" }, { 1, "On" }, { 2, "Off" } }, // Windows.UI.Xaml.Controls.LightDismissOverlayMode
            /* 0x0337 */ new Dictionary<int, string> { { 0, "DottedLine" }, { 1, "HighVisibility" } }, // Windows.UI.Xaml.FocusVisualKind
            /* 0x0338 */ new Dictionary<int, string> { { 0, "Never" }, { 1, "WhenEngaged" }, { 2, "WhenFocused" } }, // Windows.UI.Xaml.Controls.RequiresPointer
            /* 0x0339 */ null,
            /* 0x033A */ null,
            /* 0x033B */ null,
            /* 0x033C */ null,
            /* 0x033D */ null,
            /* 0x033E */ new Dictionary<int, string> { { 0, "Skip" }, { 1, "Hide" }, { 2, "Disable" } }, // Windows.UI.Xaml.Media.FastPlayFallbackBehaviour
            /* 0x033F */ new Dictionary<int, string> { { 0, "Focus" }, { 1, "Invoke" }, { 2, "Show" }, { 3, "Hide" }, { 4, "MovePrevious" }, { 5, "MoveNext" }, { 6, "GoBack" } }, // Windows.UI.Xaml.ElementSoundKind
            /* 0x0340 */ new Dictionary<int, string> { { 0, "Default" }, { 1, "FocusOnly" }, { 2, "Off" } }, // Windows.UI.Xaml.ElementSoundMode
            /* 0x0341 */ new Dictionary<int, string> { { 0, "Auto" }, { 1, "Off" }, { 2, "On" } }, // Windows.UI.Xaml.ElementSoundPlayerState
            /* 0x0342 */ null,
            /* 0x0343 */ new Dictionary<int, string> { { 0, "Auto" }, { 1, "WhenRequested" } } // Windows.UI.Xaml.ApplicationRequiresPointerMode
        };
    }
}
