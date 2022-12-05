using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
        DataTable dtCarriers = new DataTable();
        DataSet dsOrders = new DataSet();
        DataTable dtTrips = new DataTable() { };
        DataTable routeTable = new DataTable("RouteTable") { };

        public double TotalHours { get; set; }
        public int TotalDistance { get; set; }
        public double TotalPrice { get; set; }
        public int TotalDays { get; set; }

        public ReceivedOrders()
        {
            InitializeComponent();
            LoadRouteData();
            PopulateCarriersTable();
            PopulateOrdersTable();
            dtTrips = CreateTripsTable();
            tripsTable.ItemsSource = dtTrips.AsDataView();
        }

        public void PopulateCarriersTable()
        {
            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            string cmd = "SELECT * FROM carriers;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);

            da.Fill(dtCarriers);
            carriersTable.ItemsSource = dtCarriers.AsDataView();
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
            dtTrips.Rows.Add(new Object[] { order[0], order[6], order[7], order[9] });
            btn_Add.IsEnabled = false;
            btn_Remove.IsEnabled = true;
        }
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            dtTrips.Rows[0].Delete();
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
                new DataColumn("Job_Type", typeof(int)),
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
            try
            {
                string cName = dtTrips.Rows[0]["Carrier"].ToString();
                //check if carrier is entered, if not show a message box
                if (cName.Length==0)
                {
                    MessageBox.Show("Enter a Carrier for trip");
                    return;
                }
                int id = (int)dtTrips.Rows[0][0];
                DataRow orderRow = dsOrders.Tables[0].Select($"OrderID='{id}'").GetValue(0) as DataRow;

                //calculate trip hours
                string origin = dtTrips.Rows[0]["Origin"].ToString();
                string dest = dtTrips.Rows[0]["Destination"].ToString();
                int jobType = int.Parse(orderRow["JobType"].ToString());
                CalculateTotalHoursAndDistance(origin, dest, jobType);

                //calculate price
                
                int rateType = int.Parse(dtTrips.Rows[0]["Job_Type"].ToString());
                string rateTypeString = rateType==0 ? "FTLRate" : "LTLRate";
          
                DataRow carrierRow = dtCarriers.Select($"cName='{cName}'").GetValue(0) as DataRow;
                double rate = double.Parse(carrierRow[rateTypeString].ToString());
                double reefCharge = double.Parse(carrierRow["reefCharge"].ToString());   


                int quantity = int.Parse(orderRow["Quantity"].ToString());
                int vanType = int.Parse(orderRow["VanType"].ToString());
                if(vanType==1)
                {
                    rate += reefCharge;
                }

                CalculatePrice(origin,dest,TotalDistance,TotalHours,rate, quantity);

                //Update UI
                dtTrips.Rows[0]["Distance"] = TotalDistance;
                dtTrips.Rows[0]["Hours"] = TotalHours;
                dtTrips.Rows[0]["Price"] = TotalPrice;
                dtTrips.Rows[0]["Days"] = TotalDays;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void CalculatePrice(string origin, string dest, int distance, double hours, double rate, int quantity)
        {
            double totalPrice = 0;
            List<string> citiesWesttoEast = new List<string>() { "Windsor", "London", "Hamilton", "Toronto", "Oshawa", "Belleville", "Kingston", "Ottawa" };
            if(quantity>0)
            {
                totalPrice = distance * rate * quantity;
            }
            else
            {
                totalPrice = distance * rate;
            }
            TotalPrice = totalPrice;
        }

        private void CalculateTotalHoursAndDistance(string origin, string dest,int jobType)
        {
            double totalHours = 0;
            double driveTime = 0;
            double waitTime = 0;
            int totalDistance = 0;
            List<string> citiesWesttoEast = new List<string>() {"Windsor", "London", "Hamilton", "Toronto", "Oshawa", "Belleville", "Kingston", "Ottawa" };
            int originIndex = citiesWesttoEast.IndexOf(origin);
            int destIndex = citiesWesttoEast.IndexOf(dest);
            for (int i = 0; i < routeTable.Rows.Count; i++)
            {
                if(i<originIndex && i>=destIndex)
                {
                    totalDistance += int.Parse(routeTable.Rows[i]["distance"].ToString());
                    driveTime += double.Parse(routeTable.Rows[i]["timeInHours"].ToString());
                    if(jobType==1)
                    {
                        waitTime += 2; //load unload and stop time
                    }
                }
                else if(i >= originIndex && i < destIndex)
                {
                    totalDistance += int.Parse(routeTable.Rows[i]["distance"].ToString());
                    driveTime += double.Parse(routeTable.Rows[i]["timeInHours"].ToString());
                    waitTime += 2;
                }
            }
            if (jobType == 0)
            {
                waitTime += 4; //load and unload
            }
            totalHours = waitTime + driveTime;
            this.TotalHours = totalHours;
            this.TotalDays = ((int)driveTime / 8) +1; //1)	A driver can only operate a maximum of 12 hrs per day, and only 8 of those hours can be 
            this.TotalDistance = totalDistance;
        }

        private void LoadRouteData()
        {
            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            string cmd = "SELECT * FROM routes;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);
            da.Fill(routeTable);
        }

        private void btn_ConfirmTrips_Click(object sender, RoutedEventArgs e)
        {
            //GET VALUES OF TRIP
            var dtTrip = dtTrips.Rows[0].ItemArray;

            MySqlConnection conn = new DataAccess().ConnectTmsDB();
            //update order table with order status
            string cmdText = $"UPDATE orders SET OrderStatus = 'Confirmed' WHERE OrderID = {dtTrip[0]};";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
  

            //add new entry to trip table
            cmdText = $"INSERT INTO trips(OrderID, Distance, Hours, Days, Price, Carrier) VALUES({dtTrip[0]}, '{dtTrip[4]}', '{dtTrip[5]}', '{dtTrip[6]}', '{dtTrip[7]}', '{dtTrip[8]}');";
            cmd = new MySqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            //REMOVE TRIP
            dtTrips.Rows[0].Delete(); 
        }
    }
}
