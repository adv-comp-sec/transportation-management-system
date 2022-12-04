using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
using static System.Net.WebRequestMethods;

namespace TMS_DesktopApp.BuyerView
{
    /// <summary>
    /// Interaction logic for BuyerOrders.xaml
    /// </summary>
    public partial class BuyerOrders : Page
    {
        private StreamWriter invoiceFile = null;

        public BuyerOrders()
        {
            InitializeComponent();
            PopulateTable();
        }

        DataSet ds = new DataSet();
        public void PopulateTable()
        {
            string databaseInfo = ConfigurationManager.AppSettings["tms_db_connection_string"];
            MySqlConnection conn = new MySqlConnection(databaseInfo);
            //MySqlConnection conn = new DataAccess().ConnectMarketDB();
            // MySqlCommand cmd = new MySqlCommand("SELECT * FROM Contract;",conn);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM orders;", conn);
            da.Fill(ds);
            ordersTable.ItemsSource = ds.Tables[0].AsDataView();
        }

        private void btn_GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_DownloadInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoiceFile = new StreamWriter(fileName, true);
                invoiceFile.WriteLine(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (invoiceFile != null)
                {
                    invoiceFile.Close();
                    invoiceFile = null;
                }
            }

        }
    }
}
