using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LightControl.Controls
{
    public class NavigationItem : Control
    {
        static NavigationItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationItem), new FrameworkPropertyMetadata(typeof(NavigationItem)));
        }

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(NavigationItem), new FrameworkPropertyMetadata(null));
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NavigationItem), new FrameworkPropertyMetadata(null));
        public static readonly RoutedEvent SelectedEvent =
                EventManager.RegisterRoutedEvent("Selected", RoutingStrategy.Bubble,
         typeof(RoutedEventHandler), typeof(NavigationItem));
        public event RoutedEventHandler Selected { add => AddHandler(SelectedEvent, value); remove => RemoveHandler(SelectedEvent, value); }
        public static readonly RoutedEvent MousePreviewEvent =
               EventManager.RegisterRoutedEvent("MousePreview", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(NavigationItem));
        public event RoutedEventHandler MousePreview { add => AddHandler(MousePreviewEvent, value); remove => RemoveHandler(MousePreviewEvent, value); }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (!IsSelected)
            {
                RaiseEvent(new RoutedEventArgs(SelectedEvent, this));
            }
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            RaiseEvent(new RoutedEventArgs(MousePreviewEvent, this));
        }
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(NavigationItem), new PropertyMetadata(false));
    }
}
