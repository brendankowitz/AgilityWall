using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace AgilityWall.WinStore.Controls
{
    public class HeaderedPageLayout : ContentControl
    {
        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
            "PageTitle", typeof (string), typeof (HeaderedPageLayout), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string) GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageTitleBackgroundProperty = DependencyProperty.Register(
            "PageTitleBackground", typeof (Brush), typeof (HeaderedPageLayout), new PropertyMetadata(default(Brush)));

        public Brush PageTitleBackground
        {
            get { return (Brush) GetValue(PageTitleBackgroundProperty); }
            set { SetValue(PageTitleBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof (object), typeof (HeaderedPageLayout), new PropertyMetadata(default(object)));

        public object PageIcon
        {
            get { return (object) GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
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