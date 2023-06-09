﻿using MySql.Data.MySqlClient;
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

using System.Data.SqlClient;
using static TMS_DesktopApp.AdminSettings;

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

        private int DB_Number;


        public ManageDB()
        {
            InitializeComponent();
        }


        DataSet ds = new DataSet();
        private void rateBtn_Checked(object sender, RoutedEventArgs e)
        {
            DB_Number = 2;
            carrierTable.Visibility = Visibility.Collapsed;
            routeTable.Visibility = Visibility.Collapsed;
            rateFeesTable.Visibility = Visibility.Visible;
            PopulateTable(DB_Number);

        }

        private void carrierBtn_Checked(object sender, RoutedEventArgs e)
        {
            DB_Number = 0;
            rateFeesTable.Visibility = Visibility.Collapsed;
            routeTable.Visibility = Visibility.Collapsed;
            carrierTable.Visibility = Visibility.Visible;
            PopulateTable(DB_Number);


        }



        private void routeBtn_Checked(object sender, RoutedEventArgs e)
        {
            DB_Number = 1;
            rateFeesTable.Visibility = Visibility.Collapsed;
            carrierTable.Visibility = Visibility.Collapsed;
            routeTable.Visibility = Visibility.Visible;
            PopulateTable(DB_Number);


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



        private void rateFeesTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string selected = rateFeesTable.SelectedIndex.ToString();
            var row = rateFeesTable.SelectedItems;
        }



        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            MySqlConnection conn = new DataAccess().Connect_TMS_DB();


            string cmdText = "";

            if (DB_Number == 0)
            {
                var selectedContract = dsCarriers.Tables[0].Rows[carrierTable.SelectedIndex].ItemArray;

                cmdText = $"UPDATE carriers SET cName=" + "\"" + selectedContract[0] + "\"" + ", dCity=" + "\"" + selectedContract[1] + "\"" + ", FTLA=" + selectedContract[2] + ", LTLA=" + selectedContract[3] + " ,FTLRate=" + selectedContract[4] +
                          " ,LTLRate=" + selectedContract[5] + ", reefCharge=" + selectedContract[6] + " WHERE cName=" + "\"" + selectedContract[0] + "\"" + " AND dCity=" + "\"" + selectedContract[1] + "\"";



            }
            else if (DB_Number == 1)
            {
                var selectedContract = dsRoutes.Tables[0].Rows[routeTable.SelectedIndex].ItemArray;
                cmdText = "UPDATE routes SET location=" + "\"" + selectedContract[1] + "\"" + ",locationReference=" + "\"" + selectedContract[2] + "\"" + ",distance=" + selectedContract[3] + ",timeInHours=" + selectedContract[4] + " WHERE routeId=" + selectedContract[0];

            }
            else if (DB_Number == 2)
            {
                var selectedContract = dsRateFees.Tables[0].Rows[rateFeesTable.SelectedIndex].ItemArray;
                cmdText = "UPDATE carriers SET cName=" + "\"" + selectedContract[0] + "\"" + " ,FTLRate=" + selectedContract[1] + " ,LTLRate=" + selectedContract[2] + ", reefCharge=" + selectedContract[3] + "WHERE cName=" + "\"" + selectedContract[0] + "\"";

            }

            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update Success!", "Success");
            ds.Clear();
            PopulateTable(DB_Number);



        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new DataAccess().Connect_TMS_DB();
            string cmdText = "";

            if (DB_Number == 0)
            {
                var selectedContract = dsCarriers.Tables[0].Rows[carrierTable.SelectedIndex].ItemArray;
                cmdText = "DELETE FROM carriers WHERE cName=" + "\"" + selectedContract[0] + "\"" + " AND dCity=" + "\"" + selectedContract[1] + "\"";
                new LogWriter("1 Data deleted from carrier table");
            }
            else if (DB_Number == 1)
            {
                var selectedContract = dsRoutes.Tables[0].Rows[routeTable.SelectedIndex].ItemArray;
                cmdText = "DELETE FROM routes WHERE routeID=" + selectedContract[0];
                new LogWriter("1 Data deleted from route table");
            }
            else if (DB_Number == 2)
            {
                var selectedContract = dsRateFees.Tables[0].Rows[rateFeesTable.SelectedIndex].ItemArray;
                cmdText = "DELETE FROM carriers WHERE cName=" + "\"" + selectedContract[0] + "\"";
                new LogWriter("1 Data deleted from rate table");

            }

            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Delete Success!", "Success");
            ds.Clear();
            PopulateTable(DB_Number);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new DataAccess().Connect_TMS_DB();
            string cmdText = "";

            conn.Open();

            if (DB_Number == 0)
            {
                var selectedContract = dsCarriers.Tables[0].Rows[carrierTable.SelectedIndex].ItemArray;
                cmdText = $"INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) " +
                $"VALUES ('{selectedContract[0]}','{selectedContract[1]}','{selectedContract[2]}','{selectedContract[3]}','{selectedContract[4]}', '{selectedContract[5]}','{selectedContract[6]}');";
                new LogWriter("1 Data added to carrier table");
            }
            else if (DB_Number == 1)
            {
                var selectedContract = dsRoutes.Tables[0].Rows[routeTable.SelectedIndex].ItemArray;
                cmdText = $"INSERT INTO routes(routeId,location,locationReference,distance,timeInHours) " +
                $"VALUES ('{selectedContract[0]}','{selectedContract[1]}','{selectedContract[2]}','{selectedContract[3]}','{selectedContract[4]}');";
                new LogWriter("1 Data added to route table");
            }
            else if (DB_Number == 2)
            {
                var selectedContract = dsRateFees.Tables[0].Rows[rateFeesTable.SelectedIndex].ItemArray;
                cmdText = $"INSERT INTO carriers(cName,dCity,FTLA,LTLA,FTLRate,LTLRate,reefCharge) " +
                $"VALUES ('{selectedContract[0]}','0','0','0','{selectedContract[1]}','{selectedContract[2]}','{selectedContract[3]}');";
                new LogWriter("1 Data added to rate table");
            }

            MySqlCommand cmd = new MySqlCommand(cmdText, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Add Success!", "Success");
            ds.Clear();
            PopulateTable(DB_Number);


        }
    }
}