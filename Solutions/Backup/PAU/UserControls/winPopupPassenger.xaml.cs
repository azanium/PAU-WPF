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
using Microsoft.Windows.Controls;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace PAU.UserControls
{
    /// <summary>
    /// Interaction logic for winPopupPassenger.xaml
    /// </summary>
    public partial class winPopupPassenger : Window
    {
        public winPopupPassenger()
        {
            InitializeComponent();
        }

        public DataGrid DataGrid
        {
            get { return dgPassenger; }
        }

        private void UpdatePassenger(PAUPassenger passenger)
        {
            BeaCukai cukai = new BeaCukai();

            if (passenger != null)
            {
                try
                {
                    var existing = (from p in cukai.PAUPassenger
                                    where
                                        p.ID == passenger.ID
                                    select p).First();

                    existing.Name = passenger.Name;
                    existing.BirthDate = passenger.BirthDate;
                    existing.CDst = passenger.CDst;
                    existing.Date = passenger.Date;
                    existing.FareClass = passenger.FareClass;
                    existing.FirstName = passenger.FirstName;
                    existing.FlightDate = passenger.FlightDate;
                    existing.FlightNo = passenger.FlightNo;
                    existing.Gender = passenger.Gender;
                    existing.LastName = passenger.LastName;
                    existing.Nationality = passenger.Nationality;
                    existing.Passport = passenger.Passport;
                    existing.PNR = passenger.PNR;
                    existing.SeatNo = passenger.SeatNo;
                    existing.SEQNo = passenger.SEQNo;
                    existing.Notes = passenger.Notes;
                    existing.Picture = passenger.Picture;
                }
                catch (Exception)
                {
                    return;
                }

                cukai.SubmitChanges();
            }
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
                    existing.BirthDate = passenger.BirthDate;
                    existing.CDst = passenger.CDst;
                    existing.Date = passenger.Date;
                    existing.FareClass = passenger.FareClass;
                    existing.FirstName = passenger.FirstName;
                    existing.FlightDate = passenger.FlightDate;
                    existing.FlightNo = passenger.FlightNo;
                    existing.Gender = passenger.Gender;
                    existing.LastName = passenger.LastName;
                    existing.Nationality = passenger.Nationality;
                    existing.Passport = passenger.Passport;
                    existing.PNR = passenger.PNR;
                    existing.SeatNo = passenger.SeatNo;
                    existing.SEQNo = passenger.SEQNo;
                    existing.Notes = passenger.Notes;
                    existing.Picture = passenger.Picture;
                }
                catch (Exception)
                {
                    return;
                }

                cukai.SubmitChanges();
            }


        }

        private void DisplayImage(string path)
        {
            if (File.Exists(path))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(path);
                img.EndInit();
                imgPreview.Source = img;
                imgPreview.Stretch = Stretch.Uniform;
            }
        }

        private void dgPassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool first = true;
            foreach (PAUPassenger pass in e.AddedItems)
            {
                if (first)
                {
                    first = false;
                    if (pass.Picture != null)
                    {
                        DisplayImage(pass.Picture);
                    }
                }
            }
        }

        private void cbxEditMode_Click(object sender, RoutedEventArgs e)
        {
            dgPassenger.IsReadOnly = !cbxEditMode.IsChecked.Value;
        }

        private void btnLoadPicture_Click(object sender, RoutedEventArgs e)
        {
            PAUPassenger pass = (PAUPassenger)dgPassenger.SelectedItem;

            if (pass == null)
            {
                MessageBox.Show("Pilih rekod", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
    
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string photo = System.IO.Path.Combine(path, "Passenger Analysis Unit");
            if (System.IO.Directory.Exists(photo) == false)
            {
                System.IO.Directory.CreateDirectory(photo);
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Lokasi Gambar";
            dlg.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|All (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                string fname = dlg.FileName;
                string target = System.IO.Path.Combine(photo, System.IO.Path.GetFileName(fname));
                if (File.Exists(target))
                {
                    File.Delete(target);
                }

                File.Copy(fname, target);

                pass.Picture = target;
                UpdatePassenger(pass);

                DisplayImage(target);
            }

        }
    }
}
