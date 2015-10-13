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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using PAU.Documents;
using DevComponents.WpfRibbon;

namespace PAU.UserControls
{
    /// <summary>
    /// Interaction logic for winMySqlConnection.xaml
    /// </summary>
    public partial class winMySqlConnection : Window
    {
        public winMySqlConnection()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(winMySqlConnection_Loaded);
        }

        void winMySqlConnection_Loaded(object sender, RoutedEventArgs e)
        {
            server.Text = MySqlSettings.Instance.Server;
            username.Text = MySqlSettings.Instance.UserName;
            password.Text = MySqlSettings.Instance.Password;
            database.Text = MySqlSettings.Instance.Database;

            server.Focus();
        }

        private void ButtonDropDown_Click(object sender, RoutedEventArgs e)
        {
            ButtonDropDown button = (ButtonDropDown)sender;
            if (button != null)
            {
                if (button.Header is string)
                {
                    string content = button.Header as string;
                    if (content == "Close")
                    {
                        DialogResult = true;

                        MySqlSettings.Instance.Server = server.Text;
                        MySqlSettings.Instance.UserName = username.Text;
                        MySqlSettings.Instance.Password = password.Text;
                        MySqlSettings.Instance.Database = database.Text;

                        MySqlSettings.Serialize("PAU.cfg");

                        this.Close();

                        return;
                    }
                }
            }

            MySqlConnection con = new MySqlConnection(MySqlSettings.Instance.ConnectionString());
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Connection Established", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            if (button != null)
            {
                button.Header = "Close";
            }
        }
    }
}
