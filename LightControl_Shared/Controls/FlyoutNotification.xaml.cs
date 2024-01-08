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
    public partial class FlyoutNotification : Window
    {
        public FlyoutNotification()
        {
            InitializeComponent();
        }

        public static async void ShowNotication(string message, string title)
        {
            FlyoutNotification flyoutNotication = new FlyoutNotification()
            {
                Owner = Application.Current.MainWindow,
                ShowInTaskbar = false,
                Left = Application.Current.MainWindow.ActualWidth + (Application.Current.MainWindow.WindowState == WindowState.Maximized ? -20 : Application.Current.MainWindow.Left - 10) - 300,
                Top = Application.Current.MainWindow.ActualHeight + (Application.Current.MainWindow.WindowState == WindowState.Maximized ? -20 : Application.Current.MainWindow.Top-10) - 80,
            };

            flyoutNotication.MessageText.Text = message;
            flyoutNotication.TitleText.Text = title;
            var sbShow = flyoutNotication.FindResource("Show") as Storyboard;
            flyoutNotication.Show();
            sbShow.Begin();

            await Task.Delay(2000);
            var sbHide = flyoutNotication.FindResource("Hide") as Storyboard;
            sbHide.Completed += (s, e) => flyoutNotication.Close();
            sbHide.Begin();
        }
    }

}
