using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using Path = System.IO.Path;
using static TMS_DesktopApp.AdminSettings;

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

        private void backupBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSet dsCarriers = new DataSet();
            DataSet dsRoutes = new DataSet();
            DataSet dsRateFees = new DataSet();


            MySqlConnection conn = new DataAccess().Connect_TMS_DB();

            string cmd = "SELECT * FROM routes;";
            MySqlDataAdapter daRoutes = new MySqlDataAdapter(cmd, conn);
            daRoutes.Fill(dsRoutes);



            cmd = "SELECT DISTINCT cName, FTLRate, LTLRate, reefCharge FROM carriers";
            MySqlDataAdapter daRateFees = new MySqlDataAdapter(cmd, conn);
            daRateFees.Fill(dsRateFees);


            dsCarriers.WriteXml(logPath);
            dsRoutes.WriteXml(logPath);
            dsRateFees.WriteXml(logPath);
        }
    }

}
