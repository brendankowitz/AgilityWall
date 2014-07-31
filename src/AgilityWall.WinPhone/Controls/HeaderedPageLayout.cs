using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AgilityWall.WinPhone.Controls
{
    public class HeaderedPageLayout : ContentControl
    {
        public HeaderedPageLayout()
        {
            ShowHeader = true;
        }

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
            "PageTitle", typeof(string), typeof(HeaderedPageLayout), new PropertyMetadata(default(string)));

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

        public static readonly DependencyProperty ShowHeaderProperty = DependencyProperty.Register(
            "ShowHeader", typeof (bool), typeof (HeaderedPageLayout), new PropertyMetadata(default(bool)));

        public bool ShowHeader
        {
            get { return (bool) GetValue(ShowHeaderProperty); }
            set { SetValue(ShowHeaderProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register(
            "ContentMargin", typeof(Thickness), typeof(HeaderedPageLayout), new PropertyMetadata(default(Thickness)));

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }
    }
}