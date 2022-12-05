using Org.BouncyCastle.Asn1.Cms;
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

namespace TMS_DesktopApp.PlannerView
{
    /// <summary>
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Page
    {
        public PlannerWindow()
        {
            InitializeComponent();
            PlannerFrame.NavigationService.Navigate(new ReceivedOrders());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlannerFrame.NavigationService.Navigate(new ConfirmedOrders());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PlannerFrame.NavigationService.Navigate(new ReceivedOrders());
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            PlannerFrame.NavigationService.Navigate(new HomePage());
        }
    }
}
