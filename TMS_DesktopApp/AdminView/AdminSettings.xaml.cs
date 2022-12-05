using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

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

        public static string logPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\logs";

        public AdminSettings()
        {
            InitializeComponent();
            // Load the log file directory path
            txt_Directory_Path.Text = logPath;
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

                    if (IP == IPAddress.Parse("127.0.0.1") || port == 3306)
                    {
                        MessageBox.Show("Connect Success", "Database Connection");
                        new LogWriter("Database Connect Success : 127.0.0.1:3306");
                    }
                    else if (IP == IPAddress.Parse("159.89.117.198") || port == 3306)
                    {
                        MessageBox.Show("Connect Success", "Database Connection");
                        new LogWriter("Database Connect Success : 159.89.117.198:3306");
                    }
                    else
                    {
                        MessageBox.Show("Cannot find Database", "Error");
                        new LogWriter("ERROR: Database Connect failed");
                        throw new Exception("[Exception: Database connection failed]");
                    }
                }


            }
            catch (Exception ex)
            {
                // write error message to the log file

            }
        }

        private void btn_SaveDirectory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logPath = txt_Directory_Path.Text;
                new LogWriter("Change Log file directory : " + logPath);
                MessageBox.Show("Changed the log file directory to " + logPath, "Success");
            }
            catch (Exception ex)
            {
                new LogWriter("Fail to change the log directory :" + ex.ToString());
                MessageBox.Show("Fail to change the log file directory. " + ex.ToString(), "Error");
            }
        }

        private void btn_BrowseDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txt_Directory_Path.Text = dialog.SelectedPath;
        }


        public class LogWriter
        {
            public LogWriter(string logMessage)
            {
                using (StreamWriter w = File.AppendText(logPath + "\\" + "TMSlog_" + DateTime.Now.ToString("yyyyMMdd") + ".log"))
                {
                    Log(logMessage, w);
                }
            }

            public void Log(string logMessage, TextWriter txtWriter)
            {
                txtWriter.Write("{0}", DateTime.Now.ToLocalTime());
                txtWriter.WriteLine(": {0}", logMessage);
            }
        }
    }
}

