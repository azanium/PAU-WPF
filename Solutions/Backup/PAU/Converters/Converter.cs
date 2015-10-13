using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;

using PAU.Controllers;

namespace PAU.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime && value != null)
            {
                DateTime date = (DateTime)value;
                
                string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

                return string.Format("{0}-{1}-{2}", date.Day, months[date.Month - 1], date.Year);
            }

            return value;
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }

    public class DoubleToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double number = (double)value;
                return number.ToString("F2");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class KeyframeDilateConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*
            if (SpiritAnimatorController.Instance.ActiveSpirit.ActiveBehavior != null)
            {
                double dilate = SpiritAnimatorController.Instance.ActiveSpirit.ActiveBehavior.TimeDilateFactor;
                if (value is double)
                {
                    double num = ((double)value * dilate);
                    return num.ToString("F2");
                }
            }*/
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

}
