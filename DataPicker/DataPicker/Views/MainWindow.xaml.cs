using System.Windows;
using System.Windows.Input;

namespace DataPicker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ChangedButton == MouseButton.Left)
        //    {
        //        this.DragMove();
        //    }
        //}

        private bool IsMaximize = false;

        //private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount == 2)
        //    {
        //        if (IsMaximize)
        //        {
        //            this.WindowState = WindowState.Normal;
        //            this.Width = 1080;
        //            this.Height = 720;

        //            IsMaximize = false;
        //        }
        //        else
        //        {
        //            this.WindowState = WindowState.Maximized;

        //            IsMaximize = true;
        //        }
        //    }
        //}

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MinimizeApp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
