using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightControl.Controls
{
    public class NavigationPanel : ItemsControl
    {
        static NavigationPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPanel), new FrameworkPropertyMetadata(typeof(NavigationPanel)));
        }

        private const string ElementPanel = "PART_Panel";
        private const string PreviewRect = "PreviewRect";
        StackPanel Panel;
        Rectangle previewRect;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Panel = GetTemplateChild(ElementPanel) as StackPanel;
            previewRect = GetTemplateChild(PreviewRect) as Rectangle;
            Refresh();
            AddHandler(NavigationItem.SelectedEvent, new RoutedEventHandler(NavigationItemItemSelected));
            AddHandler(NavigationItem.MousePreviewEvent, new RoutedEventHandler(NavigationItemEnter));
        }

        private void NavigationItemEnter(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as NavigationItem;
            int index = Items.IndexOf(item);
            var targetMargin = new Thickness(index * 88, 0, 0, 0);
            ThicknessAnimation ta = new ThicknessAnimation(targetMargin, new Duration(TimeSpan.FromMilliseconds(250)));
            ta.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            previewRect.BeginAnimation(MarginProperty, ta);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            DoubleAnimation da = new DoubleAnimation(0.2, new Duration(TimeSpan.FromMilliseconds(250)));
            previewRect.BeginAnimation(OpacityProperty, da);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            DoubleAnimation da = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(250)));
            previewRect.BeginAnimation(OpacityProperty, da);
        }

        private void NavigationItemItemSelected(object sender, RoutedEventArgs e)
        {
            foreach (var item in Items)
            {
                if (item is NavigationItem element)
                {
                    element.IsSelected = false;
                }
            }

            if (e.OriginalSource is NavigationItem nvitem)
            {
                SetCurrentValue(SelectedItemProperty, nvitem);
                nvitem.IsSelected = true;
            }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            Refresh();
        }

        public NavigationItem SelectedItem
        {
            get { return (NavigationItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(NavigationItem), typeof(NavigationPanel), new PropertyMetadata(null));


        private void Refresh()
        {
            if (Panel == null)
            {
                return;
            }

            Panel?.Children.Clear();

            foreach (var item in Items)
            {
                if (item is UIElement element)
                {
                    ContentPresenter container = new ContentPresenter();
                    container.Content = element;
                    Panel.Children.Add(container);
                }
            }
        }
    }
}
