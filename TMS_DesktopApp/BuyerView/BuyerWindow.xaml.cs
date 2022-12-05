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

namespace TMS_DesktopApp
{ 
    /// <summary>
    /// Interaction logic for BuyerWindow.xaml
    /// </summary>
    public partial class BuyerWindow : Page
    {
        public BuyerWindow()
        {
            InitializeComponent();
            BuyerFrame.NavigationService.Navigate(new MarketPage());
        }

        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.NavigationService.Navigate(new HomePage());

            //this.NavigationService.GoBack();
        }

        private void Orders_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyerFrame.NavigationService.Navigate(new BuyerView.BuyerOrders());
        }

        private void MarketPage_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyerFrame.NavigationService.Navigate(new MarketPage());
        }
    }
}
