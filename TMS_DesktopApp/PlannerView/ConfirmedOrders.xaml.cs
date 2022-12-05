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
    /// Interaction logic for ConfirmedOrders.xaml
    /// </summary>
    public partial class ConfirmedOrders : Page
    {
        DataTable dtConfirmedOrders = new DataTable();
        DataTable dtTrips = new DataTable();
        public ConfirmedOrders()
        {
            InitializeComponent();
            PopulateOrdersTable();
        }

        private void PopulateOrdersTable()
        {
            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            string cmd = "SELECT * FROM orders  WHERE OrderStatus = 'Confirmed' OR OrderStatus = 'Completed';";
            MySqlDataAdapter daOrders = new MySqlDataAdapter(cmd, conn);
            daOrders.Fill(dtConfirmedOrders);
            confirmedOrderTable.ItemsSource = dtConfirmedOrders.AsDataView();
        }

        private void btn_CompleteTrip_Click(object sender, RoutedEventArgs e)
        {
            int index = confirmedOrderTable.SelectedIndex;
            var order = dtConfirmedOrders.Rows[index].ItemArray;
            int orderID =int.Parse(order[0].ToString());

            if (order[4].ToString() == "Completed")
            {
                MessageBox.Show("Order is already completed");
                return;
            }

            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            //update order table with order status
            string cmdText = $"UPDATE orders SET OrderStatus = 'Completed' WHERE OrderID = {order[0]};";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();

            //Calculate Cost and update cost value in order 
            //1-GET Trips data
            cmdText= $"SELECT * FROM trips  WHERE OrderID = {order[0]};";
            MySqlDataAdapter daTrips = new MySqlDataAdapter(cmdText, conn);
            daTrips.Fill(dtTrips);
            //2-Get selected order by orderID
            DataRow selectedOrder = dtTrips.Select($"OrderID={orderID}")[0] as DataRow;
            //get price
            double price = double.Parse(selectedOrder[5].ToString());
            //get job type
            int jobType = int.Parse(order[9].ToString());
            //add markup to price
            double cost = 0;
            if (jobType == 0)
            {
                //FTL
                cost = price *1.08;
            }
            else
            {
                //LTL
                cost = price * 1.05;
            }
            int days = int.Parse(selectedOrder[4].ToString());
            int surCharge = (150* days) -150;
            cost += surCharge;
            cmdText = $"UPDATE orders SET Cost = {cost} WHERE OrderID = {order[0]};";
            cmd = new MySqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
