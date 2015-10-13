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
using Microsoft.Windows.Controls;

namespace PAU.UserControls
{
    /// <summary>
    /// Interaction logic for ucNationality.xaml
    /// </summary>
    public partial class ucNationality : UserControl
    {
        public ucNationality()
        {
            InitializeComponent();
        }

        public DataGrid DataGrid
        {
            get { return dgNationality; }
        }

        private void dgNationality_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            BeaCukai cukai = new BeaCukai();
            NationalityAttention nationality = e.Row.Item as NationalityAttention;

            if (nationality != null)
            {
                var existing = (from p in cukai.NationalityAttention where p.ID == nationality.ID select p).First();

                existing.Nationality = nationality.Nationality;

                cukai.SubmitChanges();
            }


        }

        private void dgNationality_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    e.Handled = false;

                    return;
                }

                e.Handled = true;
            }

        }
    }
}
