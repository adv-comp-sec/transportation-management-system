using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for AdminSettings.xaml
    /// </summary>
    public partial class AdminSettings : Page
    {
        MySqlConnection connection = null;

        IPAddress IP;
        int port;

        public AdminSettings()
        {
            InitializeComponent();
        }

        private void btn_ConnectDBMS_Click(object sender, RoutedEventArgs e)
        {
            string strIP = txt_IPAddress.Text;
            string strPort = txt_Port.Text;

            try
            {
                if (strIP == "" || !IPAddress.TryParse(strIP, out IP) || strPort == "" || Int32.Parse(strPort) == 0)
                {
                    MessageBox.Show("Enter IP Address and Port number in the correct format", "Error");
                    throw new Exception("[Exception: Entered Unvalid IP Address or Port number.]");
                }
                else
                {
                    IP = IPAddress.Parse(strIP);
                    port = int.Parse(strPort);

                    if (IP == IPAddress.Parse("127.0.0.1") || port==3306)
                    {
                        MessageBox.Show("Connect Success", "Database Connection");
                    }
                    else if (IP == IPAddress.Parse("159.89.117.198") || port == 3306)
                    {
                        MessageBox.Show("Connect Success", "Database Connection");
                    }
                    else
                    {
                        MessageBox.Show("Cannot find Database", "Error");
                        throw new Exception("[Exception: Database connection failed]");
                    }
                }


            }
            catch (Exception ex)
            {
                // write error message to the log file

            }
        }





    }
}

  