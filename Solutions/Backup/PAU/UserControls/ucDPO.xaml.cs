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
    /// Interaction logic for ucDPO.xaml
    /// </summary>
    public partial class ucDPO : UserControl
    {
        public ucDPO()
        {
            InitializeComponent();
        }

        public ucDPO(object context)
        {
            InitializeComponent();
            Context = context;
        }

        public object Context
        {
            get { return dgDPO.DataContext; }
            set
            {
                dgDPO.DataContext = value;
            }
        }

        public DataGrid DataGrid
        {
            get { return dgDPO; }
        }

        private void dgDPO_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            BeaCukai cukai = new BeaCukai();
            PAUDPO dpo = e.Row.Item as PAUDPO;

            if (dpo != null)
            {
                var existing = (from p in cukai.PAUDPO where p.ID == dpo.ID select p).First();

                existing.Name = dpo.Name;
                existing.FirstName = dpo.FirstName;
                existing.LastName = dpo.LastName;
                existing.Passport = dpo.Passport;

                cukai.SubmitChanges();
            }

        }

        private void dgDPO_PreviewKeyDown(object sender, KeyEventArgs e)
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
