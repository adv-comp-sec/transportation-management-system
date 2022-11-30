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

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for AdminSettings.xaml
    /// </summary>
    public partial class AdminSettings : Page
    {
        IPAddress IP;
        int port;


        public AdminSettings()
        {
            InitializeComponent();
        }


        private void btn_ConnectDBMS_Click(object sender, RoutedEventArgs e)
        {
            string strIP=txt_IPAddress.Text;
            string strPort = txt_Port.Text;
          
            try
            {
                if (strIP == "" || !IPAddress.TryParse(strIP, out IP) ||  strPort == "" ||Int32.Parse(strPort)==0)
                {
                    IPPortErrorMsg.Content = "[ERROR: You must enter the right form of the IP Address or Port number.]";
                    throw new Exception("[Exception: Entered Unvalid IP Address or Port number.]");
                }
                else
                {
                    IP = IPAddress.Parse(strIP);
                    port = int.Parse(strPort);
                }
               

            }
            catch(Exception ex)
            {
                // write error message to the log file

            }


        }

   

  
    }


}
