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
using System.Reflection;
using System.ComponentModel;
using PAU.Controllers;
using System.Collections.ObjectModel;
using PAU.Documents;

namespace PAU.UserControls
{
    /// <summary>
    /// Interaction logic for ucPassenger.xaml
    /// </summary>
    public partial class ucPassenger : UserControl
    {
        #region MemVars & Props

        bool _canEdit = true;

        string _header = "";
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public object Context
        {
            get { return dgPassenger.DataContext; }
            set
            {
                dgPassenger.DataContext = value;
            }
        }

        public DataGrid DataGrid
        {
            get { return dgPassenger; }
        }

        public bool PagingEnabled
        {
            get { return pagingPanel.Visibility == Visibility.Visible; }
            set
            {
                pagingPanel.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private bool _localSearch = false;

        #endregion


        #region Ctor

        public ucPassenger()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(ucPassenger_Loaded);
        }

        public ucPassenger(object context, string header, bool canEdit, bool canAttention)
        {
            InitializeComponent();

            _localSearch = true;
            Context = context;      
            _header = header;
            _canEdit = canEdit;
            cbxEdit.Visibility = _canEdit ? Visibility.Visible : Visibility.Collapsed;
            btnFindAttention.Visibility = canAttention ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion


        #region Methods

        private void ucPassenger_Loaded(object sender, RoutedEventArgs e)
        {
            cbxSearchOp.SelectedIndex = 0;
            cbxSearchFieldName1.SelectedIndex = 0;
            cbxSearchFieldName2.SelectedIndex = 1;
        }

        private void dgPassenger_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            BeaCukai cukai = new BeaCukai();
            PAUPassenger passenger = e.Row.Item as PAUPassenger;
            
            if (passenger != null)
            {
                try
                {
                    var existing = (from p in cukai.PAUPassenger
                                    where
                                        /*p.Name == passenger.Name &&
                                        p.CDst == passenger.CDst &&
                                        p.FareClass == passenger.FareClass &&
                                        p.FirstName == passenger.FirstName &&
                                        p.FlightNo == passenger.FlightNo &&
                                        p.Gender == passenger.Gender &&
                                        p.LastName == passenger.LastName &&
                                        p.Nationality == passenger.Nationality &&
                                        p.Passport == passenger.Passport &&
                                        p.PNR == passenger.PNR &&
                                        p.SeatNo == passenger.SeatNo &&
                                        p.SEQNo == passenger.SEQNo &&
                                        p.Notes == passenger.Notes
                                        */
                                    p.ID == passenger.ID
                                    select p).First();

                    existing.Name = passenger.Name;
                    existing.BirthDate = new DateTime(passenger.BirthDate.Value.Year, passenger.BirthDate.Value.Month, passenger.BirthDate.Value.Day);
                    existing.CDst = passenger.CDst;
                    existing.Date = new DateTime(passenger.Date.Value.Year, passenger.Date.Value.Month, passenger.Date.Value.Day);
                    existing.FareClass = passenger.FareClass;
                    existing.FirstName = passenger.FirstName;
                    existing.FlightDate = new DateTime(passenger.FlightDate.Value.Year, passenger.FlightDate.Value.Month, passenger.FlightDate.Value.Day);
                    existing.FlightNo = passenger.FlightNo;
                    existing.Gender = passenger.Gender;
                    existing.LastName = passenger.LastName;
                    existing.Nationality = passenger.Nationality;
                    existing.Passport = passenger.Passport;
                    existing.PNR = passenger.PNR;
                    existing.SeatNo = passenger.SeatNo;
                    existing.SEQNo = passenger.SEQNo;
                    existing.Notes = passenger.Notes;
                }
                catch (Exception)
                {
                    return;
                }

                cukai.SubmitChanges();
            }
        }

        private void Filter()
        {
            if (DataGrid.ItemsSource is ICollectionView)
            {
                const int FIRST_NAME = 0;
                const int LAST_NAME = 1;
                const int PASSPORT = 2;
                const int FLIGHT_DATE = 3;
                const int FLIGHT_NO = 4;

                string searchText1 = tbSearchText1.Text;
                string searchText2 = tbSearchText2.Text;
                int index1 = cbxSearchFieldName1.SelectedIndex;
                int index2 = Math.Max(0, cbxSearchFieldName2.SelectedIndex);
                int op = Math.Max(0, cbxSearchOp.SelectedIndex);

                ICollectionView view = DataGrid.ItemsSource as ICollectionView;

                if (string.IsNullOrEmpty(searchText1) == false && string.IsNullOrEmpty(searchText2))
                {
                    view.Filter = delegate(object item)
                    {
                        PAUPassenger pass = (PAUPassenger)item;
                        
                        if (pass != null)
                        {
                            if (index1 == FIRST_NAME)
                            {
                                return pass.FirstName.ToUpper().Contains(searchText1.ToUpper());
                            }
                            if (index1 == LAST_NAME)
                            {
                                return pass.LastName.ToUpper().Contains(searchText1.ToUpper());
                            }
                            if (index1 == PASSPORT)
                            {
                                return pass.Passport.ToUpper().Contains(searchText1.ToUpper());
                            }
                            if (index1 == FLIGHT_DATE)
                            {
                                DateTime date;
                                if (MainController.ConvertStringToDateIndo(searchText1, out date))
                                {
                                    return pass.FlightDate.Value.Year == date.Year && pass.FlightDate.Value.Month == date.Month && pass.FlightDate.Value.Day == date.Day;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (index1 == FLIGHT_NO)
                            {
                                return pass.FlightNo.Equals(searchText1, StringComparison.OrdinalIgnoreCase);
                            }
                            
                        }
                        return false;
                    };
                }
                else if (string.IsNullOrEmpty(searchText1) == false && string.IsNullOrEmpty(searchText2) == false)
                {
                    view.Filter = delegate(object item)
                    {
                        PAUPassenger pass = (PAUPassenger)item;
                        if (pass != null)
                        {
                            bool fname1 = pass.FirstName.ToUpper().Contains(searchText1.ToUpper());
                            bool fname2 = pass.FirstName.ToUpper().Contains(searchText2.ToUpper());
                            bool lname1 = pass.LastName.ToUpper().Contains(searchText1.ToUpper());
                            bool lname2 = pass.LastName.ToUpper().Contains(searchText2.ToUpper());
                            bool pass1 = pass.Passport.ToUpper().Contains(searchText1.ToUpper());
                            bool pass2 = pass.Passport.ToUpper().Contains(searchText2.ToUpper());
                            bool fdate1 = index1 == FLIGHT_DATE ? MainController.CompareStringToDate(searchText1, pass.FlightDate.Value) : false;
                            bool fdate2 = index2 == FLIGHT_DATE ? MainController.CompareStringToDate(searchText2, pass.FlightDate.Value) : false;
                            bool fno1 = pass.FlightNo.Equals(searchText1, StringComparison.OrdinalIgnoreCase);
                            bool fno2 = pass.FlightNo.Equals(searchText2, StringComparison.OrdinalIgnoreCase);
                            bool[] logics1 = { fname1, lname1, pass1, fdate1, fno1 };
                            bool[] logics2 = { fname2, lname2, pass2, fdate2, fno2 };

                            for (int i = 0; i <= FLIGHT_NO; i++)
                            {
                                for (int j = 0; j <= FLIGHT_NO; j++)
                                {
                                    if (op == 0) // AND
                                    {
                                        if (logics1[i] && logics2[j])
                                        {
                                            return true;
                                        }
                                    }
                                    if (op == 1)  // OR
                                    {
                                        if (logics1[i] || logics2[j])
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                        return false;
                    };
                }
                else
                {
                    view.Filter = null;
                }
            }
        }

        private void tbSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Filter();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_localSearch)
            {
                Filter();
            }
            else
            {
                MainController.Instance.FilterPassengers();
            }
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            tbSearchText1.Text = "";
            tbSearchText2.Text = "";

            if (_localSearch)
            {
                Filter();
            }
            else
            {
                MainController.Instance.ResetDatabases();
            }
        }

        private void btnFindAttention_Click(object sender, RoutedEventArgs e)
        {
            MainController.Instance.DisplayAttentions(DataGrid.ItemsSource as ICollectionView, _header);
        }

        private void dgPassenger_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cbxEdit.IsChecked == false)
            {
                PAUPassenger pass = DataGrid.CurrentItem as PAUPassenger;

                if (pass != null)
                {
                    winPopupPassenger popup = new winPopupPassenger();

                    BeaCukai cukai = new BeaCukai();
                    var items = from p in cukai.PAUPassenger 
                                where
                                    (p.FirstName.ToUpper().Contains(pass.FirstName.ToUpper()) &&
                                     p.LastName.ToUpper().Contains(pass.LastName.ToUpper()))
                                select p;

                    ObservableCollection<PAUPassenger> history = new ObservableCollection<PAUPassenger>(items.ToList());
                    popup.DataGrid.ItemsSource = history;

                    popup.ShowDialog();
                }
            }
        }

        private void cbxEdit_Click(object sender, RoutedEventArgs e)
        {
            dgPassenger.IsReadOnly = !cbxEdit.IsChecked.Value;
        }

        private void dgPassenger_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && cbxEdit.IsChecked == true)
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    e.Handled = false;

                    return;
                }

                e.Handled = true;
            }
        }

        private void dgPassenger_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                List<string> columns = new List<string>();
                foreach (DataGridColumn col in dgPassenger.Columns)
                {
                    columns.Add(col.Header.ToString());
                }
                //dlg.PrintVisual(dgPassenger, "Passenger");
                DocPaginator doc = new DocPaginator(dgPassenger, new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight),
                    columns, false, null, null);

                dlg.PrintDocument(doc, "Passenger Analysis Unit");
            }
        }

        private void edtCurrentPage_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (edtCurrentPage.Value != MainController.Instance.CurrentPassengerPage && edtCurrentPage.Value != null)
            {
                switch (MainController.Instance.PagingAction)
                {
                    case MainController.ePagingAction.Passengers:
                        MainController.Instance.LoadPassengers(edtCurrentPage.Value.Value);
                        break;

                    case MainController.ePagingAction.Search:
                        MainController.Instance.FilterPassengersPaged(edtCurrentPage.Value.Value);
                        break;
                }
            }
        }

        #endregion

    }
}
