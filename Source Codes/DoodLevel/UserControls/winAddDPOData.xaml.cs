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
    /// Interaction logic for winAddDPOData.xaml
    /// </summary>
    public partial class winAddDPOData : Window
    {
        public winAddDPOData()
        {
            InitializeComponent();

            DataContext = this;
        }

        private string _firstName = "";
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }

        private string _lastName = "";
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            } 
        }

        private string _passport = "";
        public string Passport 
        {
            get { return _passport; }
            set
            {
                _passport = value;
            }
        }

        private void ButtonDropDown_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Isikan First Name", "Invalid", MessageBoxButton.OK);
                return;
            }

            DialogResult = true;

            BeaCukai cukai = new BeaCukai();
            PAUDPO dpo = new PAUDPO();
            dpo.FirstName = FirstName;
            dpo.LastName = LastName;
            dpo.Passport = Passport;

            cukai.PAUDPO.InsertOnSubmit(dpo);
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
