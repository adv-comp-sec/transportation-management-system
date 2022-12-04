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
            dsTrips = CreateTripsTable();
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

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            int index = receivedOrders.SelectedIndex;
            var order = dsOrders.Tables[0].Rows[index].ItemArray;
            dsTrips.Rows.Add(new Object[] { order[0], order[6], order[7], order[9] });
            btn_Add.IsEnabled = false;
            btn_Remove.IsEnabled = true;
        }
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            dsTrips.Rows[0].Delete();
            btn_Add.IsEnabled = true;
            btn_Remove.IsEnabled = false;
        }
        private DataTable CreateTripsTable()
        {
            DataTable tripsTable = new DataTable("Trips");
            // Define all the columns once.
            DataColumn[] cols =
            {
                new DataColumn("OrderID", typeof(int)),
                new DataColumn("Origin", typeof(String)),
                new DataColumn("Destination", typeof(String)),
                new DataColumn("Job_Type", typeof(String)),
                new DataColumn("Distance", typeof(int)),
                new DataColumn("Hours", typeof(int)),
                new DataColumn("Days", typeof(int)),
                new DataColumn("Price", typeof(int)),
                new DataColumn("Carrier", typeof(String)),
            };
            tripsTable.Columns.AddRange(cols);
  
            return tripsTable;
        }

        private void btn_Calculate_Click(object sender, RoutedEventArgs e)
        {
            //check if carrier is entered, if not show a message box

            //calculate trip hours

            //calculate amount of day

            //calculate price
        }
    }

}
