using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AgilityWall.WinPhone.Controls
{
    [TemplateVisualState(Name = "Normal", GroupName = "PageStates")]
    [TemplateVisualState(Name = "Busy", GroupName = "PageStates")]
    public class HeaderedPageLayout : ContentControl
    {
        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
            "PageTitle", typeof (string), typeof (HeaderedPageLayout), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string) GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty ApplicationTitleProperty = DependencyProperty.Register(
            "ApplicationTitle", typeof (string), typeof (HeaderedPageLayout), new PropertyMetadata(default(string)));

        public string ApplicationTitle
        {
            get { return (string) GetValue(ApplicationTitleProperty); }
            set { SetValue(ApplicationTitleProperty, value); }
        }

        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register(
            "BackgroundBrush", typeof (Brush), typeof (HeaderedPageLayout), new PropertyMetadata(default(Brush)));

        public Brush BackgroundBrush
        {
            get { return (Brush) GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }

    }
}