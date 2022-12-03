using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for MarketPage.xaml
    /// </summary>
    /// 
    public class Contract
    {
        public string Client_Name { get; set; }
        public string Job_Type { get; set; }
        public string Quantity { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Van_Type { get; set; }
        public string isChecked { get; set; }

    };
    
    public partial class MarketPage : Page
    {
        private List<Contract> MarketData = new List<Contract>() { };
        public MarketPage()
        {
            InitializeComponent();
            PopulateTable();
        }
        DataSet ds = new DataSet();
        public void PopulateTable()
        {
        
            //MySqlConnection conn = new MySqlConnection("Server=159.89.117.198;Port=3306;user=DevOSHT;password=Snodgr4ss!;Database=cmp;");
            MySqlConnection conn = new DataAccess().ConnectMarketDB();
           // MySqlCommand cmd = new MySqlCommand("SELECT * FROM Contract;",conn);

            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Contract;", conn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(ds);
            marketDBTable.ItemsSource = ds.Tables[0].AsDataView();
            
            /*conn.Open();
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                Contract newContract = new Contract
                {
                    Client_Name = rdr.GetString(0),
                    Job_Type = rdr.GetString(1),
                    Quantity = rdr.GetString(2),
                    Origin = rdr.GetString(3),
                    Destination = rdr.GetString(4),
                    Van_Type = rdr.GetString(5),
                };
                //AddItem(newContract);
                

            }
            marketDBTable.ItemsSource = MarketData;
            rdr.Close();
            conn.Close();*/
        }

        public void AddItem(Contract newContract)
        {
            marketDBTable.Items.Add(newContract);
            MarketData.Add(newContract);
        }

        public void RemoveItem(int index)
        {
            marketDBTable.Items.RemoveAt(index);
            MarketData.RemoveAt(index);
        }

        public void ClearMarket()
        {
            marketDBTable.Items.Clear();
            MarketData = new List<Contract>() { };
        }

        private void marketDBTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //marketDBTable.IsEnabled = false;
            string selected = marketDBTable.SelectedIndex.ToString();
            var row = marketDBTable.SelectedItems;
            
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            var index = marketDBTable.SelectedIndex;
            var sItem = marketDBTable.SelectedItem;
            var client = ds.Tables[0].Rows[index][0];
            ds.Tables[0].Rows[index][0] = "Fatih";
            //var row = sItem.
            //var something = data.Row[0];
            //var client = data.it;
            /*ClearMarket();
            PopulateTable();*/
        }

        private void btn_CreateOrder_Click(object sender, RoutedEventArgs e)
        {

            //GET VALUES OF ORDER
            var selectedContract = MarketData[marketDBTable.SelectedIndex];
            RemoveItem(marketDBTable.SelectedIndex);
            /*IS Client is a customer of TMS?*/
            /*SUCCESS*/
            //add order to database
            MySqlConnection conn = new DataAccess().ConnectTmsDB();

            string cmdText = $"INSERT INTO orders(CustomerID,TripID ,RelevantCitiesId,OrderStatus,OrderDate,Origin,Destination,VanType,JobType,Quantity) " +
                $"VALUES (1,2,1,'Recieved','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{selectedContract.Origin}','{selectedContract.Destination}',{selectedContract.Van_Type},{selectedContract.Job_Type},{selectedContract.Quantity});";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            //remove item from table
               
        }

   
    }
}
