using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace TMS_DesktopApp.PlannerView
{
    /// <summary>
    /// Interaction logic for ReceivedOrders.xaml
    /// </summary>
    public partial class ReceivedOrders : Page
    {
        bool CarriersAvailabilityFlag = false;
        bool SelectOrder = false;
        MySqlDataAdapter da;
        DataSet ds = new DataSet();

        MySqlDataAdapter daOrders;
        DataSet dsOrders = new DataSet();

        bool city_toronto = false;
        bool city_ottawa = false;

        public ReceivedOrders()
        {
            InitializeComponent();
            PopulateCarriersTable();
            PopulateOrdersTable();
        }

        public void PopulateCarriersTable()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;user=root;password=DP01/11engRD;Database=TMS_DB;");
            string cmd = "SELECT cName, dCity, FTLA, LTLA FROM carriers;";
            da = new MySqlDataAdapter(cmd, conn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(ds);
            //GET THE DATA

            carriersTable.ItemsSource = ds.Tables[0].AsDataView();
            //marketDBTable.ItemsSource = ds.DefaultViewManager;

        }

        public void PopulateOrdersTable()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;user=root;password=DP01/11engRD;Database=TMS_DB;");
            string cmd = "SELECT * FROM orders;";
            daOrders = new MySqlDataAdapter(cmd, conn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            daOrders.Fill(dsOrders);
            receivedOrders.ItemsSource = dsOrders.Tables[0].AsDataView();
        }

        private void AddTrip_Click(object sender, RoutedEventArgs e)
        {
            if (city_toronto == true)
            {

            }
            else if (city_ottawa == true)
            {

            }
        }

        private void Toronto_Checked(object sender, RoutedEventArgs e)
        {
            if (City_Toronto.IsChecked.Value)
            {
                city_toronto = true;
            }
        }

        private void Ottawa_Checked(object sender, RoutedEventArgs e)
        {
            if (City_Ottawa.IsChecked.Value)
            {
                city_ottawa = true;
            }
        }
    }
}
