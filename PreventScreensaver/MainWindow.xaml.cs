using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PreventScreensaver
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon NotifyIcon;
        public event EventHandler RunningChanged;
        public MainWindow()
        {
            InitializeComponent();

            string path  = AppDomain.CurrentDomain.BaseDirectory;
            NotifyIcon = new NotifyIcon();
            NotifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(path + "PreventScreensaver.exe");
            NotifyIcon.Visible = true;
           
            var menu = new System.Windows.Forms.ContextMenu();
            menu.MenuItems.Add("중지", OnClick);
            menu.MenuItems.Add("종료", OnExit);
            NotifyIcon.ContextMenu = menu;

            BackgroundTask BackgroundTask = new BackgroundTask();
            RunningChanged += delegate { BackgroundTask.Stop(); };
            BackgroundTask.Start();

            this.Hide();
        }

        private void OnClick(object sender, EventArgs e)
        {
            var button = NotifyIcon.ContextMenu.MenuItems[0];
            if (button.Text == "중지")
            {
                RunningChanged?.Invoke(this, null);
                button.Text = "시작";
            }
            else
            {
                BackgroundTask BackgroundTask = new BackgroundTask();
                RunningChanged += delegate { BackgroundTask.Stop(); };
                BackgroundTask.Start();
                button.Text = "중지";
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
