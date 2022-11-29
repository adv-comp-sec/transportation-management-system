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

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for MarketPage.xaml
    /// </summary>
    public partial class MarketPage : Page
    {
        bool OnlineOrderFlag = false;
        MySqlDataAdapter da;
        DataSet ds = new DataSet();
        public MarketPage()
        {
            InitializeComponent();
            PopulateTable();
        }

        public void PopulateTable()
        {
            MySqlConnection conn = new MySqlConnection("Server=159.89.117.198;Port=3306;user=DevOSHT;password=Snodgr4ss!;Database=cmp;");
            string cmd = "SELECT * FROM Contract;";
            da = new MySqlDataAdapter(cmd, conn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(ds);
            //GET THE DATA

            marketDBTable.ItemsSource = ds.Tables[0].AsDataView();
            //marketDBTable.ItemsSource = ds.DefaultViewManager;

        }

 
    }
}
