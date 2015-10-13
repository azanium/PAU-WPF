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

namespace PAU.UserControls
{
    /// <summary>
    /// Interaction logic for winAddWNAttention.xaml
    /// </summary>
    public partial class winAddWNAttention : Window
    {
        public winAddWNAttention()
        {
            InitializeComponent();

            DataContext = this;
        }

        private string _nationality = "";
        public string Nationality
        {
            get { return _nationality; }
            set
            {
                _nationality = value;
            }
        }

        private void ButtonDropDown_Click(object sender, RoutedEventArgs e)
        {
            if (tbNationality.Text.Trim() == "")
            {
                MessageBox.Show("Isikan Nationality (WN)", "Invalid", MessageBoxButton.OK);
                return;
            }

            DialogResult = true;

            BeaCukai cukai = new BeaCukai();
            NationalityAttention att = new NationalityAttention();
            att.Nationality = tbNationality.Text;

            cukai.NationalityAttention.InsertOnSubmit(att);
            cukai.SubmitChanges();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
