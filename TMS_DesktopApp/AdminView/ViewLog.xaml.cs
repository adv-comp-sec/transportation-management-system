using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using static TMS_DesktopApp.AdminSettings;

namespace TMS_DesktopApp.AdminView
{
    /// <summary>
    /// Interaction logic for ViewLog.xaml
    /// </summary>
    public partial class ViewLog : Page
    {
        public ViewLog()
        {
            InitializeComponent();
            dtg_DirectoryTable.ItemsSource = new DirectoryInfo(logPath).GetFiles();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (btn_Back.IsEnabled == false)
            {
                string fileName = dtg_DirectoryTable.SelectedItem.ToString();
                string filePath = logPath + "\\" + dtg_DirectoryTable.SelectedItem.ToString();

                lbl_FileName.Content += fileName;
                lbl_DirectoryPath.Content += logPath;

                DataTable allLines = new DataTable();

                allLines.Columns.Add("Log");
                foreach (var line in File.ReadAllLines(filePath))
                {
                    allLines.Rows.Add(line);
                }

                dtg_DirectoryTable.ItemsSource = allLines.DefaultView;

                btn_Back.IsEnabled = true;

            }

        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            dtg_DirectoryTable.ItemsSource = new DirectoryInfo(logPath).GetFiles();
            btn_Back.IsEnabled = false;

            lbl_FileName.Content = "FileName: ";
            lbl_DirectoryPath.Content = "Directory path: ";
        }

    }
}
