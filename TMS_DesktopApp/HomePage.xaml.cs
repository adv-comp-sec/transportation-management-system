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
using TMS_DesktopApp.PlannerView;

namespace TMS_DesktopApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public string user;
        public string admin = "admin";
        public string buyer = "buyer";
        public string planner = "planner";
        public HomePage()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            user = txt_Username.Text;

            if (user == admin)
            {
                this.NavigationService.Navigate(new AdminWindow());
            }
            else if (user == buyer)
            {
                this.NavigationService.Navigate(new BuyerWindow());
            }
            else if (user == planner)
            {
                this.NavigationService.Navigate(new PlannerWindow());
            }
            else
            {
                error_Msg.Text = "Wrong credentials.";
            }
        }
    }
}
