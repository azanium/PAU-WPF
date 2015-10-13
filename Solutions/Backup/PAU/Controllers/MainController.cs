using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.Win32;
using Microsoft.Windows.Controls;

using DevComponents.WpfDock;

using MySql.Data.MySqlClient;

using PAU.Controllers.Interfaces;
using PAU.Documents;
using PAU.UserControls;


namespace PAU.Controllers
{
    public class MainController : ControllerBase
    {
        #region MemVars & Props

        private static MainController _instance = null;
        public static MainController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainController();
                }
                return _instance;
            }
        }

        public enum eSearchCriteria
        {
            FirstName = 0,
            LastName,
            Passport,
            FlightDate,
            FlightNo
        }

        public eSearchCriteria SearchCriteriaFirst
        {
            get
            {
                return (eSearchCriteria)SearchCriteriaIndex1;
            }
        }

        public eSearchCriteria SearchCriteriaSecond
        {
            get
            {
                return (eSearchCriteria)SearchCriteriaIndex2;
            }
        }

        private string _searchText1 = "";
        public string SearchText1
        {
            get { return _searchText1; }
            set
            {
                if (_searchText1 != value)
                {
                    _searchText1 = value;
                }
                NotifyPropertyChanged("SearchText1");
            }
        }

        private string _searchText2 = "";
        public string SearchText2
        {
            get { return _searchText2; }
            set
            {
                if (_searchText2 != value)
                {
                    _searchText2 = value;
                }
                NotifyPropertyChanged("SearchText2");
            }
        }

        private int _searchCriteriaIndex1 = 2;
        public int SearchCriteriaIndex1
        {
            get { return _searchCriteriaIndex1; }
            set
            {
                if (_searchCriteriaIndex1 != value)
                {
                    _searchCriteriaIndex1 = value;
                }
                NotifyPropertyChanged("SearchCriteriaIndex1");
            }
        }

        private int _searchCriteriaIndex2 = 1;
        public int SearchCriteriaIndex2
        {
            get { return _searchCriteriaIndex2; }
            set
            {
                if (_searchCriteriaIndex2 != value)
                {
                    _searchCriteriaIndex2 = value;
                }
                NotifyPropertyChanged("SearchCriteriaIndex2");
            }
        }

        public enum ePagingAction
        {
            Passengers,
            Search
        }
        public ePagingAction PagingAction = ePagingAction.Passengers;

        public UserControl BottomPaneTool
        {
            get;
            set;
        }

        public Canvas RootCanvas
        {
            get;
            set;
        }

        public Canvas ViewCanvas
        {
            get;
            set;
        }

        public DockSite AppDock
        {
            get;
            set;
        }

        public DockWindowGroup DockGroup
        {
            get;
            set;
        }

        public ucPassenger PassengerGrid
        {
            get;
            set;
        }

        public ucDPO DPOGrid
        {
            get;
            set;
        }

        public ucNationality NationalityGrid
        {
            get;
            set;
        }

        private int _rowPerPage = 100;
        public int RowPerPage
        {
            get { return _rowPerPage; }
            set
            {
                if (_rowPerPage != value)
                {
                    if (value > 0)
                    {
                        _rowPerPage = value;
                    }
                }

                NotifyPropertyChanged("RowPerPage");
            }
        }

        private int _bookingDateRange = 3;
        public int BookingDateRange
        {
            get { return _bookingDateRange; }
            set
            {
                if (_bookingDateRange != value)
                {
                    _bookingDateRange = value;
                }

                NotifyPropertyChanged("BookingDateRange");
            }
        }

        private int _offsetPassengers = 0;
        public int OffsetPassengers
        {
            get { return _offsetPassengers; }
            set
            {
                if (_offsetPassengers != value)
                {
                    _offsetPassengers = value;

                    NotifyPropertyChanged("OffsetPassengers");
                }
            }
        }

        private int _currentPassengerPage = 1;
        public int CurrentPassengerPage
        {
            get { return _currentPassengerPage; }
            set
            {
                if (_currentPassengerPage != value)
                {
                    _currentPassengerPage = value;

                    NotifyPropertyChanged("CurrentPassengerPage");
                }
            }
        }

        private int _totalPassengerCount = 0;
        public int TotalPassengersCount
        {
            get { return _totalPassengerCount; }
            set
            {
                if (_totalPassengerCount != value)
                {
                    _totalPassengerCount = value;

                    NotifyPropertyChanged("TotalPassengerCount");
                }
            }
        }

        public int TotalPassengerPage
        {
            get 
            {
                if (TotalPassengersCount < 1)
                {
                    return 1;
                }

                return (int)Math.Ceiling((float)TotalPassengersCount / (float)RowPerPage); 
            }
        }

        #endregion


        #region Ctor

        public MainController()
        {
        }

        #endregion


        #region Public Methods

        Dictionary<DockWindow, DataGrid> _mapWindowToGrid = new Dictionary<DockWindow, DataGrid>();

        public override void Loaded(UIElement view)
        {
            base.Loaded(view);

            ResetBindings();

            //ResetDatabases();
        }

        public void ResetBindings()
        {
            this.Bind(Commands.ImportManifest, this.ImportManifest, this.CanImportManifest);
            this.Bind(Commands.ImportManifest2, this.ImportManifest2, this.CanImportManifest2);
            this.Bind(Commands.RefreshData, this.RefreshData);
            this.Bind(Commands.AddDPOData, this.AddDPOData);
            this.Bind(Commands.ConnectionSettings, this.ConnectionSettings);
            this.Bind(Commands.AddWNAttention, this.AddWNAttention);
            this.Bind(Commands.BackPage, this.GoBackPage, this.CanGoBackPage);
            this.Bind(Commands.NextPage, this.GoNextPage, this.CanGoNextPage);
        }


        #endregion


        #region Application Menu Delegates

        public void LoadPassengers(int page)
        {
            BeaCukai cukai = new BeaCukai();

            var queryPassengers = (from p in cukai.PAUPassenger select p);

            OffsetPassengers = (page - 1) * RowPerPage;
            CurrentPassengerPage = page;

            ResetPassengers(queryPassengers.Skip(OffsetPassengers).Take(RowPerPage));
        }

        private void CanGoBackPage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentPassengerPage > 1;
        }

        private void GoBackPage(object sender, ExecutedRoutedEventArgs e)
        {
            switch (PagingAction)
            {
                case ePagingAction.Passengers:
                    LoadPassengers(CurrentPassengerPage - 1);
                    break;

                case ePagingAction.Search:
                    FilterPassengersPaged(CurrentPassengerPage - 1);
                    break;
            }
        }

        private void CanGoNextPage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentPassengerPage < TotalPassengerPage;
        }

        private void GoNextPage(object sender, ExecutedRoutedEventArgs e)
        {
            switch (PagingAction)
            {
                case ePagingAction.Passengers:
                    LoadPassengers(CurrentPassengerPage + 1);
                    break;

                case ePagingAction.Search:
                    FilterPassengersPaged(CurrentPassengerPage + 1);
                    break;
            }
        }

        private void RefreshData(object sender, ExecutedRoutedEventArgs e)
        {
            ResetDatabases();
        }

        private void ConnectionSettings(object sender, ExecutedRoutedEventArgs e)
        {
            winMySqlConnection con = new winMySqlConnection();
            con.ShowDialog();
        }

        #endregion


        #region Database

        public void DisplayAttentions(ICollectionView colview, string header)
        {
            if (colview == null)
            {
                return;
            }

            if (DPOS == null)
            {
                MessageBox.Show("Please refresh the database first before attentions check", "Warning!", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            var pass = colview.OfType<PAUPassenger>();

            ObservableCollection<PAUPassenger> attentions = new ObservableCollection<PAUPassenger>();
            ObservableCollection<PAUPassenger> wnAttentions = new ObservableCollection<PAUPassenger>();
            ObservableCollection<PAUPassenger> dpos = new ObservableCollection<PAUPassenger>();

            bool found = false;
            
            foreach (PAUPassenger p in pass)
            {
                TimeSpan span = p.FlightDate.Value.Subtract(p.Date.Value);
                if (span <= TimeSpan.FromDays(BookingDateRange))
                {
                    attentions.Add(p);
                    found = true;
                }

                int count = (from dpo in DPOS
                             where
                             (p.FirstName.ToUpper().Contains(dpo.FirstName.ToUpper()) &&
                              p.LastName.ToUpper().Contains(dpo.LastName.ToUpper()))

                             //p.Passport.Equals(dpo.Passport)
                             select dpo).Distinct().Count();

                if (count > 0)
                {
                    dpos.Add(p);
                }
                
                foreach (NationalityAttention nat in Nationalities)
                {
                    if (p.Nationality != null)
                    {
                        if (p.Nationality.Equals(nat.Nationality, StringComparison.OrdinalIgnoreCase))
                        {
                            if (wnAttentions.Contains(p) == false)
                            {
                                wnAttentions.Add(p);
                                found = true;
                            }
                        }
                    }
                }
            }

            if (attentions.Count > 0)
            {
                DataGrid attentionGrid;
                SpawnNewDockWindow(string.Format("Attentions ({0})", header), out attentionGrid);
                if (attentionGrid != null)
                {
                    attentionGrid.ItemsSource = CollectionViewSource.GetDefaultView(attentions);
                }
            }

            if (wnAttentions.Count > 0)
            {
                DataGrid wnAttGrid;
                SpawnNewDockWindow(string.Format("WN Att ({0})", header), out wnAttGrid);
                if (wnAttGrid != null)
                {
                    wnAttGrid.ItemsSource = CollectionViewSource.GetDefaultView(wnAttentions);
                }
            }

            if (dpos.Count > 0)
            {
                DataGrid dpoGrid;
                SpawnNewDockWindow(string.Format("Name(s) On DPO ({0})", header), out dpoGrid);
                if (dpoGrid != null)
                {
                    dpoGrid.ItemsSource = CollectionViewSource.GetDefaultView(dpos);
                }
            }

            if (!found)
            {
                MessageBox.Show("No Attentions found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ResetDatabases()
        {
            PagingAction = ePagingAction.Passengers;

            BeaCukai cukai = new BeaCukai();

            var queryPassengers = (from p in cukai.PAUPassenger select p);
            var queryDPOS = (from d in cukai.PAUDPO select d);
            var nationality = (from d in cukai.NationalityAttention select d);

            OffsetPassengers = 0;
            TotalPassengersCount = queryPassengers.Distinct().Count();
            CurrentPassengerPage = 1;

            NotifyPropertyChanged("TotalPassengerPage");
            NotifyPropertyChanged("TotalPassengersCount");

            ResetPassengers(queryPassengers.Take(RowPerPage));
            ResetDPOS(queryDPOS);
            ResetNationalityAttentions(nationality);
        }

        private System.Linq.Expressions.Expression<Func<PAUPassenger, bool>> GetSearchExpression(eSearchCriteria criteria, string searchText)
        {
            System.Linq.Expressions.Expression<Func<PAUPassenger, bool>> expr = 
                p => p.FirstName.ToLower().Contains(SearchText1.ToLower());

            DateTime flightDate;
            if (!ConvertStringToDate(searchText, out flightDate) && criteria == eSearchCriteria.FlightDate)
            {
                MessageBox.Show("Invalid Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            switch (criteria)
            {
                case eSearchCriteria.LastName:
                    expr = p => p.LastName.ToLower().Contains(searchText.ToLower());
                    break;

                case eSearchCriteria.FlightNo:
                    expr = p => p.FlightNo.ToLower().Contains(searchText.ToLower());
                    break;

                case eSearchCriteria.Passport:
                    expr = p => p.Passport.ToLower().Contains(searchText.ToLower());
                    break;

                case eSearchCriteria.FlightDate:
                    expr = p => p.FlightDate.Value.Date.Day == flightDate.Day &&
                        p.FlightDate.Value.Date.Month == flightDate.Month &&
                        p.FlightDate.Value.Date.Year == flightDate.Year;
                    break;
            }

            return expr;
        }

        private System.Linq.Expressions.Expression<Func<PAUPassenger, bool>> LogicAnd()
        {
            System.Linq.Expressions.ParameterExpression pau = System.Linq.Expressions.Expression.Parameter(typeof(PAUPassenger), "p");

            if (string.IsNullOrEmpty(SearchText1) == false && string.IsNullOrEmpty(SearchText2) == false)
            {
                System.Linq.Expressions.Expression leftExpr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaFirst, SearchText1), pau);

                System.Linq.Expressions.Expression rightExpr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaSecond, SearchText2), pau);

                System.Linq.Expressions.Expression expr = System.Linq.Expressions.Expression.And(leftExpr, rightExpr);

                return System.Linq.Expressions.Expression.Lambda<Func<PAUPassenger, bool>>(expr, pau);
            }

            if (string.IsNullOrEmpty(SearchText1) == false && string.IsNullOrEmpty(SearchText2))
            {
                System.Linq.Expressions.Expression expr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaFirst, SearchText1), pau);

                return System.Linq.Expressions.Expression.Lambda<Func<PAUPassenger, bool>>(expr, pau);
            }

            if (string.IsNullOrEmpty(SearchText1) && string.IsNullOrEmpty(SearchText2) == false)
            {
                System.Linq.Expressions.Expression expr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaSecond, SearchText2), pau);

                return System.Linq.Expressions.Expression.Lambda<Func<PAUPassenger, bool>>(expr, pau);
            }

            return null;
        }

        private System.Linq.Expressions.Expression<Func<PAUPassenger, bool>> LogicOr()
        {
            System.Linq.Expressions.ParameterExpression pau = System.Linq.Expressions.Expression.Parameter(typeof(PAUPassenger), "p");

            if (string.IsNullOrEmpty(SearchText1) == false && string.IsNullOrEmpty(SearchText2) == false)
            {
                System.Linq.Expressions.Expression leftExpr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaFirst, SearchText1), pau);

                System.Linq.Expressions.Expression rightExpr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaSecond, SearchText2), pau);

                System.Linq.Expressions.Expression expr = System.Linq.Expressions.Expression.Or(leftExpr, rightExpr);

                return System.Linq.Expressions.Expression.Lambda<Func<PAUPassenger, bool>>(expr, pau);
            }

            if (string.IsNullOrEmpty(SearchText1) == false && string.IsNullOrEmpty(SearchText2))
            {
                System.Linq.Expressions.Expression expr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaFirst, SearchText1), pau);

                return System.Linq.Expressions.Expression.Lambda<Func<PAUPassenger, bool>>(expr, pau);
            }

            if (string.IsNullOrEmpty(SearchText1) && string.IsNullOrEmpty(SearchText2) == false)
            {
                System.Linq.Expressions.Expression expr = System.Linq.Expressions.Expression.Invoke(
                    GetSearchExpression(SearchCriteriaSecond, SearchText2), pau);

                return System.Linq.Expressions.Expression.Lambda<Func<PAUPassenger, bool>>(expr, pau);
            }

            return null;
        }

        public void FilterPassengersPaged(int page)
        {
            /*
            BeaCukai cukai = new BeaCukai();
            var queryPassengers = (from p in cukai.PAUPassenger select p);

            OffsetPassengers = (page - 1) * RowPerPage;
            CurrentPassengerPage = page;

            ResetPassengers(queryPassengers.Skip(OffsetPassengers).Take(RowPerPage));*/

            BeaCukai cukai = new BeaCukai();

            System.Linq.Expressions.Expression<Func<PAUPassenger, bool>> expr = null;

            if (SearchSelectedIndex == 0)
            {
                expr = LogicAnd();
            }
            else
            {
                expr = LogicOr();
            }

            IQueryable<PAUPassenger> query = null;
            if (expr != null)
            {
                query = (from p in cukai.PAUPassenger select p).Where(expr);
            }

            if (null != query)
            {
                OffsetPassengers = (page - 1) * RowPerPage;
                CurrentPassengerPage = page;

                ResetPassengers(query.Skip(OffsetPassengers).Take(RowPerPage));
            }
        }

        public void FilterPassengers()
        {
            PagingAction = ePagingAction.Search;

            BeaCukai cukai = new BeaCukai();

            System.Linq.Expressions.Expression<Func<PAUPassenger, bool>> expr = null;

            if (SearchSelectedIndex == 0)
            {
                expr = LogicAnd();
            }
            else
            {
                expr = LogicOr();
            }

            IQueryable<PAUPassenger> query = null;
            if (expr != null)
            {
                query = (from p in cukai.PAUPassenger select p).Where(expr);
            }

            if (null != query)
            {
                OffsetPassengers = 0;
                TotalPassengersCount = query.Distinct().Count();
                CurrentPassengerPage = 1;

                NotifyPropertyChanged("TotalPassengerPage");
                NotifyPropertyChanged("TotalPassengersCount");

                ResetPassengers(query.Take(RowPerPage));
            }
        }

        private IQueryable<PAUPassenger> FilterQueryPassenger(ref IQueryable<PAUPassenger> query, eSearchCriteria criteria, string searchText)
        {
            switch (criteria)
            {
                case eSearchCriteria.FirstName:
                    query.Where(p => p.FirstName.ToLower().Contains(searchText.ToLower()));
                    break;

                case eSearchCriteria.LastName:
                    query.Where(p => p.LastName.ToLower().Contains(searchText.ToLower()));
                    break;

                case eSearchCriteria.FlightNo:
                    query.Where(p => p.FlightNo.ToLower().Contains(searchText.ToLower()));
                    break;

                case eSearchCriteria.FlightDate:
                    break;

                case eSearchCriteria.Passport:
                    query.Where(p => p.Passport.ToLower().Contains(searchText.ToLower()));
                    break;
            }

            return query;
        }

        private void ResetPassengers(IEnumerable<PAUPassenger> list)
        {
            if (Passengers != null)
            {
                Passengers.CollectionChanged -= new NotifyCollectionChangedEventHandler(Passengers_CollectionChanged);
            }

            Passengers = new ObservableCollection<PAUPassenger>(list.ToList());

            int count = 1;
            foreach (var p in Passengers)
            {
                p.No = OffsetPassengers + (count++);
            }

            Passengers.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Passengers_CollectionChanged);

            PassengerGrid.DataGrid.ItemsSource = CollectionViewSource.GetDefaultView(Passengers);
        }

        private void ResetNationalityAttentions(IEnumerable<NationalityAttention> list)
        {
            if (Nationalities != null)
            {
               Nationalities.CollectionChanged -= new NotifyCollectionChangedEventHandler(Nationalities_CollectionChanged);
            }

            Nationalities = new ObservableCollection<NationalityAttention>(list.ToList());

            int count = 1;
            foreach (var p in Nationalities)
            {
                p.No = count++;
            }

            Nationalities.CollectionChanged += new NotifyCollectionChangedEventHandler(Nationalities_CollectionChanged);

            NationalityGrid.DataGrid.ItemsSource = CollectionViewSource.GetDefaultView(Nationalities);
        }

        private void ResetDPOS(IEnumerable<PAUDPO> list)
        {
            if (DPOS != null)
            {
                DPOS.CollectionChanged -= new NotifyCollectionChangedEventHandler(DPOS_CollectionChanged);
            }

            DPOS = new ObservableCollection<PAUDPO>(list.ToList());

            int count = 1;
            foreach (var p in DPOS)
            {
                p.No = count++;
            }

            DPOS.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(DPOS_CollectionChanged);

            DPOGrid.DataGrid.ItemsSource = CollectionViewSource.GetDefaultView(DPOS);
        }
        private bool _canEditPassengers = false;
        public bool CanEditPassengers
        {
            get { return _canEditPassengers; }
            set
            {
                if (_canEditPassengers != value)
                {
                    _canEditPassengers = value;
                    NotifyPropertyChanged("CanEditPassengers");
                    NotifyPropertyChanged("IsReadOnlyPassengers");
                }
            }
        }

        public bool IsReadOnlyPassengers
        {
            get { return !CanEditPassengers; }
        }

        public ObservableCollection<PAUDPO> DPOS
        {
            get;
            set;
        }

        public ObservableCollection<PAUPassenger> Passengers
        {
            get;
            set;
        }

        public ObservableCollection<NationalityAttention> Nationalities
        {
            get;
            set;
        }

        private void Nationalities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                BeaCukai cukai = new BeaCukai();

                foreach (NationalityAttention att in e.OldItems)
                {
                    cukai.NationalityAttention.DeleteOnSubmit(att);
                }
                cukai.SubmitChanges();
            }
        }

        private void DPOS_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                //if (MessageBox.Show("Are you sure you want to delete the record(s)?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    BeaCukai cukai = new BeaCukai();

                    foreach (PAUDPO dpo in e.OldItems)
                    {
                        cukai.PAUDPO.DeleteOnSubmit(dpo);
                    }
                    cukai.SubmitChanges();
                }
            }
        }

        private void Passengers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                //if (MessageBox.Show("Are you sure you want to delete the record(s)?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    BeaCukai cukai = new BeaCukai();

                    foreach (PAUPassenger passenger in e.OldItems)
                    {
                        cukai.PAUPassenger.DeleteOnSubmit(passenger);
                    }
                    cukai.SubmitChanges();
                }
            }
        }

        #endregion


        #region Import Menus


        private void AddDPOData(object sender, ExecutedRoutedEventArgs e)
        {
            winAddDPOData win = new winAddDPOData();
            if (win.ShowDialog().Value)
            {
                BeaCukai cukai = new BeaCukai();
                ResetDPOS(from s in cukai.PAUDPO select s);
            }
        }

        private void AddWNAttention(object sender, ExecutedRoutedEventArgs e)
        {
            winAddWNAttention win = new winAddWNAttention();
            if (win.ShowDialog().Value)
            {
                BeaCukai cukai = new BeaCukai();
                ResetNationalityAttentions(from s in cukai.NationalityAttention select s);
            }
        }

        private ObservableCollection<PAUPassenger> _importedPassengers = new ObservableCollection<PAUPassenger>();
        public ObservableCollection<PAUPassenger> ImportedPassengers
        {
            get { return _importedPassengers; }
            set
            {
                _importedPassengers = value;
                NotifyPropertyChanged("ImportedPassengers");
            }
        }

        private bool IsDateSame(DateTime? dt1, DateTime? dt2)
        {
            if (dt1 == null || dt2 == null)
            {
                return false;
            }

            return dt1.Value.Year == dt2.Value.Year && dt1.Value.Month == dt2.Value.Month && dt1.Value.Day == dt2.Value.Day;
        }

        private class PAXObj
        {
            public DateTime flightDate;
            public string flightNo;
            public List<PAUPassenger> passengers;
            public PAXObj(string no, DateTime date, List<PAUPassenger> pass)
            {
                flightDate = date;
                flightNo = no;
                passengers = pass;
            }
        }

        private bool _isReady = true;

        private void ProcessManifestPAX(string filename)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += (s, e) =>
                {
                    BackgroundWorker sender = s as BackgroundWorker;
                    DateTime date;
                    string no;
                    List<PAUPassenger> pass;
                    ReadAirAsia(sender, filename, out date, out no, out pass);
                    sender.ReportProgress(-1, new PAXObj(no, date, pass));
                };

            worker.ProgressChanged += (s, e) =>
                {
                    if (e.ProgressPercentage >= 0)
                    {
                        StatusText = "Processing line: " + e.ProgressPercentage + " => " + (e.UserState as string);
                    }
                    else if (e.ProgressPercentage == -1)
                    {
                        PAXObj obj = e.UserState as PAXObj;
                        DateTime flightDate = obj.flightDate;
                        string flightNo = obj.flightNo;
                        List<PAUPassenger> passengers = obj.passengers;

                        DataGrid importGrid;
                        SpawnNewDockWindow("Recent Import", out importGrid);

                        filename = Path.GetFileName(filename);

                        StatusText = "Please wait... import process might take a while...";

                        if (importGrid != null)
                        {
                            BeaCukai cukai = new BeaCukai();

                            if (passengers.Count > 0)
                            {
                                foreach (PAUPassenger passenger in passengers)
                                {
                                    int count = (from p in cukai.PAUPassenger select p).Where(p => p.FirstName.ToLower() == passenger.FirstName.ToLower()).
                                        Where(p => p.LastName.ToLower() == passenger.LastName.ToLower()).
                                        Where(p => p.FlightNo.ToLower() == passenger.FlightNo.ToLower()).
                                        Where(p => p.FlightDate.Value.Date.Day == passenger.FlightDate.Value.Date.Day && p.FlightDate.Value.Date.Month == passenger.FlightDate.Value.Date.Month &&
                                                   p.FlightDate.Value.Date.Year == passenger.FlightDate.Value.Date.Year).
                                        Distinct().Count();

                                    if (count == 0)
                                    {
                                        cukai.PAUPassenger.InsertOnSubmit(passenger);
                                    }
                                }
                                cukai.SubmitChanges();
                            }

                            ImportedPassengers = new ObservableCollection<PAUPassenger>(passengers);
                            ObservableCollection<PAUPassenger> attentions = new ObservableCollection<PAUPassenger>();
                            ObservableCollection<PAUPassenger> dpos = new ObservableCollection<PAUPassenger>();
                            ObservableCollection<PAUPassenger> wnAttentions = new ObservableCollection<PAUPassenger>();

                            int index = 1;
                            foreach (PAUPassenger p in ImportedPassengers)
                            {
                                TimeSpan span = p.FlightDate.Value.Subtract(p.Date.Value);
                                if (span <= TimeSpan.FromDays(BookingDateRange))
                                {
                                    attentions.Add(p);
                                }

                                p.No = index++;

                                int count = (from dpo in cukai.PAUDPO
                                             where
                                             (p.FirstName.ToLower().Contains(dpo.FirstName.ToLower()) &&
                                              p.LastName.ToLower().Contains(dpo.LastName.ToLower()))
                                             select dpo).Count();

                                if (count > 0)
                                {
                                    dpos.Add(p);
                                }

                                count = (from nat in cukai.NationalityAttention
                                         where
                                         (nat.Nationality.ToLower() == p.Nationality.ToLower())
                                         select nat).Distinct().Count();

                                if (count > 0)
                                {
                                    if (wnAttentions.Contains(p) == false)
                                    {
                                        wnAttentions.Add(p);
                                    }
                                }
                            }

                            importGrid.ItemsSource = CollectionViewSource.GetDefaultView(ImportedPassengers);

                            if (attentions.Count > 0)
                            {
                                DataGrid attentionGrid;
                                SpawnNewDockWindow(string.Format("Attentions ({0})", filename), out attentionGrid);
                                if (attentionGrid != null)
                                {
                                    attentionGrid.ItemsSource = CollectionViewSource.GetDefaultView(attentions);
                                }
                            }

                            if (wnAttentions.Count > 0)
                            {
                                DataGrid wnAttGrid;
                                SpawnNewDockWindow(string.Format("WN Att ({0})", filename), out wnAttGrid);
                                if (wnAttGrid != null)
                                {
                                    wnAttGrid.ItemsSource = CollectionViewSource.GetDefaultView(wnAttentions);
                                }
                            }

                            if (dpos.Count > 0)
                            {
                                DataGrid dpoGrid;
                                SpawnNewDockWindow(string.Format("Name(s) On DPO ({0})", filename), out dpoGrid);
                                if (dpoGrid != null)
                                {
                                    dpoGrid.ItemsSource = CollectionViewSource.GetDefaultView(dpos);
                                }
                            }

                            ResetDatabases();

                            MessageBox.Show("Import Data Selesai", "Infomasi", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            _isReady = true;
                            StatusText = "Ready";
                        }
                    }
                };

            _isReady = false;

            worker.RunWorkerAsync();

        }

        private void ProcessManifestApis(string filename)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            
            _isReady = false;
            
            worker.DoWork += (s, e) =>
                {
                    string no;
                    DateTime date;
                    List<PAUPassenger> pass;
                    ReadAirAsiaDetails(worker, filename, out date, out no, out pass);
                    worker.ReportProgress(-1, new PAXObj(no, date, pass));
                };

            worker.ProgressChanged += (s, e) =>
                {
                    if (e.ProgressPercentage > 0)
                    {
                        string line = e.UserState as string;
                        StatusText = "Processing Line " + e.ProgressPercentage + " => " + line;
                    }
                    else  if (e.ProgressPercentage == -1)
                    {
                        PAXObj obj = e.UserState as PAXObj;

                        BeaCukai cukai = new BeaCukai();

                        IEnumerable<PAUPassenger> pass = from p in cukai.PAUPassenger
                                                         where
                                                           (p.FlightNo.ToLower() == obj.flightNo.ToLower() &&
                                                            p.FlightDate.Value.Date.Day == obj.flightDate.Date.Day &&
                                                            p.FlightDate.Value.Date.Month == obj.flightDate.Date.Month &&
                                                            p.FlightDate.Value.Date.Year == obj.flightDate.Date.Year)
                                                         select p;


                        ImportedPassengers = new ObservableCollection<PAUPassenger>(pass);

                        DataGrid importGrid;
                        SpawnNewDockWindow("Recent Import", out importGrid);

                        if (importGrid != null)
                        {
                            importGrid.ItemsSource = CollectionViewSource.GetDefaultView(ImportedPassengers);
                        }

                        ResetDatabases();

                        MessageBox.Show("Import Data Selesai", "Infomasi", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        _isReady = true;
                        StatusText = "Ready";
                    }
                    
                };

            worker.RunWorkerAsync();
        }

        private void CanImportManifest(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isReady;
        }

        private void ImportManifest(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.DefaultExt = "*.txt";
            dlgOpen.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (dlgOpen.ShowDialog().Value)
            {
                string filename = dlgOpen.FileName;

                ProcessManifestPAX(filename);
            }
        }

        private void CanImportManifest2(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isReady;
        }

        private void ImportManifest2(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.DefaultExt = "*.txt";
            dlgOpen.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (dlgOpen.ShowDialog().Value)
            {
                string filename = dlgOpen.FileName;

                ProcessManifestApis(filename);
            }
        }

        private bool SpawnNewDockWindow(string header, out DataGrid grid)
        {
            DockWindow window = null;
            bool exist = false;
            grid = null;

            foreach (DockWindow w in DockGroup.Items)
            {
                string h = "";
                if (w.Header is TextBlock)
                {
                    TextBlock tb = (TextBlock)w.Header;
                    h = tb.Text;
                }
                else if (w.Header is string)
                {
                    h = (string)w.Header;
                }

                if (h.Equals(header, StringComparison.OrdinalIgnoreCase))
                {
                    exist = true;
                    window = w;
                    break;
                }
            }

            if (!exist)
            {
                window = new DockWindow();
                window.CanAutoHide = false;
                window.CanFloat = false;
                window.CanTearOff = false;
                window.CanClose = true;
                window.Header = header;

                DockPanel panel = new DockPanel();
                panel.LastChildFill = true;

                window.Content = panel;

                ucPassenger passengerGrid = new ucPassenger(this, header, false, false);
                passengerGrid.PagingEnabled = false;
                panel.Children.Add(passengerGrid);
                grid = passengerGrid.DataGrid;

                _mapWindowToGrid.Add(window, grid);

                DockGroup.Items.Add(window);

            }
            else
            {
                if (_mapWindowToGrid.ContainsKey(window))
                {
                    grid = _mapWindowToGrid[window];
                }
            }

            window.Visibility = Visibility.Visible;
            int index = DockGroup.Items.IndexOf(window);
            DockGroup.SelectedIndex = index;

            return exist;
        }

        public static bool ConvertStringToDateIndo(string dateString, out DateTime date)
        {
            date = DateTime.Now;

            char[] separator = { '/', '-' };
            string[] dates = dateString.Split(separator);

            if (dates.Count() < 3)
            {
                return false;
            }

            try
            {
                date = new DateTime(Int32.Parse(dates[2]), Int32.Parse(dates[1]), Int32.Parse(dates[0]));
            }
            catch (Exception)
            {
                MessageBox.Show("Input tanggal salah", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return true;
        
        }

        public static bool CompareStringToDate(string dateStr, DateTime date)
        {
            DateTime dt;
            if (ConvertStringToDateIndo(dateStr, out dt))
            {
                if (dt.Year == date.Year &&
                    dt.Month == date.Month &&
                    dt.Day == date.Day)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ConvertPAXStringToDate(string dateString, out DateTime date)
        {
            date = DateTime.Now;

            if (string.IsNullOrEmpty(dateString))
            {
                return false;
            }

            string day = dateString.Substring(0, 2);
            string monthStr = dateString.Substring(2, 3);
            string year = dateString.Substring(5, 2);
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            int month = 1;
            foreach (string m in months)
            {
                if (monthStr.Equals(m, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                month++;
            }
            if (month > 12)
            {
                month = Int32.Parse(monthStr);
            }

            try
            {
                date = new DateTime(Int32.Parse(year), month, Int32.Parse(day));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return true;
        }

        public static bool ConvertStringToDate(string dateString, out DateTime date)
        {
            
            date = DateTime.Now;

            if (string.IsNullOrEmpty(dateString))
            {
                return false;
            }
            string[] dates = dateString.Split('/', '-');

            if (dates.Length != 3)
            {
                return false;
            }

            string day = dates[0];//dateString.Substring(0, 2);
            string monthStr = dates[1]; //dateString.Substring(2, 3);
            string year = dates[2]; //dateString.Substring(5, 2);
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            int month = 1;
            foreach (string m in months)
            {
                if (monthStr.Equals(m, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                month++;
            }
            if (month > 12)
            {
                month = Int32.Parse(monthStr);
            }

            try
            {
                date = new DateTime(Int32.Parse(year), month, Int32.Parse(day));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return true;
        }

        private void ReadAirAsiaDetails(BackgroundWorker worker, string filename, out DateTime flightDate, out string flightNo, out List<PAUPassenger> passangers)
        {
            flightDate = DateTime.Now;
            flightNo = "";
            passangers = new List<PAUPassenger>();

            using (StreamReader reader = new StreamReader(filename))
            {
                char[] separator = { ',', '/' };
                string line;

                string fno = "";
                DateTime flight_date = DateTime.MinValue;
                string num = "", pnr = "", last_name = "", first_name = "";
                string gender = "", dob = "", nat = "";

                bool start = false;
                bool parse = false;
               // bool found_first = false;

                BeaCukai cukai = new BeaCukai();
                
                int lineNo = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    if (worker != null)
                    {
                        worker.ReportProgress(lineNo, line);
                    }
                    lineNo++;

                    if (line.Contains("Pax Verification"))
                    {
                        start = true;
                        continue;
                    }

                    if (line.Contains("------------------") && start)
                    {
                        parse = true;
                        continue;
                    }

                    if (line.Trim() == "")
                    {
                        parse = false;
                    }

                    if (line.Contains("Flight") && line.Contains("Date:"))
                    {
                        int flightPos = line.IndexOf("Flight#:") + 8;
                        int datePos = line.IndexOf("Date:") + 5;
                        fno = line.Substring(flightPos, 6).Trim();
                        string fdate = line.Substring(datePos, 14).Trim();
                        string[] dates = fdate.Split(separator);
                        if (dates.Count() > 0)
                        {
                            ConvertPAXStringToDate(dates[0], out flightDate);
                            ConvertPAXStringToDate(dates[0], out flight_date);
                        }
                        flightNo = fno;
                        continue;
                    }

                    if (parse)
                    {
                        
                        int number = -1;
                        try
                        {
                            number = Int32.Parse(line.Substring(0, 3).Trim());
                        }
                        catch (Exception)
                        {
                            number = -1;
                        }

                        if (number > -1)
                        {
                            pnr = line.Substring(5, 6).Trim();
                            last_name = line.Substring(12, 25).Trim();
                            first_name = line.Substring(37, line.Length - 37).Trim();
                            num = number.ToString();

                            if ((line = reader.ReadLine()) != null)
                            {

                                //found_first = false;
                                char[] sep = { ' ' };
                                //string[] datas = line.Split(sep);
                                gender = line.Substring(11, 2).Trim();
                                dob = line.Substring(13, 6).Trim();
                                nat = line.Substring(22, 3).Trim();
                                //MessageBox.Show(line+#13);
                                PAUPassenger pass = null;
                                try
                                {
                                     pass = (from p in cukai.PAUPassenger select p).Where(p => p.FirstName.ToLower() == first_name.ToLower()).
                                                Where(p => p.LastName.ToLower() == last_name.ToLower()).
                                                Where(p => p.FlightNo.ToLower() == fno.ToLower()).
                                                Where(p => p.FlightDate.Value.Date.Day == flight_date.Date.Day && p.FlightDate.Value.Date.Month == flight_date.Date.Month && 
                                                           p.FlightDate.Value.Date.Year == flight_date.Date.Year).First();
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Trace.TraceError("MainController.ReadAirAsiaDetails: Rec No. " + num + "message=" + ex.Message);
                                    continue;
                                }
                                
                                if (pass != null)
                                {
                                    pass.PNR = pnr;
                                    pass.Gender = gender;
                                    pass.Nationality = nat;
                                    pass.BirthDate = ConvertMySqlToDate(dob);

                                    if (UpdateRecord(pass) == 0)
                                    {
                                        return;
                                    }

                                    passangers.Add(pass);
                                    
                                    /*MessageBox.Show(num + "," + pnr + ",'" + 
                                        last_name + "','" + first_name + "'" + "," + 
                                        gender + "," + 
                                        dob + "," + 
                                        nat);
                                    */
                                }
                            }
                        }


                    }

                }
            }
        }

        private string ConvertDateToMySql(DateTime date)
        {
            return string.Format("{0}-{1}-{2}", date.Year.ToString("D4"), date.Month.ToString("D2"), date.Day.ToString("D2"));
        }

        private DateTime ConvertMySqlToDate(string date)
        {
            int year = Int32.Parse(date.Substring(0, 2));
            string month = date.Substring(2, 2);
            string day = date.Substring(4, 2);
            if (year > 30)
            {
                year += 1900;
            }
            else
            {
                year += 2000;
            }

            return new DateTime(year, Int32.Parse(month), Int32.Parse(day));
        }

        private int UpdateRecord(PAUPassenger passenger)
        {
            int rowsAffected = 0;
            try
            {
                // 0 = pnr, 1 = gender, 2 = nationality
                // 4 = first_name, 5 = last_name, 6 = flight_date, 7 = flight_no
                string sql = string.Format("UPDATE pau_passenger SET pnr='{0}', gender='{1}', nationality='{2}', birth_date='{3}' " +
                                           "WHERE first_name='{4}' && last_name='{5}' && flight_date='{6}' && flight_no='{7}'",
                                           passenger.PNR, passenger.Gender, passenger.Nationality, ConvertDateToMySql(passenger.BirthDate.Value),
                                           passenger.FirstName, passenger.LastName, ConvertDateToMySql(passenger.FlightDate.Value), passenger.FlightNo);
                //MessageBox.Show(passenger.Gender+"\n"+sql);
                MySqlConnection con = new MySqlConnection(MySqlSettings.Instance.ConnectionString());
                MySqlCommand cmd = new MySqlCommand(sql, con);

                con.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            return rowsAffected;
        }

        private void ReadAirAsia(BackgroundWorker worker, string filename, out DateTime flightDate, out string flightNo, out List<PAUPassenger> passengers)
        {
            flightDate = DateTime.Now;
            flightNo = "";
            passengers = new List<PAUPassenger>();
            using (StreamReader reader = new StreamReader(filename))
            {
                char[] separator = { ',', '/' };
                string line;
                bool parse = false;
                bool isNoShow = false;
                int lineNo = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    if (worker != null)
                    {
                        worker.ReportProgress(lineNo, line);
                    }
                    lineNo++;

                    if (line.Contains("Checked-in/Boarded:") || line.Contains("No Shows"))
                    {
                        parse = true;

                        if (line.Contains("No Shows"))
                        {
                            isNoShow = true;
                        }
                        else
                        {
                            isNoShow = false;
                        }

                        continue;
                    }

                    if (line.Trim() == "")
                    {
                        parse = false;
                    }

                    if (line.Contains("Flight:") && line.Contains("Date:"))
                    {
                        int flightPos = line.IndexOf("Flight:") + 7;
                        int datePos = line.IndexOf("Date:") + 5;
                        string fNo = line.Substring(flightPos, 6).Trim();
                        string date = line.Substring(datePos, 14).Trim();
                        string[] dates = date.Split(separator);
                        ConvertPAXStringToDate(dates[0], out flightDate);
                        flightNo = fNo;
                        continue;
                    }

                    if (parse && line.Length >= 79)
                    {
                        string num = line.Substring(0, 3).Trim();

                        bool acc = true;
                        try
                        {
                            int testNum = Int32.Parse(num);
                        }
                        catch (Exception)
                        {
                            acc = false;
                        }

                        if (acc)
                        {

                            string name = line.Substring(4, 26).Trim();

                            string[] names = name.Split(separator, 26);
                            string pnr = line.Substring(35, 6).Trim();
                            string fare_class = line.Substring(42, 7).Trim();
                            string seq_no = line.Substring(52, 3).Trim();
                            string date = line.Substring(55, 7).Trim();
                            string seat_no = line.Substring(64, 6).Trim();
                            string flight_no = line.Substring(75, 4).Trim();
                            //MessageBox.Show(num + "." + names[1] + "."+ names[0]+"."+pnr+"." + fare_class+"."+seq_no+"."+date+"."+seat_no+"."+flight_no);

                            DateTime dt;
                            ConvertPAXStringToDate(date, out dt);

                            PAUPassenger passenger = new PAUPassenger();
                            passenger.Date = dt;
                            passenger.FareClass = fare_class;
                            passenger.FlightNo = flight_no;
                            passenger.Gender = "-";
                            passenger.Name = name;
                            passenger.FirstName = names[1];
                            passenger.LastName = names[0];
                            passenger.Passport = "";
                            passenger.PNR = pnr;
                            passenger.SeatNo = seat_no;
                            passenger.SEQNo = seq_no;
                            passenger.FlightDate = flightDate;
                            passenger.Notes = isNoShow ? "No Show" : "";
                            passenger.CDst = "";

                            passengers.Add(passenger);
                        }
                    }
                }
                
            }

        }

        #endregion


        #region Search

        private int _searchSelectedIndex = 0;
        public int SearchSelectedIndex
        {
            get { return _searchSelectedIndex; }
            set
            {
                if (_searchSelectedIndex != value)
                {
                    _searchSelectedIndex = value;
                    NotifyPropertyChanged("SearchSelectedIndex");
                }
            }
        }

        private string _searchString = "";
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (_searchString != value)
                {
                    _searchString = value;
                    NotifyPropertyChanged("SearchString");
                }
            }
        }

        #endregion


        #region Status Bar

        private string _statusText = "Ready";
        public string StatusText
        {
            get { return _statusText; }
            set 
            {
                if (_statusText != value)
                {
                    _statusText = value;
                    NotifyPropertyChanged("StatusText");
                }
            }
        }


        #endregion
    }
}
