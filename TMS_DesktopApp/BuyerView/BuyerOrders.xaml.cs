using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
        const string dryVan = "Dry Cargo";
        const string reeferVan = "Reefer Cargo";
        const string FTL = "FTL";
        const string LTL = "LTL";

        private StreamWriter invoiceFile = null;

        private string invoiceNumber { get; set; }

        private string dateOfIssue { get; set; }

        private string clientName { get; set; }

        private string companyName { get; set; }

        private string description { get; set; }
        private decimal unitPrice { get; set; }

        private int quantity { get; set; }
        
        private decimal cost { get; set; }

        private string subtotal { get; set; }

        private decimal total { get; set; }

        private decimal taxRate { get; set; }

        private string vanType { get; set; }

        private string jobType { get; set; }

        //Date of Issue
        //Billed to
        //From
        //Description Unit cost Quantity    Amount

        //Subtotal
        //Tax Rate
        //Total

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
            var selectedOrder = ds.Tables[0].Rows[ordersTable.SelectedIndex].ItemArray;     // store selected order values (row) into an array

            invoiceNumber = Guid.NewGuid().ToString();

            dateOfIssue = DateTime.Now.ToString();

            clientName = selectedOrder[1].ToString();   // get second element from table customerID
            // TO DO: relation between tables to get the name using the customer ID

            companyName = ConfigurationManager.AppSettings["tmsName"];

            taxRate = 0.13m;            // HST in Ontarion        

            string vanType = selectedOrder[8].ToString();

            string jobType = selectedOrder[9].ToString();

            
            if (vanType == "0")     // check van type
            {
                vanType = dryVan;
                
            }
            else if (vanType == "1")
            {
                vanType = reeferVan;
                //TO DO: get the reefer charger from customer
            }
            else
            {
                // Write to log file the error
            }

            if (jobType == "0")     // check job type
            {
                jobType = FTL;
                unitPrice = 4.985m;

                // TO DO: get unit price from carrier + OSHT markup 
            }
            else if (jobType == "1")
            {
                jobType = LTL;
                unitPrice = 0.2995m;
            }
            else
            {
                // Write to log file the error
            }

            if (quantity == 0)      // get quantity
            {
                quantity = 1;       // if 0, assign 1 full truck

                // TO DO: check how to convert the combined LTL to one full truck 
            }
            else
            {
                quantity = Convert.ToInt32(selectedOrder[10].ToString());   // get pallets quantity for LTL
            }

            cost = Convert.ToDecimal(selectedOrder[10].ToString());         // get cost from order

            subtotal = cost.ToString();

            total = (cost * taxRate / 100) + cost;      // add tax rate to the subtotal


            DownloadInvoice_Click();//create file

        }

        private void DownloadInvoice_Click()
        {
            try
            {
                invoiceNumber = Guid.NewGuid().ToString();
                string filePath = "C:\\temp\\" + invoiceNumber + ".txt";
                invoiceFile = new StreamWriter(filePath, true);

                invoiceFile.WriteLine("INVOICE " + invoiceNumber + "\n\n"
                    + "Date of Issue: " + dateOfIssue + "\n\n"
                    + "Billed to " + clientName + "\n"
                    + "From " + companyName + "\n\n"
                    + "-----------------------------------------------\n"
                    + "Description: " + jobType + vanType + "\n");

                if (jobType == FTL)
                {
                    invoiceFile.WriteLine("Unit price: $" + unitPrice.ToString() + "/km as a flat rate\n");
                }
                else
                {
                    invoiceFile.WriteLine("Unit price: $" + unitPrice.ToString() + "/km for each pallet\n");
                }

                invoiceFile.WriteLine("Cost: $" + cost.ToString() + "\n"
                    + "-----------------------------------------------\n\n"
                    + "Subtotal: " + subtotal + "\n"
                    + "HST: " + taxRate.ToString() + "\n"
                    + "Total: " + total.ToString() + "\n"
                    );
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
