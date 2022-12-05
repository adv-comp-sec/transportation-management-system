using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TMS_DesktopApp.AdminView;
using Path = System.IO.Path;

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Page
    {
        public AdminWindow()
        {
            InitializeComponent();
        }


        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminContent.Content = new AdminSettings();
        }

        private void manageDBBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminContent.Content = new ManageDB();
        }

        private void logBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminContent.Content = new ViewLog();
        }
    }

}
