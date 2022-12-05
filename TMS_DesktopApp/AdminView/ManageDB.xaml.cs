using MySql.Data.MySqlClient;
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
using MySqlX.XDevAPI.Relational;
using System.Data;
using System.Windows.Controls.Primitives;

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for ManageDB.xaml
    /// </summary>
    public partial class ManageDB : Page
    {
        DataSet dsCarriers = new DataSet();
        DataSet dsRoutes = new DataSet();
        DataSet dsRateFees = new DataSet();

        MySqlConnection conn = new DataAccess().Connect_TMS_DB();


        public ManageDB()
        {
            InitializeComponent();
        }


        DataSet ds = new DataSet();
        private void rateBtn_Checked(object sender, RoutedEventArgs e)
        {
            carrierTable.Visibility = Visibility.Collapsed;
            routeTable.Visibility = Visibility.Collapsed;
            rateFeesTable.Visibility = Visibility.Visible;
            PopulateTable(2);

        }

        private void carrierBtn_Checked(object sender, RoutedEventArgs e)
        {
            rateFeesTable.Visibility = Visibility.Collapsed;
            routeTable.Visibility = Visibility.Collapsed;
            carrierTable.Visibility = Visibility.Visible;
            PopulateTable(0);
          
         
        }



        private void routeBtn_Checked(object sender, RoutedEventArgs e)
        {
            rateFeesTable.Visibility = Visibility.Collapsed;
            carrierTable.Visibility = Visibility.Collapsed;
            routeTable.Visibility = Visibility.Visible;
            PopulateTable(1);


        }

        public void PopulateTable(int DB_number)
        {
            dsRoutes.Clear();
            dsCarriers.Clear();
            dsRateFees.Clear();



         
            string cmd = "";
            if (DB_number == 0)
            {
                cmd = "SELECT * FROM carriers;";
                MySqlDataAdapter daCarriers = new MySqlDataAdapter(cmd, conn);
                daCarriers.Fill(dsCarriers);
                carrierTable.ItemsSource = dsCarriers.Tables[0].AsDataView();
            }

            else if (DB_number == 1)
            {
                cmd = "SELECT * FROM routes;";
                MySqlDataAdapter daRoutes = new MySqlDataAdapter(cmd, conn);
                daRoutes.Fill(dsRoutes);
                routeTable.ItemsSource = dsRoutes.Tables[0].AsDataView();
            }
            else if (DB_number == 2)
            {
                cmd = "SELECT DISTINCT cName, FTLRate, LTLRate, reefCharge FROM carriers";
                MySqlDataAdapter daRateFees = new MySqlDataAdapter(cmd, conn);
                daRateFees.Fill(dsRateFees);
                rateFeesTable.ItemsSource = dsRateFees.Tables[0].AsDataView();
            }

        }

        private void carrierTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string selected = carrierTable.SelectedIndex.ToString();
            var row = carrierTable.SelectedItems;
        }

        private void routeTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string selected = routeTable.SelectedIndex.ToString();
            var row = routeTable.SelectedItems;
        }



        //private void rateFeesTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //string selected = rateFeesTable.SelectedIndex.ToString();
        //var row = rateFeesTable.SelectedItems;
        //}


   
     ///=========================this function needs to be fix==================
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
           
            var selectedContract = dsCarriers.Tables[0].Rows[carrierTable.SelectedIndex].ItemArray;
            //Remove selected row
            dsCarriers.Tables[0].Rows.RemoveAt(carrierTable.SelectedIndex);
            MySqlConnection conn = new DataAccess().Connect_TMS_DB();

            string cmdText = $"DELETE from carriers WHERE cName={selectedContract[0]} AND dCity={selectedContract[1]};";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            //cmdText = $"INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) " +
            // $"VALUES ('{selectedContract[0]}','{selectedContract[1]}','{selectedContract[2]}','{selectedContract[3]}','{selectedContract[4]}', '{selectedContract[5]}','{selectedContract[6]}');";
            //cmd = new MySqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // if update / add/ delete success, show messagebox

        }

     
    }
}
