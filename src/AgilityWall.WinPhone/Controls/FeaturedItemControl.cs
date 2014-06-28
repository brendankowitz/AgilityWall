using System;
using System.Windows;
using System.Windows.Controls;

namespace AgilityWall.WinPhone.Controls
{
    public class FeaturedItemControl : Control
    {
        public static readonly DependencyProperty FeaturedImageProperty = DependencyProperty.Register(
            "FeaturedImage", typeof (Uri), typeof (FeaturedItemControl), new PropertyMetadata(default(Uri)));

        public Uri FeaturedImage
        {
            get { return (Uri) GetValue(FeaturedImageProperty); }
            set { SetValue(FeaturedImageProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (string), typeof (FeaturedItemControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleStyleProperty = DependencyProperty.Register(
            "TitleStyle", typeof (Style), typeof (FeaturedItemControl), new PropertyMetadata(default(Style)));

        public Style TitleStyle
        {
            get { return (Style) GetValue(TitleStyleProperty); }
            set { SetValue(TitleStyleProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof (string), typeof (FeaturedItemControl), new PropertyMetadata(default(string)));

        public string Description
        {
            get { return (string) GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof (string), typeof (FeaturedItemControl), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }


    }
}