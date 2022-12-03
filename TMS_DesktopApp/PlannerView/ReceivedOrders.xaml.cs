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
        DataSet dsCarriers = new DataSet();
        DataSet dsOrders = new DataSet();
        DataTable dsTrips = new DataTable() { };

        public ReceivedOrders()
        {
            InitializeComponent();
            PopulateCarriersTable();
            PopulateOrdersTable();
            tripsTable.ItemsSource = dsTrips.AsDataView();
        }

        public void PopulateCarriersTable()
        {
            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            string cmd = "SELECT cName, dCity, FTLA, LTLA FROM carriers;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);

            da.Fill(dsCarriers);
            carriersTable.ItemsSource = dsCarriers.Tables[0].AsDataView();
        }
        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            dsOrders.Clear();
            PopulateOrdersTable();
        }
        public void PopulateOrdersTable()
        {
            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            string cmd = "SELECT * FROM orders;";
            MySqlDataAdapter daOrders = new MySqlDataAdapter(cmd, conn);

            daOrders.Fill(dsOrders);
            receivedOrders.ItemsSource = dsOrders.Tables[0].AsDataView();
        }

        private void AddTrip_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
