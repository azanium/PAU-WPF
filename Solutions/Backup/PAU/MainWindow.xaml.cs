using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevComponents.WpfRibbon;
using PAU.Controllers;
using PAU.Documents;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using PAU.UserControls;
using System.Reflection;
using System.IO;

namespace PAU
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        MainController controller = MainController.Instance;
        bool _conEstablished = false;

        public MainWindow()
        {
            InitializeComponent();

            MySqlSettings.DeserializeInstance("PAU.cfg");

            MySqlConnection con = new MySqlConnection(MySqlSettings.Instance.ConnectionString());
            try
            {
                con.Open();
            }
            catch (Exception)
            {
                winMySqlConnection conDlg = new winMySqlConnection();

                _conEstablished = conDlg.ShowDialog().Value;

                if (_conEstablished == false)
                {
                    Close();
                }
            }

            PAU.Properties.Settings.Default.Reload();

            dgPassenger.DataContext = controller;

            this.DataContext = controller;

            controller.AppDock = AppDock;
            controller.DockGroup = dockGroup;
            controller.PassengerGrid = dgPassenger;
            controller.DPOGrid = dgDPO;
            controller.NationalityGrid = dgNationality;

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dgPassenger.Header = "All Passengers";
            
            this.WindowState = PAU.Properties.Settings.Default.Maximized ? WindowState.Maximized : WindowState.Normal;
            MainController.Instance.RowPerPage = PAU.Properties.Settings.Default.RowPerPage;
            MainController.Instance.BookingDateRange = PAU.Properties.Settings.Default.BookingDateRange;

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandExecute));
            
            controller.Loaded(this);

            _statusServer.Content = MySqlSettings.Instance.Server;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PAU.Properties.Settings.Default.Maximized = this.WindowState == WindowState.Maximized;
            PAU.Properties.Settings.Default.RowPerPage = MainController.Instance.RowPerPage;
            PAU.Properties.Settings.Default.BookingDateRange = MainController.Instance.BookingDateRange;

            PAU.Properties.Settings.Default.Save();
        }

        public void CloseCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void edtRowPerPage_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (edtRowPerPage.Value != null)
            {
                MainController.Instance.RowPerPage = edtRowPerPage.Value.Value;
            }
        }

        private void edtBookingDateRange_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (edtBookingDateRange.Value != null)
            {
                //MainController.Instance.BookingDateRange = edtBookingDateRange.Value.Value;
            }
        }
    }
}
