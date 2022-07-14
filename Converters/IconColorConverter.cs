using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace VoterX.Utilities.Converters
{
    public class IconColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Application.Current.Resources["ApplicationForegroundColor"];
            if (value is string style)
            {
                //switch (style)
                //{
                //    case "ApplicationDangerColor":
                //        result = Application.Current.Resources["ApplicationDangerColor"];
                //        break;
                //    case "ApplicationForegroundColor":
                //    default:
                //        result = Application.Current.Resources["ApplicationForegroundColor"];
                //        break;
                //}
                result = Application.Current.Resources[style];
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }
}
