using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace VoterX.Utilities.Converters
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int result = 0;
                if (Int32.TryParse(value.ToString(), out result) == true)
                {
                    return result * 2;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int result = 0;
                if (Int32.TryParse(value.ToString(), out result) == true)
                {
                    return result / 2;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
    }
}
