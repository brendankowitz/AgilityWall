using System;
using System.Collections;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;

namespace AgilityWall.WinStore.Controls
{
    public class ItemsHub : Hub, IDisposable
    {
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemsHub),
                new PropertyMetadata(null, ItemTemplateChanged));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ItemsHub),
                new PropertyMetadata(null, ItemsSourceChanged));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hub = d as ItemsHub;
            if (hub != null)
            {
                var template = e.NewValue as DataTemplate;
                if (template != null)
                {
                    foreach (var section in hub.Sections)
                    {
                        section.ContentTemplate = template;
                    }
                }
            }
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hub = d as ItemsHub;
            if (hub != null)
                hub.UpdateItemSource(e.NewValue as IList);
        }

        private IList _currentSource;
        private void UpdateItemSource(IList items)
        {
            if (_currentSource != null)
            {
                var source = _currentSource as INotifyCollectionChanged;
                if (source != null)
                    source.CollectionChanged -= SourceOnCollectionChanged;
            }

            _currentSource = items;
            UpdateItemSource();

            var newSource = items as INotifyCollectionChanged;
            if (newSource != null)
                newSource.CollectionChanged += SourceOnCollectionChanged;
        }

        private void SourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItemSource();
        }

        private void UpdateItemSource()
        {
            Sections.Clear();
            if (_currentSource != null)
            {
                foreach (var item in _currentSource)
                {
                    var header = item as IHaveDisplayName;
                    var section = new HubSection
                    {
                        DataContext = item,
                        Header = header != null ? header.DisplayName : item
                    };
                    var template = this.ItemTemplate;
                    section.ContentTemplate = template;
                    Sections.Add(section);
                }
            }
        }

        void IDisposable.Dispose()
        {
            if (_currentSource != null)
            {
                var source = _currentSource as INotifyCollectionChanged;
                if (source != null)
                    source.CollectionChanged -= SourceOnCollectionChanged;
            }
        }
    }
}